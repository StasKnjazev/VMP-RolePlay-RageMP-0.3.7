using GTANetworkAPI;
using System;
using System.Collections.Generic;
using GVMPc.Jail;
using GVMPc.Players;

namespace GVMPc
{
    class PlayerEvents : Script
    {

        [ServerEvent(Event.PlayerConnected)]
        public static void OnPlayerConnected(Client p)
        {
            try
            {
                p.SetSharedData("PLAYER_RANK", "User");
                p.SetSharedData("voiceRange", 15);
				if(Database.getUserJailtime(p.Name) > 0)
				{
					Jail.Jail.CMD_JAIL(p, p.Name, (int)Database.getUserJailtime(p.Name));
				}
                Anticheat.Wait(p); p.Dimension = (uint)new Random().Next(1000, 9999);
                Anticheat.Wait(p); p.Position = new Vector3(-2633.423, 1891.713, 158.4416).Add(new Vector3(0, 0, 1.5));
                p.Rotation = new Vector3(0.0f, 0.0f, 13f);
                Clothing.PlayerClothes.setCaillou(p);
                if (Start.loggedInPlayers.ContainsKey(p))
                {
                    Start.loggedInPlayers.Remove(p);
                }

                Functions.disableAllPlayerControls(p, true);

                if (!Database.isAccountExists(p.SocialClubName))
                    Database.createAccount(p);

				//Discord.SendMessage("Spieler Connected.", "Der Spieler " + p.Name + " ist dem Server gejoint", "[]", Discord.COLORS.GREEN, Webhook.CONNECT);

				p.SetData("IS_FARMING", false);
				p.SetData("GOT_LKWMONEY", false);

                Database.updateAddress(p);
                Anticheat.Wait(p);

                if (Database.isIdentifierBanned("social", p.SocialClubName) || Database.isIdentifierBanned("hwid", p.Serial) || Database.isIdentifierBanned("ip", p.Address))
                {
					p.TriggerEvent("openWindow", new object[2]
                    {
									"Bann",
									"{\"name\":\"" + p.Name + "\"}"
                    });
					p.Kick();
                }

                p.TriggerEvent("guiReady");
                p.TriggerEvent("OnPlayerReady");
                p.TriggerEvent("client:respawning");
                p.TriggerEvent("openWindow", "TextInputBox",
                "{\"textBoxObject\":{\"Title\":\"Registrierung / Login\",\"Message\":\"Gebe bitte deinen Benutzernamen ein (Beispiel: Vorname_Nachname). Falls du noch nicht registriert bist, wirst du automatisch registriert! Im nächsten Schritt kannst du dir dein Passwort auswählen.\",\"Callback\":\"registerUser\",\"CloseCallback\":\"showLoginInput\"}}"
                );
                p.TriggerEvent("componentReady", "TextInputBox");
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

		[RemoteEvent("registerUser")]
        public static void registerUser(Client p, string username)
        {
            int num = 0;
            foreach (char c2 in username)
            {
                if (c2 == '_')
                {
                    num++;
                }
            }
            if (num != 1)
            {
                p.SendNotification("Der Name darf nur aus einem Vor- und Nachnamen bestehen. Beispiel: Max_Mustermann", true);
                p.TriggerEvent("openWindow", new object[2]
                {
                    "TextInputBox",
                    "{\"textBoxObject\":{\"Title\":\"Registrierung / Login\",\"Message\":\"Gebe bitte deinen Benutzernamen ein (Beispiel: Vorname_Nachname). Falls du noch nicht registriert bist, wirst du automatisch registriert! Im nächsten Schritt kannst du dir dein Passwort auswählen.\",\"Callback\":\"registerUser\",\"CloseCallback\":\"showLoginInput\"}}"
                });
                p.TriggerEvent("componentReady", new object[1]
                {
                    "TextInputBox"
                });
            }
            else
            {
				p.TriggerEvent("openWindow", new object[2]
                {
					"Login",
					"{\"name\":\"" + username + "\"}"
                });
				p.Name = username;
			}
        }

        [RemoteEvent("showLoginInput")]
        public void showLoginInput(Client p)
        {
            p.TriggerEvent("openWindow", new object[2]
                {
                    "TextInputBox",
                    "{\"textBoxObject\":{\"Title\":\"Registrierung / Login\",\"Message\":\"Gebe bitte deinen Benutzernamen ein (Beispiel: Vorname_Nachname). Falls du noch nicht registriert bist, wirst du automatisch registriert! Im nächsten Schritt kannst du dir dein Passwort auswählen.\",\"Callback\":\"registerUser\",\"CloseCallback\":\"showLoginInput\"}}"
                });
            p.TriggerEvent("componentReady", new object[1]
            {
                    "TextInputBox"
            });
        }

        [RemoteEvent("stopAnimation")]
        public void stopAnimation(Client p)
        {
            try
            {
                NAPI.Player.StopPlayerAnimation(p);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("playFunkAnimation")]
        public void playFunkAnimation(Client p)
        {
            try
            {
                NAPI.Player.PlayPlayerAnimation(p, 49, "random@arrests", "generic_radio_chatter", 8f);
                p.SetSharedData("isTalkingFunk", true);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("changeUserAccount")]
        public void changeUserAccount(Client p)
        {
			p.Kick();
        }

        [RemoteEvent("PlayerLogin")]
        public void Login(Client p, string password)
        {
            if (password == null)
                return;

            try
            {
                Anticheat.Wait(p);

                string result = Database.loginUser(p.Name, password, p);

                string name = p.Name;

					if (result.Contains("eingeloggt"))
					{

						p.SetSharedData("PLAYER_RANK", Database.getPlayerRank2(name));
						p.TriggerEvent("LoadIpls");

						if (!Start.loggedInPlayers.ContainsKey(p))
							Start.loggedInPlayers.Add(p, name);

						Functions.disableAllPlayerControls(p, false);

						//Discord.DiscordWebhooks.SendMessage("Spieler connected.", "Der Spieler " + name + " hat sich eingeloggt.", Discord.DiscordWebhooks.joinWebhook, "Join-Log");

						if (Database.isPlayerTeam(name))
							Notification.SendPlayerNotifcation(p, "Deine Rechte wurden vollständig initialisiert.", 5000, "white", "INFORMATION", "white");

						p.TriggerEvent("ConnectTeamspeak", true);

						p.Dimension = 0;
						p.SetSharedData("aduty", false);

						p.Name = name;




						if (Database.isMaintenance())
						{
							if (!Database.isPlayerTeam(p.Name))
							{
							p.TriggerEvent("openWindow", "Register");
							p.Kick("Wartungsarbeiten");
							}
						}

						Anticheat.Wait(p); p.Position = Database.getPlayerPosition(name);

						NAPI.Task.Run(() =>
						{
							Anticheat.Wait(p);
							NAPI.Player.SetPlayerArmor(p, (int)Database.getUserArmor(p));
							Database.restoreCustomization(p);
						}, delayTime: 300);

					    NAPI.Task.Run(() =>
					    {
							Anticheat.Wait(p);
							p.Armor = (int)Database.getUserArmor(p);
					    }, delayTime: 500);

						NAPI.Task.Run(() =>
						{
							Anticheat.Wait(p);

							if (Database.isPlayerDeath(name))
								p.Health = 0;

							Anticheat.Wait(p);
						}, delayTime: 4000);

					    NAPI.Task.Run(() =>
					    {
						    Anticheat.Wait(p);

						    Database.getLoadout(p.Name);

					    }, delayTime: 6000);

						p.SetSharedData("PLAYER_NAME", p.Name);
						p.SetSharedData("FRAKTION", Database.getPlayerFraktion(name));
						p.SetSharedData("FRAKTION_SHORT", Functions.getFraktionFromName(p.GetSharedData("FRAKTION")).shortName);
						p.SetSharedData("FRAKTION_RANK", Database.getUserFraktionRank(name));
					    p.SetSharedData("BUSINESS", Database.getPlayerBusiness(name));
					    p.SetSharedData("BUSINESS_RANK", Database.getUserBusinessRank(name));
					    p.SetSharedData("XP", Database.getXP(name));
						NAPI.Task.Run(() =>
						{
							Anticheat.Wait(p);

							Items.Weapons.WeaponSync(p);
						}, 3000);

						int death = 0;

						if (Database.isPlayerDeath(p.Name))
						{
							death = 1;
						}

						p.TriggerEvent("onPlayerLoaded", new object[34]
								{
								p.Name.Split("_")[0],
								p.Name.Split("_")[1],
								Database.getSQLId(p.Name),
								0,
								Database.getPlayerBusiness(p.Name).ToString(),
								0,
								Database.getMoney(p.Name),
								Database.getBlackMoney(p.Name),
								Database.getXP(p.Name),
								Database.getEventPoints(p.Name),
								Database.getWarns(p.SocialClubName),
								0,
								Database.getPlayerFraktion(p.Name),
								0,
								Database.getPlaytime(p.SocialClubName),
								Database.getUserPhoneNumber(p.Name),
								death,
								false,
								false,
								false,
								false,
								false,
								1,
								1,
								0,
								"{ }",
								Database.getPlayerRank(p.Name).rankPermission,
								1,
								1,
								0,
								0,
								0,
								"{ }",
								Database.getFraktionByName(p.GetSharedData("FRAKTION")).fraktionsDimension
						});

					NAPI.Player.SetPlayerArmor(p, (int)Database.getUserArmor(p));
					p.SetClothes(9, 15, 2);
					p.TriggerEvent("componentServerEvent", "Login", "status", "successfully");
					}
					else
					{
						p.TriggerEvent("componentServerEvent", "Login", "status", result);
					}
            
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("aduty:noclip")]
        public void ADuty_NoClip(Client p)
        {
            try
            {
                if (Commands.Commands.isPlayerInAduty(p))
                    p.TriggerEvent("aduty:togglenoclip");
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        /*[RemoteEvent("UpdateCharacterCustomization")]
        public static void UpdateCharacterCustomization(Client p, int price)
        {
            Database.UpdateCharacterCustomization(p, price);
        } */

        private static HeadOverlay CreateHeadOverlay(Byte index, Byte color, Byte secondaryColor, float opacity)
        {
            return new HeadOverlay
            {
                Index = index,
                Color = color,
                SecondaryColor = secondaryColor,
                Opacity = opacity
            };
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Client p, Client killer, uint reason)
        {
            try
            {

                if (killer.Exists && killer != null && killer != p && Other.Paintball.würfelparkPlayers.Contains(killer))
                {
                    killer.Health = 200;
                    killer.Armor = 100;
                    killer.SetData(Other.Paintball.PAINTBALL_KILLS, killer.GetData(Other.Paintball.PAINTBALL_KILLS) + 1);
                    if (killer.GetData(Other.Paintball.PAINTBALL_DEATHS) == 0 || killer.GetData(Other.Paintball.PAINTBALL_KILLS) == 0)
                    {
                        killer.TriggerEvent("updatePaintballScore", killer.GetData(Other.Paintball.PAINTBALL_KILLS), killer.GetData(Other.Paintball.PAINTBALL_DEATHS), 0);
                    }
                    else
                    {
                        killer.TriggerEvent("updatePaintballScore", killer.GetData(Other.Paintball.PAINTBALL_KILLS), killer.GetData(Other.Paintball.PAINTBALL_DEATHS), (int)(killer.GetData(Other.Paintball.PAINTBALL_KILLS) / (int)(killer.GetData(Other.Paintball.PAINTBALL_DEATHS))));
                    }

                    Notification.SendPlayerNotifcation(p, "Du wurdest von " + killer.Name + " getötet.", 5000, "white", "INFORMATION", "white");
                    Notification.SendPlayerNotifcation(killer, "Du hast " + p.Name + " getötet. +750$", 5000, "white", "INFORMATION", "white");
                    Database.changeMoney(Start.loggedInPlayers[killer], 750, false);
                    //Discord.DiscordWebhooks.SendMessage("Spieler hat anderen Spieler getötet.", "Der Spieler " + killer.Name + " hat den Spieler " + p.Name + " getötet. Grund-ID: " + reason, Discord.DiscordWebhooks.killWebhook, "Kill-Log");
                }

                if (Other.Paintball.würfelparkPlayers.Contains(p))
                {
                    p.SetData(Other.Paintball.PAINTBALL_DEATHS, p.GetData(Other.Paintball.PAINTBALL_DEATHS) + 1);
                    if (p.GetData(Other.Paintball.PAINTBALL_DEATHS) == 0 || p.GetData(Other.Paintball.PAINTBALL_KILLS) == 0)
                    {
                        p.TriggerEvent("updatePaintballScore", p.GetData(Other.Paintball.PAINTBALL_KILLS), p.GetData(Other.Paintball.PAINTBALL_DEATHS), 0);
                    }
                    else
                    {
                        p.TriggerEvent("updatePaintballScore", p.GetData(Other.Paintball.PAINTBALL_KILLS), p.GetData(Other.Paintball.PAINTBALL_DEATHS), (int)(p.GetData(Other.Paintball.PAINTBALL_KILLS) / (int)(p.GetData(Other.Paintball.PAINTBALL_DEATHS))));
                    }


                    NAPI.Task.Run(() =>
                    {
                       // p.TriggerEvent("stopScreenEffect", "DeathFailOut");
                        if (p.GetData(Other.Paintball.PAINTBALL_DEATHS) >= 10)
                        {
                            Anticheat.Wait(p);
                            NAPI.Player.SpawnPlayer(p, p.Position);
                            p.TriggerEvent("finishPaintball");
                            Other.Paintball.würfelparkPlayers.Remove(p);
                            p.ResetData(Other.Paintball.PAINTBALL_DEATHS);
                            p.ResetData(Other.Paintball.PAINTBALL_KILLS);
                            Anticheat.Wait(p); p.Position = Other.Paintball.paintballs["Würfelpark"];
                            Notification.SendPlayerNotifcation(p, "Du hast Paintball verlassen, da du zu viele Deaths hattest.", 5000, "white", "INFORMATION", "white");
                            p.Dimension = 0;
                        }
                        else
                        {
                            NAPI.Player.SpawnPlayer(p, Other.Paintball.getRandomSpawnpoint(Other.Paintball.würfelparkPoints));
                            p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_ADVANCEDRIFLE"), 9999);
                            p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_GUSENBERG"), 9999);
                            p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_HEAVYPISTOL"), 9999);
                            p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_ASSAULTRIFLE"), 9999);
                            p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_BULLPUPRIFLE"), 9999);
                            Anticheat.Wait(p); p.Armor = 100;
                        }

                    }, delayTime: 2000);



                }
                else
                {


                    if (!Start.deathTime.ContainsKey(p))
                    {

                        if (killer.Exists && killer != null && killer != p)
                        {
                            if (p.GetSharedData("FRAKTION") != killer.GetSharedData("FRAKTION"))
                            {
                                if (Gangwar.GebietRegister.currentRunningGangwarGebiet != null && p.Dimension == Fraktionen.FraktionRegister.GangwarDimension)
                                {
                                    if (Gangwar.GebietRegister.fraktionOne == Database.getFraktionByName(killer.GetSharedData("FRAKTION")))
                                    {
                                        Gangwar.GebietRegister.pointsFraktionOne = Gangwar.GebietRegister.pointsFraktionOne + 3;
                                        foreach (Client target in NAPI.Pools.GetAllPlayers())
                                        {
                                            if (target.HasSharedData("FRAKTION"))
                                            {
                                                if (target.GetSharedData("FRAKTION") == killer.GetSharedData("FRAKTION"))
                                                {
                                                    Notification.SendPlayerNotifcation(target, "+3 Punkte für das töten eines Spielers", 5000, "white", killer.GetSharedData("FRAKTION"), "white");
                                                }
                                            }
                                        }
                                    }
                                    else if (Gangwar.GebietRegister.fraktionTwo == Database.getFraktionByName(killer.GetSharedData("FRAKTION")))
                                    {
                                        Gangwar.GebietRegister.pointsFraktionTwo = Gangwar.GebietRegister.pointsFraktionTwo + 3;
                                        foreach (Client target in NAPI.Pools.GetAllPlayers())
                                        {
                                            if (target.HasSharedData("FRAKTION"))
                                            {
                                                if (target.GetSharedData("FRAKTION") == killer.GetSharedData("FRAKTION"))
                                                {
                                                    Notification.SendPlayerNotifcation(target, "+3 Punkte für das töten eines Spielers", 5000, "white", killer.GetSharedData("FRAKTION"), "");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //Discord.DiscordWebhooks.SendMessage("Spieler hat anderen Spieler getötet.", "Der Spieler " + killer.Name + " hat den Spieler " + p.Name + " getötet. Grund-ID: " + reason, Discord.DiscordWebhooks.killWebhook, "Kill-Log");
                        }
                        else
                        {
                            //Discord.DiscordWebhooks.SendMessage("Spieler ist gestorben.", "Der Spieler " + p.Name + " ist gestorben. Grund-ID: " + reason, Discord.DiscordWebhooks.killWebhook, "Kill-Log");
                        }
                    }

                    try
                    {
                        if (Gangwar.GebietRegister.fraktionOne == Database.getFraktionByName(p.GetSharedData("FRAKTION")) || Gangwar.GebietRegister.fraktionTwo == Database.getFraktionByName(p.GetSharedData("FRAKTION")))
                        {
                            Start.deathTime.Add(p, (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds + 60);
                        }
                        else
                        {
                            Start.deathTime.Add(p, 480000);
                        }
                    }
                    catch (Exception ex) { Log.Write(ex.Message); }
                    NAPI.Player.SpawnPlayer(p, p.Position);
                    p.RemoveAllWeapons();
                    p.StopAnimation();
                    p.Eval("mp.events.callRemote('server:leaveradio');");

                    Functions.setPlayerOnGround(p);


                    NAPI.Task.Run(delegate
                    {
						// p.SendNotification("12");
						p.TriggerEvent("startScreenEffect", new object[3]
                        {
						"DeathFailOut",
						480000,
						false
                         });
						NAPI.Task.Run(delegate
						{
							NAPI.Player.PlayPlayerAnimation(p, 33, "combat@damage@rb_writhe", "rb_writhe_loop", 8f);
							p.TriggerEvent("setInvincible", true);
							p.TriggerEvent("updateInjured", true);
							Functions.disableAllPlayerControls(p, true);
							Database.setDeathStatus(p.Name, true);
						} , 6 * 6000);
						p.TriggerEvent("setInvincible", true);
                        Functions.disableAllPlayerControls(p, true);

                        //NAPI.Player.PlayPlayerAnimation(p, 33, "missarmenian2", "corpse_search_exit_ped", 8f);

                        NAPI.Player.PlayPlayerAnimation(p, 33, "combat@damage@rb_writhe", "rb_writhe_loop", 8f);

                        Database.setDeathStatus(p.Name, true);

                    }, delayTime: 800);


                }

            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void PlayerEnterVehicle(Client p, Vehicle veh, sbyte seatId)
        {
            if (veh.Locked)
                p.StopAnimation();

			veh.SetSharedData(Vehicles.VehicleData.VEHICLE_FUEL_STATUS, (int)Database.getVehicleFuel(veh.NumberPlate));
			veh.SetSharedData(Vehicles.VehicleData.VEHICLE_MILEAGE_STATUS, (int)Database.getVehicleDistance(veh.NumberPlate));
        }

    }
}
