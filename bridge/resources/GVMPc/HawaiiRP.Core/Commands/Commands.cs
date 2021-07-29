using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;
using GVMPc.Vehicles;
using MySql.Data.MySqlClient;
using GVMPc.Laptop;
using System.Linq;

namespace GVMPc.Commands
{
    class Commands : Script
    {
        public static List<Client> adminDuty = new List<Client>();

		public static bool ignoring = false;

        [Command("tune")]
        public void CMD_Tune(Client p, int id1, int id2)
        {
            try
            {
                if (Database.getPlayerRights(p.Name) >= 97)
                {
                    if (p.IsInVehicle)
                    {
                        p.Vehicle.SetMod(id1, id2);
                        Database.changeVehicleTuning(p.Vehicle.NumberPlate, id1, id2);
                        Notification.SendPlayerNotifcation(p, "Du hast dein Fahrzeug erfolgreich getuned.", 5000, "white", "TUNER", "blue");
                    }
                    else
                    {
                        Notification.SendPlayerNotifcation(p, "Du sitzt in keinem Fahrzeug.", 5000, "white", "TUNER", "blue");
                    }

                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung für diesen Befehl.", 5000, "white", "TUNER", "blue");
                    return;
                }

            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("color")]
        public void CMD_Color(Client p, int id1, int id2, int id3)
        {
            try
            {
                if (Database.getPlayerRights(p.Name) >= 97)
                {
                    if (p.IsInVehicle)
                    {
                        p.Vehicle.CustomPrimaryColor = new Color(id1, id2, id3);
                        p.Vehicle.CustomSecondaryColor = new Color(id1, id2, id3);
                        Notification.SendPlayerNotifcation(p, "Du hast die Farbe geändert.", 5000, "white", "TUNER", "blue");
                    }
                    else
                    {
                        Notification.SendPlayerNotifcation(p, "Du sitzt in keinem Fahrzeug.", 5000, "white", "TUNER", "blue");
                    }

                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung für diesen Befehl.", 5000, "white", "TUNER", "blue");
                    return;
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

        [Command("giveitem", Alias = "item")]
        public void CMD_GiveWeapon(Client p, string name, string item, int count)
        {
            try
            {
				if (Database.getPlayerRights(p.Name) >= 99)
				{
					if (Items.ItemManager.itemList.Contains(item))
					{

						Database.changeInventoryItem(name, item, count, false);
						Notification.SendPlayerNotifcation(p, "Du hast dir das Item " + item + " gegeben.", 5000, "white", "INFORMATION", "white");
						//Discord.SendMessage("Item geben.", "Der Admin " + p.Name + " hat " + name + count + item + " gegeben", "[]", Discord.COLORS.RED, Webhook.COMMANDS);
					} else
					{
						Notification.SendPlayerNotifcation(p, "Das Item existiert nicht.", 3500, "red", "INFORMATION", "red");
					}
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red");
				}

				} catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("aduty", Alias = "admindienst")]
        public void CMD_ADuty(Client p)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }
                if (isPlayerInAduty(p))
                {
                    //Discord.DiscordWebhooks.SendMessage("Spieler verlässt Admindienst", "Der Spieler " + p.Name + " hat den Admindienst verlassen.", Discord.DiscordWebhooks.adminWebhook, "Admin-Log");
                    Notification.SendPlayerNotifcation(p, "Du bist nun nicht mehr im Admindienst.", 5000, "red", "ADMINISTATION", "white");
                    Database.restoreCustomization(p);
                    adminDuty.Remove(p);
                    p.TriggerEvent("aduty:toggleaduty", false);
                    p.TriggerEvent("setPlayerAduty", !p.GetSharedData("aduty"));
                    p.TriggerEvent("updateAduty", !p.GetSharedData("aduty"));
                    p.TriggerEvent("setInvincible", false);
                    p.SetSharedData("aduty", false);
                    Items.Weapons.WeaponSync(p);
                }
                else
                {
                    //Discord.DiscordWebhooks.SendMessage("Spieler betritt Admindienst", "Der Spieler " + p.Name + " hat den Admindienst betreten.", Discord.DiscordWebhooks.adminWebhook, "Admin-Log");
                    Notification.SendPlayerNotifcation(p, "Du bist nun im Admindienst. (Dein Rang: " + Database.getPlayerRank(p.Name).rankName + ")", 5000, "red", "ADMINISTATION", "white");
                    Clothing.PlayerClothes.setAdmin(p, Database.getPlayerRank(p.Name).clothId);
                    adminDuty.Add(p);
                    p.TriggerEvent("aduty:toggleaduty", true);
                    p.TriggerEvent("setInvincible", true);
                    p.TriggerEvent("setPlayerAduty", !p.GetSharedData("aduty"));
                    p.TriggerEvent("updateAduty", !p.GetSharedData("aduty"));
                    p.SetSharedData("aduty", true);

                }
            } catch(Exception ex) { Log.Write(ex.Message); }


        }

		[Command("frisk")]
		public void CMD_FRISK(Client p)
		{
			p.TriggerEvent("openWindow", "Frisk", "{\"weaponListObject\":{\"PersonToFrisk\":\""+ p.Name +"\",\"CanForceWeaponDrop\":\"false\",\"WeaponList\":[{\"WeaponName\":\"Assaultrifle\",\"WeaponIcon\":\"Assaultrifle.png\",\"WeaponCount\":\"1\"}]}}");
		}

		[RemoteEvent("closedWeaponFrisk")]
		public void closedWeaponFrisk(Client p, string Data)
		{
			Log.Write(Data + "");
			p.TriggerEvent("closeWindow", "Frisk");
		}

		[Command("tpm", Alias = "tptowaypoint")]
        public void CMD_TPM(Client p)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "ADMINISTATION", "red"); return; }

                if (isPlayerInAduty(p))
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dich zu deinem Wegpunkt teleportiert.", 5000, "red", "ADMINISTATION", "white");
                    p.TriggerEvent("aduty:tptoway");
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du bist nicht im Admindienst.", 5000, "red", "ADMINISTATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

        [Command("ped")]
        public void CMD_PED(Client p, string model)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

				PedHash hash = NAPI.Util.PedNameToModel(model);

				p.SetSkin(hash);

				Notification.SendPlayerNotifcation(p, "Du hast dich erfolgreich verwandelt", 4500, "red", "PED", "");

            } catch(Exception ex) { Log.Write(ex.Message); }

        }

        [Command("warns")]
        public void CMD_Warns(Client p, string name)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                int warns = 0;

                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (Start.loggedInPlayers.ContainsKey(target))
                    {
                        if (Start.loggedInPlayers[target] == name)
                        {
                            warns = (int)Database.getWarns(target.SocialClubName);
                        }
                    }
                }
                Notification.SendPlayerNotifcation(p, "Warns von " + name + ": " + warns, 5000, "red", "ADMINISTATION", "white");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("warn")]
        public void CMD_Warn(Client p, string name)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                int warns = 0;

                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (Start.loggedInPlayers.ContainsKey(target))
                    {
                        if (Start.loggedInPlayers[target] == name)
                        {
                            //Discord.DiscordWebhooks.SendMessage("Spieler gewarnt", "Der Spieler " + p.Name + " hat den Spieler " + target.Name + " gewarnt.", Discord.DiscordWebhooks.warnWebhook, "Warn-Log");
                            Database.changeWarns(target.SocialClubName, 1, false);
                            warns = (int)Database.getWarns(target.SocialClubName);
                            target.Kick("Du wurdest gekickt: Warn");
							target.TriggerEvent("openWindow", new object[2]
							{
								"Kick",
								"{\"name\":\"" + p.Name +"\",\"reason\":\"Warn}"
							});
                        }
                    }
                }
                Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + name + " gewarnt. Warns von " + name + ": " + warns, 3500, "red", "ADMINISTATION", "white");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }


        [Command("xcm", GreedyArg = true)]
        public void CMD_XCM(Client p, string arg)
        {
            try
            {
                string[] args = arg.Split(" ");

                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                string targetname = args[0];
                string reason = arg.Replace(targetname + " ", "").Replace(targetname, "");

                if (Database.getPlayerRights(p.Name) > 97)
                {
                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (Start.loggedInPlayers.ContainsKey(target))
                        {
                            if (Start.loggedInPlayers[target] == targetname)
                            {
								target.TriggerEvent("openWindow", new object[2]
								{
									"Bann",
									"{\"name\":\"" + target.Name + "\"}"
								});
                                //Discord.DiscordWebhooks.SendMessage("Spieler gebannt", "Der Spieler " + p.Name + " hat den Spieler " + target.Name + " gebannt. Grund: " + reason, Discord.DiscordWebhooks.banWebhook, "Ban-Log");
                                Database.banPlayer(target.SocialClubName, reason);
                                Notification.SendGlobalNotification("Der Spieler " + target.Name + " hat von dem " + Database.getPlayerRank(p.Name).rankName + " " + p.Name + " einen permanenten Communityausschluss erhalten.", 8000, "#f30202", Notification.icon.dev);
                                target.Kick();
								target.TriggerEvent("bannotify", reason);
                            }
                        }
                    }
                    Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + targetname + " gebannt.", 3500, "red", "ADMINISTATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("clearfrak")]
        public void CMD_PhoneClearfrak(Client p)
        {
            Handy.LeaderApp.PhoneClearfrak(p);
        }

        [Command("unban")]
        public void CMD_Unban(Client p, string arg)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                string targetname = arg;

                if (Database.getPlayerRights(p.Name) > 95)
                {
                    Database.unbanPlayer(targetname);
                    Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + targetname + " entbannt.", 3500, "red", "ADMINISTATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }



        [Command("kick", GreedyArg = true)]
        public void CMD_KICK(Client p, string arg)
        {
            try
            {
                string[] args = arg.Split(" ");

                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                string targetname = args[0];
                string reason = arg.Replace(targetname + " ", "").Replace(targetname, "");

                if (Database.getPlayerRights(p.Name) > 90)
                {
                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (Start.loggedInPlayers.ContainsKey(target))
                        {
                            if (Start.loggedInPlayers[target] == targetname)
                            {
								target.TriggerEvent("openWindow", new object[2]
                                {
								   "Kick",
									"{\"name\":\""+ target.Name +"\",\"grund\":\"" + reason +"\"}"
                                });
								//Discord.DiscordWebhooks.SendMessage("Spieler gekickt", "Der Spieler " + p.Name + " hat den Spieler " + target.Name + " gekickt. Grund: " + reason, Discord.DiscordWebhooks.kickWebhook, "Kick-Log");
								Notification.SendGlobalNotification("Der Spieler " + target.Name + " wurde von dem " + Database.getPlayerRank(p.Name).rankName + " " + p.Name + " gekickt.", 8000, "#f30202", Notification.icon.dev);
                                target.Kick();
								target.TriggerEvent("kicknotify", reason);
							}
						}
                    }
                    Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + targetname + " gekickt.", 3500, "red", "ADMINISTATION", "white");
				}
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("adminclothes")]
        public void CMD_AdminClothes(Client p)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                if (Database.getPlayerRights(p.Name) > 98)
                {
                    p.TriggerEvent("adminclothes:open");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("goto")]
        public void CMD_GOTO(Client p, string name)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                Vector3 targetPos = new Vector3(0, 0, 0);

                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (Start.loggedInPlayers.ContainsKey(target))
                    {
                        if (Start.loggedInPlayers[target] == name)
                        {
                            targetPos = target.Position;
                        }
                    }
                }

                if (targetPos.X == 0 && targetPos.Y == 0 && targetPos.Z == 0)
                {
                    Notification.SendPlayerNotifcation(p, "Der Spieler wurde nicht gefunden.", 3500, "red", "ADMINISTATION", "white");
                }
                else
                {
                    Anticheat.Wait(p); p.Position = targetPos;
                    Notification.SendPlayerNotifcation(p, "Du hast dich zu Spieler " + name + " teleportiert.", 3500, "red", "ADMINISTATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

		[Command("track")]
		public void CMD_TRACKER(Client p, string plate)
		{
			try
			{
				Vehicle vehicle = NAPI.Pools.GetAllVehicles().Find((Vehicle t) => t.NumberPlate == plate);

				if (p.GetSharedData("FRAKTION") == "FIB")
				{

					if (vehicle.NumberPlate == plate)
					{
						Blip trackingBlip = NAPI.Blip.CreateBlip(433, vehicle.Position, 1f, 1, "Getracktes Fahrzeug", 255, 0, true, 0, uint.MaxValue);
						Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug mit dem Kennzeichen " + plate + " getrackt", 4500, "white", "TRACKER", "");
						NAPI.Task.Run(delegate
						{
							NAPI.Blip.SetBlipTransparency(trackingBlip, 0);
							Notification.SendPlayerNotifcation(p, "Du hast die Verbindung zu Fahrzeug " + plate + " verloren", 4500, "red", "TRACKER", "");

						}, 100000);

					} else
					{
						Notification.SendPlayerNotifcation(p, "Dieses Kennzeichen ist ungültig", 4500, "red", "TRACKER", "");
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht berechtigt dazu", 4500, "red", "", "");
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

        [Command("bring")]
        public void CMD_BRING(Client p, string name)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (Start.loggedInPlayers.ContainsKey(target))
                    {
                        if (Start.loggedInPlayers[target] == name)
                        {
                            target.Position = p.Position;
                        }
                    }
                }


                Notification.SendPlayerNotifcation(p, "Du hast Spieler " + name + " zu dir teleportiert.", 3500, "red", "ADMINISTATION", "white");
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

        [Command("tppos", Alias = "tp")]
        public void CMD_TP(Client p, float x, float y, float z)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                if (isPlayerInAduty(p))
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dich zu den Koordinaten teleportiert.", 5000, "red", "ADMINISTATION", "white");
                    Anticheat.Wait(p); p.Position = new Vector3(x, y, z);
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du bist nicht im Admindienst.", 5000, "red", "ADMINISTATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

		[Command("packblackmoney")]
		public void CMD_PACKBLACKMONEY(Client p, int amount)
		{

			if (p.HasData("IS_PACKINGBLACKMONEY"))
				return;

			int count = (int)Database.getBlackMoney(p.Name);

			if (amount > Database.getBlackMoney(p.Name))
			{
				Notification.SendPlayerNotifcation(p, "Du hast nicht genug Schwarzgeld dabei", 4500, "red", "", "");
			} else if(amount > 25000)
			{
				Notification.SendPlayerNotifcation(p, "Du kannst nicht mehr als 25000$ Schwarzgeld packen", 5000, "red", "", "");
			}
			else
			{
				p.SetData("IS_PACKINGBLACKMONEY", true);
				Functions.disableAllPlayerControls(p, true);
				p.TriggerEvent("sendProgressbar", 10000);
				NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
				NAPI.Task.Run(delegate
				{
					Functions.disableAllPlayerControls(p, false);
					Database.changeBlackMoney(p.Name, amount, true);
					Database.changeInventoryItem(p.Name, "Schwarzgeld", amount, false);
					Notification.SendPlayerNotifcation(p, "Du hast " + amount + " Schwarzgeld gepackt", 5000, "red", "", "");
					NAPI.Player.StopPlayerAnimation(p);
					p.SetData("IS_PACKINGBLACKMONEY", false);
				}, 10000);

			}
		}

        [Command("pos", Alias = "position")]
        public void CMD_Pos(Client p)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                Log.Write("POS: " + p.Position.X.ToString().Replace(",", ".") + ", " + p.Position.Y.ToString().Replace(",", ".") + ", " + (p.Position.Z - 1.1f).ToString().Replace(",", ".") + " ROT: " + p.Rotation.ToString().Replace(",", "."));
				Log.Write("{'x':" + p.Position.X.ToString().Replace(",", ".") + ",'y':" + p.Position.Y.ToString().Replace(",", ".") + ",'z':" + p.Position.Z.ToString().Replace(",", ".") + "}");
				Notification.SendPlayerNotifcation(p, "POS: " + p.Position.X.ToString().Replace(",", ".") + ", " + p.Position.Y.ToString().Replace(",", ".") + ", " + (p.Position.Z - 1.1f).ToString().Replace(",", ".") + " ROT: " + p.Rotation.ToString().Replace(",", "."), 8000, "red", "ADMINISTATION", "white");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("dimension")]
        public void CMD_Dimension(Client p, string name, int dimension)
        {
			Client target = Database.getPlayerFromName(name);

            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                if (Database.getPlayerRights(p.Name) >= 97)
                {
                    target.Dimension = (uint)dimension;
                    Notification.SendPlayerNotifcation(p, "Deine Dimension wurde auf " + dimension + " gesetzt.", 5000, "red", "ADMINISTATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

        [Command("createfraktion")]
        public void CMD_CreateFraktion(Client p, string name, string shortname)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                if (Database.getPlayerRights(p.Name) == 100)
                {
                    Database.createFraktion(name, shortname);
                    Notification.SendPlayerNotifcation(p, "Die Fraktion " + name + " wurde erstellt.", 5000, "white", "INFORMATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

		[Command("voice")]
		public void CMD_Voice(Client p)
		{
			try
			{
				p.TriggerEvent("ConnectTeamspeak", false);
				NAPI.Task.Run(() =>
				{
					p.TriggerEvent("ConnectTeamspeak", true);
				}, 5000);
			}
			catch (Exception ex) { Log.Write(ex.Message); }

		}

		[Command("setfrak", Alias = "sf")]
		public void setFraktion(Client p, string name, string fraktion)
		{
			Client target = Database.getPlayerFromName(name);

			try
			{

				MySqlConnection val = new MySqlConnection(Start.connectionString);
				try
				{
					val.Open();
					MySqlCommand val2 = val.CreateCommand();
					val2.CommandText = "UPDATE users SET fraktion = @fraktion WHERE name = @name";
					val2.Parameters.AddWithValue("@fraktion", fraktion.Replace("_", " "));
					val2.Parameters.AddWithValue("@name", name);
					val2.ExecuteNonQuery();
					target.SetSharedData("FRAKTION", fraktion);
					Notification.SendPlayerNotifcation(p, "Du hast die Fraktion von " + name + " auf " + fraktion.Replace("_", " ") + " geupdatet", 5000, "red", "Administrator", "");
					

				} catch (Exception ex)
				{
					Log.Write(ex.Message + ex.StackTrace);

				} finally
				{
					val.Dispose();
				}



			} catch (Exception ex)
			{
				Log.Write(ex.Message + ex.StackTrace);
			}
		}

		[Command("addblackmoney")]
		public void CMD_AddblackMoney(Client p, string name, int amount)
		{
			try
			{
				if(!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

				if(Database.getPlayerRights(p.Name)  >= 98)
				{
					Database.changeBlackMoney(name, amount, false);
					p.TriggerEvent("updateBlackMoney", amount);
					Notification.SendPlayerNotifcation(p, "Du hast dir " + amount + " Schwarzgeld gegeben", 4500, "red", "Administration", "");
					//Discord.SendMessage("Schwarzgeld geben", "Der Admin " + p.Name + " hat " + name + " " + amount + " Schwarzgeld addiert", "[]", Discord.COLORS.RED, Webhook.COMMANDS);
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

        [Command("announce")]
        public void CMD_Announce(Client p, string message, int icon)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                if (Database.getPlayerRights(p.Name) >= 95)
                {
					if(icon == 0)
					{
						Notification.SendGlobalNotification(message, 8000, "yellow", Notification.icon.casino);
					} else if(icon == 1)
					{
						Notification.SendGlobalNotification(message, 8000, "yellow", Notification.icon.dev);
					} else if(icon == 2)
					{
						Notification.SendGlobalNotification(message, 8000, "yellow", Notification.icon.glob);
					} else if(icon == 3)
					{
						Notification.SendGlobalNotification(message, 8000, "yellow", Notification.icon.glob);
					} else if(icon == 4)
					{
						Notification.SendGlobalNotification(message, 8000, "yellow", Notification.icon.wed);
					}
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung.", 5000, "white", "INFORMATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

        [Command("onlist")]
        public void CMD_Onlist(Client p)
        {
            Notification.SendPlayerNotifcation(p,"" + NAPI.Pools.GetAllPlayers().Count, 5000, "white", "Spielerzahl", "white");
        }

        [Command("v", Alias = "vanish")]
        public void CMD_Vanish(Client p, int toggle)
        {

            try
            {

                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                if (isPlayerInAduty(p))
                {
                    if(toggle == 1)
                    {
                        p.Transparency = 0;
                        Notification.SendPlayerNotifcation(p, "Du bist nun unsichtbar.", 3500, "red", "ADMINISTRATION", "white");
                    }
                    else
                    {
                        p.Transparency = 255;
                        Notification.SendPlayerNotifcation(p, "Du bist nun sichtbar.", 3500, "red", "ADMINISTRATION", "white");
                    }
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du bist nicht im Admindienst.", 3500, "red", "ADMINISTRATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

		[Command("updatevehicle")]
		public void CMD_UPDATEVEHICLE(Client p, int fuel, int distance)
		{
			Vehicle veh = p.Vehicle;

			Notification.SendPlayerNotifcation(p, "Vehicle Data geupdatet", 5000, "red", "Administration", "");

			p.TriggerEvent("updateVehicleData", fuel, distance, 200, false, true);
		}

        [Command("veh", Alias = "car")]
        public void CMD_Veh(Client p, string vehname)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                VehicleHash vehHash = (VehicleHash)NAPI.Util.GetHashKey(vehname);
                if (vehHash == 0)
                {
                    Notification.SendPlayerNotifcation(p, "Ungültiges Fahrzeug.", 5000, "white", "INFORMATION", "white");
                    return;
                }
                Vehicle veh = NAPI.Vehicle.CreateVehicle(vehHash, p.Position, p.Rotation, 5, 5);
                veh.NumberPlate = "AR-" + Database.getPlayerRights(p.Name);
                veh.CustomPrimaryColor = Database.getPlayerRank(p.Name).rgbColor;
                veh.CustomSecondaryColor = Database.getPlayerRank(p.Name).rgbColor;
                veh.Dimension = p.Dimension;
                p.SetIntoVehicle(veh, -1);
				veh.SetSharedData(VehicleData.VEHICLE_FUEL_STATUS, 100);
				veh.SetSharedData(VehicleData.VEHICLE_LOCKED_STATUS, false);
				veh.SetSharedData(VehicleData.VEHICLE_MILEAGE_STATUS, 10000);
                //Discord.DiscordWebhooks.SendMessage("Spieler spawnt Fahrzeug", "Der Spieler " + p.Name + " hat das Fahrzeug " + vehname + " gespawnt.", Discord.DiscordWebhooks.adminWebhook, "Admin-Log");
                Notification.SendPlayerNotifcation(p, "Das Fahrzeug wurde erfolgreich gespawnt.", 5000, "white", "INFORMATION", "white");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		[Command("test")]
		public void CMD_TEST(Client p)
		{
			object JSONobject = new
			{
				Besitzer = "   Kein Besitzer",
				Kosten = "   120000$"
			};

			p.TriggerEvent("sendInfocard", "Naturschutz Tankstelle", "red", "fuelstation_erwin.jpg", 4500, NAPI.Util.ToJson(JSONobject));
		}

        [Command("dv", Alias = "delcar")]
        public void CMD_DV(Client p)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }
                if (p.IsInVehicle)
                {
                    p.Vehicle.Delete();
                    Notification.SendPlayerNotifcation(p, "Dein Fahrzeug wurde entfernt.", 5000, "white", "INFORMATION", "white");
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du sitzt in keinem Fahrzeug.", 5000, "white", "INFORMATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("perso")]
        public void CMD_Perso(Client p)
        {
			if (Database.getItemCount(p.Name, "Personalausweis") > 0)
			{
				Personalausweis.showPerso(p, p.Name.Split("_")[0], p.Name.Split("_")[1], "Vinewood Street 1", (int)Database.getPlayerPlaytime(p.SocialClubName), 0, 0, 0);
			}
			else if (Database.getItemCount(p.Name, "FakeID") > 0)
			{
				Random random = new Random();
				switch (random.Next(1, 20))
				{
					case 1:
						Personalausweis.showPerso(p, "Alessio", "Champion", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 2:
						Personalausweis.showPerso(p, "Mohammed", "Ali", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 3:
						Personalausweis.showPerso(p, "Justin", "Heinz", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 4:
						Personalausweis.showPerso(p, "John", "Edson", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 5:
						Personalausweis.showPerso(p, "Kai", "Grazio", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 6:
						Personalausweis.showPerso(p, "Jefferson", "White", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 7:
						Personalausweis.showPerso(p, "Peter", "Sauer", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 8:
						Personalausweis.showPerso(p, "Peter", "Lustig", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 9:
						Personalausweis.showPerso(p, "Tom", "Champion", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 10:
						Personalausweis.showPerso(p, "Lucio", "Favre", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 11:
						Personalausweis.showPerso(p, "Kenny", "West", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 12:
						Personalausweis.showPerso(p, "Johnatahn", "McLaren", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 13:
						Personalausweis.showPerso(p, "Nick", "Heinzmann", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 14:
						Personalausweis.showPerso(p, "Hamudi", "Saleme", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 15:
						Personalausweis.showPerso(p, "Abu", "Antar", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 16:
						Personalausweis.showPerso(p, "Abu", "Cafar", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 17:
						Personalausweis.showPerso(p, "Dennis", "Aramani", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 18:
						Personalausweis.showPerso(p, "Tim", "Hilton", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 19:
						Personalausweis.showPerso(p, "Finn", "Jacker", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 20:
						Personalausweis.showPerso(p, "Rancho", "Hancho", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
				}
			}
			{
				Notification.SendPlayerNotifcation(p, "Du hast keinen Perso dabei", 4500, "red", "", "");
			}
        }

		[Command("dienstausweis")]
		public void CMD_Dienstausweis(Client p)
		{
			Dienstausweis.showDienstausweis(p, "FIB", 15, 1, p.Name, p.Name, "NULL", 3);
		}

		[Command("closeWindow")]
		public void CMD_CLOSEWINDOW(Client p, string name)
		{
			p.TriggerEvent("closeWindow", name);
		}

		[Command("report")]
		public void CMD_Report(Client p, string name, string reason)
		{
			try
			{
				Notification.SendPlayerNotifcation(p, "Der Report wurde an die Adminitration gemeldet.", 4500, "red", "Report", "");
				foreach(Client client in NAPI.Pools.GetAllPlayers())
				{
					if (ignoring == true)
						return;

					if(Database.getPlayerRights(client.Name) > 0)
					{
						Notification.SendPlayerNotifcation(p, "Title: " + name + " Grund: " + reason + " Von " + p.Name , 10000, "red", "TICKET", "");
					}
				}


			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[Command("support", GreedyArg = true)]
		public void CMD_Support(Client p, string description)
		{
			try
			{
				if (description.Contains("HURENSOHN")) return;

				if(!p.HasData("HAS_TICKET"))
				{
					Laptop.SupportApp.addTicket(p, description);
					Notification.SendPlayerNotifcation(p, "Die Administration wurde informiert", 5000, "red", "TICKETSYSTEM", "");
					foreach(Client c in NAPI.Pools.GetAllPlayers())
					{
						if(Database.isPlayerTeam(c.Name))
						{
							Notification.SendPlayerNotifcation(p, "Neues Ticket von " + p.Name, 5000, "red", "TICKETSYSTEM", "");
						}
					}

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast bereits ein Ticket", 5000, "red", "TICKETSYSTEM", "");
				}

			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[Command("ignore")]
		public void CMD_Ignore(Client p)
		{
			if(Database.getPlayerRights(p.Name) > 0)
			{
				Notification.SendPlayerNotifcation(p, "Du ignorierst ab jetzt alle Tickets", 5000, "red", "TICKET", "red");
				ignoring = true;

			}
		}

		[Command("unignore")]
		public void CMD_Unignore(Client p)
		{
			if(Database.getPlayerRights(p.Name) > 0)
			{
				Notification.SendPlayerNotifcation(p, "Du nimmst ab jetzt wieder Tickets an", 5000, "red", "TICKET", "red");
				ignoring = false;
			}
		}

		[Command("givecarkey")]
		public void givecarkey(Client p, string name)
		{
			try
			{
				Vehicle vehicle = p.Vehicle;

				if(Database.isVehicleOwnedByPlayer(p.Name, vehicle.NumberPlate))
				{
					Database.setUserRenter(name, vehicle.NumberPlate);
					Notification.SendPlayerNotifcation(p, "Du hast deinen Schlüssel an " + name + " weitergegeben", 4500, "white", "", "");
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[Command("askin")]
		public void CMD_askin(Client player, int component, int drawable, int texture)
		{
			NAPI.Player.SetPlayerClothes(player, component, drawable, texture);
		}

		[Command("ooc", GreedyArg = true)]
        public void CMD_OOC(Client p, string args)
        {
            try
            {

                if (args.ToLower().Contains("paradox"))
                {
                    Functions.XCM(p);
                }

                if (args.ToLower().Contains("parad0x"))
                {
                    Functions.XCM(p);
                }

				if(args.ToLower().Contains("Hurensohn"))
				{
					Functions.XCM(p);
				}

                NAPI.ClientEvent.TriggerClientEventInRange(p.Position, 100.0f, "sendPlayerNotification", args, 5000, "green", "OOC - (" + p.Name + ")", "");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("quitffa")]
        public void CMD_QUITFFA(Client p)
        {
            try
            {
                if (Other.Paintball.würfelparkPlayers.Contains(p) && !p.Dead)
                {
                    NAPI.Player.SpawnPlayer(p, p.Position);
                    Other.Paintball.würfelparkPlayers.Remove(p);
                    p.ResetData(Other.Paintball.PAINTBALL_DEATHS);
                    p.ResetData(Other.Paintball.PAINTBALL_KILLS);
                    p.TriggerEvent("finishPaintball");
                    Anticheat.Wait(p); p.Position = Other.Paintball.paintballs["Würfelpark"];
                    Notification.SendPlayerNotifcation(p, "Du hast Paintball verlassen.", 5000, "white", "INFORMATION", "white");
                    p.Dimension = 0;
                    Items.Weapons.WeaponSync(p);
					p.RemoveAllWeapons();
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du bist nicht in Paintball.", 5000, "white", "INFORMATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
            
        }


		[Command("rescue")]
		public void CMD_Rescue(Client p, Client target = null)
		{
			try
			{
				if (p.GetSharedData("FRAKTION") == "Los Santos Medical Department")
				{
					if (p.Position.DistanceTo(target.Position) < 2f)
					{
						NAPI.Player.PlayPlayerAnimation(p, 33, "", "", 0);
						Functions.disableAllPlayerControls(p, true);
						p.TriggerEvent("sendProgressbar", new object[1]
						{
						60000
						});
						NAPI.Task.Run(delegate
						{
							Functions.disableAllPlayerControls(p, false);
							NAPI.Player.StopPlayerAnimation(p);
							target.Health = 100;
							Database.setDeathStatus(target.Name, false);
							target.Armor = 0;
							Items.Weapons.WeaponSync(target);
							Notification.SendPlayerNotifcation(p, "Du hast jemanden wiederbelebt", 4500, "red", "LSMC", "");
							Notification.SendPlayerNotifcation(target, "Du wurdest wiederbelebt", 4500, "red", "", "");

						}, 60000);

					} else
					{
						Notification.SendPlayerNotifcation(p, "Es ist kein Spieler in deiner nähe", 4500, "red", "LSMC", "");	
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist kein Mediziner", 4500, "red", "LSMC", "");
				}


			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[Command("revive")]
        public void CMD_Revive(Client p, string name)
        {
            try
            {
                if (!Database.isPlayerTeam(p.Name)) { Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red"); return; }

                if (Database.getPlayerRights(p.Name) < 93)
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dazu keine Berechtigung.", 3500, "red", "INFORMATION", "red");
                }
                else
                {
                    if (name == "me")
                    {
                        //Discord.DiscordWebhooks.SendMessage("Spieler revived", "Der Spieler " + p.Name + " hat sich selber revived.", Discord.DiscordWebhooks.reviveWebhook, "Revive-Log");
                        Database.setDeathStatus(p.Name, false);
                        Notification.SendPlayerNotifcation(p, "Du hast dich wiederbelebt.", 5000, "red", "INFORMATION", "red");
                        NAPI.Player.SpawnPlayer(p, p.Position);
						NAPI.Player.StopPlayerAnimation(p);
						p.TriggerEvent("updateInjured", false);
                        p.TriggerEvent("stopScreenEffect", "DeathFailOut");
                        p.TriggerEvent("setInvincible", false);
                        Functions.disableAllPlayerControls(p, false);
                        Anticheat.Wait(p); p.Health = 200;
                        p.Armor = 0;
                        Items.Weapons.WeaponSync(p);
                    }
                    else
                    {
                        bool found = false;
                        foreach (KeyValuePair<Client, string> item in Start.loggedInPlayers)
                        {
                            if (item.Value == name)
                            {
                                found = true;
                                //Discord.DiscordWebhooks.SendMessage("Spieler revived", "Der Spieler " + p.Name + " hat den Spieler " + item.Key.Name + " revived.", Discord.DiscordWebhooks.kickWebhook, "Kick-Log");
                                Notification.SendPlayerNotifcation(p, "Du hast den Spieler " + name + " wiederbelebt.", 5000, "red", "INFORMATION", "red");
                                Database.setDeathStatus(item.Key.Name, false);
								item.Key.TriggerEvent("updateInjured", false);
                                Notification.SendPlayerNotifcation(item.Key, "Du wurdest von Teammitglied " + p.Name + " wiederbelebt.", 5000, "red", "INFORMATION", "red");
                                Start.deathTime.Remove(item.Key);
                                NAPI.Player.SpawnPlayer(item.Key, item.Key.Position);
								item.Key.StopAnimation();
                                item.Key.TriggerEvent("stopScreenEffect", "DeathFailOut");
                                item.Key.TriggerEvent("setInvincible", false);
                                item.Key.TriggerEvent("disableAllPlayerActions", false);
								Functions.disableAllPlayerControls(item.Key, false);
                                item.Key.Health = 200;
                                item.Key.Armor = 0;
                                Items.Weapons.WeaponSync(item.Key);
                            }
                        }

                        if (!found)
                            Notification.SendPlayerNotifcation(p, "Der Spieler wurde nicht gefunden.", 5000, "white", "INFORMATION", "white");

                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		[Command("dvall")]
        public void CMD_DVALL(Client p)
        {
            try
            {
                if (Database.getPlayerRights(p.Name) > 98)
                {
                    foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                    {
                        vehicle.Delete();
                    }
                    Notification.SendPlayerNotifcation(p, "Alle Fahrzeuge gelöscht.", 5000, "white", "INFORMATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [Command("dvradius")]
        public void CMD_DVALL(Client p, int radius)
        {
            try
            {
                if (Database.getPlayerRights(p.Name) > 98)
                {
                    foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                    {
                        if (p.Position.DistanceTo(vehicle.Position) < radius)
                            vehicle.Delete();
                    }
                    Notification.SendPlayerNotifcation(p, "Alle in Fahrzeuge im Radius gelöscht.", 5000, "white", "INFORMATION", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		[Command("setmoney")]
		public void setMoney(Client p, string name, int amount)
		{

			Client client = Database.getPlayerFromName(name);

			MySqlConnection val = new MySqlConnection(Start.connectionString);
			try
			{
				val.Open();
				MySqlCommand val2 = val.CreateCommand();
				val2.CommandText = "UPDATE users SET money = @money WHERE name = @name";
				val2.Parameters.AddWithValue("@money", amount);
				val2.Parameters.AddWithValue("@name", amount);
				val2.ExecuteNonQuery();
				p.TriggerEvent("updateMoney", amount);
				Notification.SendPlayerNotifcation(p, "Du hast dem Spieler " + name + amount + "$ addiert/subtrahiert", 4500, "red", "Administration", "");
				//Discord.SendMessage("Geld geben.", "Der Admin " + p.Name + " hat " + name + " " +amount + "$ addiert", "[]", Discord.COLORS.RED, Webhook.COMMANDS);


			} catch (Exception ex)
			{
				Log.Write(ex.Message + ex.StackTrace);
			} finally
			{
				val.Dispose();
			}
		}

        [Command("packgun")]
        public void CMD_Packgun(Client p)
        {
			if (p.GetData("PACKGUN") == true)
				return;

            try
            {
				if (p.CurrentWeapon == WeaponHash.AdvancedRifle)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.AdvancedRifle);
						Database.changeInventoryItem(p.Name, "Advancedrifle", 1, false);
						Database.changeInventoryItem(p.Name, "Ammoadvancedrifle", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.AssaultRifle)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.AssaultRifle);
						Database.changeInventoryItem(p.Name, "Assaultrifle", 1, false);
						Database.changeInventoryItem(p.Name, "AssaultrifleAmmo", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.CombatPDW)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.CombatPDW);
						Database.changeInventoryItem(p.Name, "PDW", 1, false);
						Database.changeInventoryItem(p.Name, "CombatPDWAmmo", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.HeavyPistol)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.HeavyPistol);
						Database.changeInventoryItem(p.Name, "Heavypistol", 1, false);
						Database.changeInventoryItem(p.Name, "HeavypistolAmmo", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.Pistol)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.Pistol);
						Database.changeInventoryItem(p.Name, "Pistol", 1, false);
						Database.changeInventoryItem(p.Name, "Ammoadvancedrifle", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.Pistol50)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.AdvancedRifle);
						Database.changeInventoryItem(p.Name, "Advancedrifle", 1, false);
						Database.changeInventoryItem(p.Name, "Ammoadvancedrifle", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.Gusenberg)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.AdvancedRifle);
						Database.changeInventoryItem(p.Name, "Advancedrifle", 1, false);
						Database.changeInventoryItem(p.Name, "Ammoadvancedrifle", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.CompactRifle)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.AdvancedRifle);
						Database.changeInventoryItem(p.Name, "Advancedrifle", 1, false);
						Database.changeInventoryItem(p.Name, "Ammoadvancedrifle", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}
				else if (p.CurrentWeapon == WeaponHash.StunGun)
				{
					p.SetData("PACKGUN", true);
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
					Functions.disableAllPlayerControls(p, true);
					p.TriggerEvent("sendProgressbar", new object[1]
					{
					10000
					});
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						p.RemoveWeapon(WeaponHash.AdvancedRifle);
						Database.changeInventoryItem(p.Name, "Advancedrifle", 1, false);
						Database.changeInventoryItem(p.Name, "Ammoadvancedrifle", 2, false);
						Functions.disableAllPlayerControls(p, false);
						p.SetData("PACKGUN", false);
					}, 10000);
				}

            } catch(Exception ex) { Log.Write(ex.Message); }
        }


        public static bool isPlayerInAduty(Client p)
        {
            return adminDuty.Contains(p);
        }

    }
}
