using GTANetworkAPI;
using GVMPc.Menus;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Timers;
using GVMPc.Handy;
using GVMPc.Banking;
using GVMPc.ClothingShops;
using GVMPc.Vehicles.Garages;

namespace GVMPc
{
    class Start : Script
    {
        public static DateTime startTime;

        public static readonly int port = 3306;
        public static string connectionString;
        public static Dictionary<Client, string> loggedInPlayers = new Dictionary<Client, string>();
        public static Dictionary<Client, int> deathTime = new Dictionary<Client, int>();
        public static Timer OnMinuteSpentTimer;
        public static Timer OnHourSpentTimer;
		public static Timer OnHalfHourTimer;
        public static Timer OnSecondSpentTimer;
        public static bool ReleaseBuild = true;

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            Daten.setDatabaseData();
			Log.Write("Bank | " + Bank.points.Count + " Banken geladen");
			Log.Write("Kleidungsladen | " + ClothingShopRegister.clothingshopList.Count + " Kleidungsläden geladen");
			Log.Write("Dealer | 4 Dealer geladen");
			Log.Write("Dokumentfälscherei | Erfolgreich geladen");
			Log.Write("Events | Event Dealer geladen");
			Log.Write("Events | Events geladen");
			Log.Write("Fraktion | " + Fraktionen.FraktionRegister.fraktionList.Count + " Fraktionen geladen");
			Log.Write("Fraktion | " + Fraktionen.FraktionsVehicles.list.Count + " Fraktionsfahrzeuge geladen");
			Log.Write("Gangwar | " + Gangwar.GebietRegister.gebietList.Count + " Gebiete geladen");
			Log.Write("Items | " + Items.ItemManager.itemRegisterList.Count + " Items geladen");
			Log.Write("Jail | Jail geladen");
			Log.Write("Jobs | 4 Jobs geladen");
			Log.Write("Blips | Blips geladen");
			Log.Write("Routen | Routen geladen");
			Log.Write("Shops | " + Shops.ShopRegister.shopList.Count + " Shops geladen");
			Log.Write("Tankstellen | " + Tankstellen.TankstellenRegister.tankstellen.Count + " Tankstellen geladen");
			Log.Write("Tattoo | " + Tattoo.TattooRegister.points.Count + " Tattooläden geladen");
			Log.Write("Tuner | Tuner geladen");
			Log.Write("Garagen | Garagen geladen");
			Log.Write("Fahrzeugshops | " + Vehicles.Shops.AutoShopRegister.autoshopList.Count + " Autoshops geladen");
			Log.Write("Hawaii Roleplay um " + DateTime.Now + " gestartet");
            Log.Write("");
            Log.Write("");
            Log.Write(" ▄         ▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄         ▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ");
            Log.Write("▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌");
            Log.Write("▐░▌       ▐░▌▐░█▀▀▀▀▀▀▀█░▌▐░▌       ▐░▌▐░█▀▀▀▀▀▀▀█░▌ ▀▀▀▀█░█▀▀▀▀  ▀▀▀▀█░█▀▀▀▀ ");
            Log.Write("▐░▌       ▐░▌▐░▌       ▐░▌▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌          ▐░▌     ");
            Log.Write("▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄█░▌▐░▌   ▄   ▐░▌▐░█▄▄▄▄▄▄▄█░▌     ▐░▌          ▐░▌     ");
            Log.Write("▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌  ▐░▌  ▐░▌▐░░░░░░░░░░░▌     ▐░▌          ▐░▌     ");
            Log.Write("▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌▐░▌ ▐░▌░▌ ▐░▌▐░█▀▀▀▀▀▀▀█░▌     ▐░▌          ▐░▌     ");
            Log.Write("▐░▌       ▐░▌▐░▌       ▐░▌▐░▌▐░▌ ▐░▌▐░▌▐░▌       ▐░▌     ▐░▌          ▐░▌     ");
            Log.Write("▐░▌       ▐░▌▐░▌       ▐░▌▐░▌░▌   ▐░▐░▌▐░▌       ▐░▌ ▄▄▄▄█░█▄▄▄▄  ▄▄▄▄█░█▄▄▄▄ ");
            Log.Write("▐░▌       ▐░▌▐░▌       ▐░▌▐░░▌     ▐░░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌");
            Log.Write(" ▀         ▀  ▀         ▀  ▀▀       ▀▀  ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀ ");
            Log.Write("");
            Log.Write("");
            //Discord.SendServerStatusLog("Server Status", "Der Game Server wurde um " + DateTime.Now + " gestartet", Discord.COLORS.CYAN, Webhook.STATUS);
            NAPI.Server.SetAutoRespawnAfterDeath(false);
            NAPI.Server.SetCommandErrorMessage(" ");
            NAPI.Server.SetGlobalServerChat(false);
            NAPI.Server.SetAutoSpawnOnConnect(false);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            connectionString = "Server=" + Daten.host + "; Database=" + Daten.database + "; UID=" + Daten.username + "; PASSWORD=" + Daten.password;

            startTime = DateTime.Now;

            OnMinuteSpentTimer = new Timer(60000.0);
            OnMinuteSpentTimer.AutoReset = true;
            OnMinuteSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                OnMinuteSpent(null);
            };
            OnMinuteSpentTimer.Start();

			OnHourSpentTimer = new Timer(60 * 60000);
            OnHourSpentTimer.AutoReset = true;
            OnHourSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                OnHourSpend(null);
            };
            OnHourSpentTimer.Start();

			OnHalfHourTimer = new Timer(30 * 60000);
			OnHalfHourTimer.AutoReset = true;
			OnHalfHourTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnHalfHourSpent(null);
			};
			OnHalfHourTimer.Start();

			OnSecondSpentTimer = new Timer(1000.0);
            OnSecondSpentTimer.AutoReset = true;
            OnSecondSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                OnSecondSpent(null);
            };
            OnSecondSpentTimer.Start();


            Players.AdminRanks.RegisterAdmins();

            foreach(Client p in NAPI.Pools.GetAllPlayers())
            {
                PlayerEvents.OnPlayerConnected(p);
            }

            Database.parkAllVehicles();
			

        }

        public void OnSecondSpent(object unused)
        {
            try
            {

                foreach (String name in Functions.handcuffed)
                {
                    Client p = Database.getPlayerFromName(name);
                    if (p != null)
                    {
                        NAPI.Player.SetPlayerCurrentWeapon(p, WeaponHash.Unarmed);
                    }
                }

				foreach(KeyValuePair<Client, string> item in loggedInPlayers)
				{
					if(NAPI.Pools.GetAllPlayers().Contains(item.Key))
					{
						if(Database.getXP(Start.loggedInPlayers[item.Key]) > 199)
						{
							Database.changeUserXP(item.Key.Name, 200, true);
							Database.changePlaytime(item.Key.SocialClubName, 1, false);
						}
					}
				}
                foreach (KeyValuePair<Client, int> p in Start.deathTime)
                {
                    if (NAPI.Pools.GetAllPlayers().Contains(p.Key))
                    {
                        p.Key.Health = 200;
                    }
                }

                int seconds = DateTime.Now.Second;
                int minutes = DateTime.Now.Minute;
                int hours = DateTime.Now.Hour;
                NAPI.World.SetTime(hours, minutes, seconds);

                foreach (KeyValuePair<Client, int> deathPlayer in deathTime)
                {
                    if (!NAPI.Pools.GetAllPlayers().Contains(deathPlayer.Key) || !Start.loggedInPlayers.ContainsKey(deathPlayer.Key))
                    {
                        deathTime.Remove(deathPlayer.Key);
                        return;
                    }

					int ms = 40000;
                    deathPlayer.Key.Health = 200;

                    if (deathPlayer.Value < ms)
                    {
                        Database.setDeathStatus(Start.loggedInPlayers[deathPlayer.Key], false);
                        Notification.SendPlayerNotifcation(deathPlayer.Key, "Du wurdest wiederbelebt.", 5000, "white", "INFORMATION", "white");
                        deathTime.Remove(deathPlayer.Key);
                        NAPI.Player.SpawnPlayer(deathPlayer.Key, new Vector3(-1038.069, -2737.8801, 19.069271));
                        deathPlayer.Key.StopAnimation();
						NAPI.Player.StopPlayerAnimation(deathPlayer.Key);
                        deathPlayer.Key.TriggerEvent("stopScreenEffect", "DeathFailOut");
                        deathPlayer.Key.TriggerEvent("setInvincible", false);
                        Functions.disableAllPlayerControls(deathPlayer.Key, false);
                        deathPlayer.Key.Health = 200;
                        deathPlayer.Key.Armor = 0;

                        if (deathPlayer.Key.Dimension != Fraktionen.FraktionRegister.GangwarDimension)
                        {
                            Database.removeAllItems(Start.loggedInPlayers[deathPlayer.Key]);
                            deathPlayer.Key.RemoveAllWeapons();
                            foreach(WeaponHash weaponHash in Database.getLoadout(deathPlayer.Key.Name))
                            {
                                Database.removeLoadout(deathPlayer.Key.Name, weaponHash);
                            }
                        }
                    }
                    else
                    {
                        Functions.disableAllPlayerControls(deathPlayer.Key, true);
                    }
                }
            } catch (Exception ex) { Log.Write(ex.Message); }

        }

		[RemoteEvent("m")]
        public static void nativeMenu(Client p, string id)
        {
            try
            {
                if (id == null)
                    return;

                if (!(id == "NaN"))
                {
                    NativeMenu nativeMenu = p.GetData("PLAYER_CURRENT_NATIVEMENU");
                    if (nativeMenu != null)
                    {
                        p.Eval("mp.events.callRemote('nM-" + nativeMenu.Title + "', '" + nativeMenu.Items[Convert.ToInt32(id)].selectionName + "');");
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("Pressed_H")]
        public static void handsUp(Client p)
        {
            try
            {
				if (p.IsInVehicle)
					return;

                if (deathTime.ContainsKey(p))
                    return;


                if (p.HasData("handhoch"))
                {
                    NAPI.Player.PlayPlayerAnimation(p, 49, "mp_am_hold_up", "handsup_base");
                    p.ResetData("handhoch");
                }
                else
                {
                    NAPI.Player.StopPlayerAnimation(p);
                    p.SetData("handhoch", true);
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		[RemoteEvent("Pressed_J")]
		public static void salute(Client p)
		{
			try
			{
				if (p.IsInVehicle)
					return;

				if (deathTime.ContainsKey(p))
					return;


				if (p.HasData("salute"))
				{ 
					NAPI.Player.PlayPlayerAnimation(p, 49, "anim@mp_player_intincarsalutestd@ds@", "idle_a");
					p.ResetData("salute");
				} else
				{
					NAPI.Player.StopPlayerAnimation(p);
					p.SetData("salute", true);
				}
			} catch (Exception ex) { Log.Write(ex.Message); }
		}

        public static void OnMinuteSpent(object unused)
        {
            try
            {

                NativeManager.PosCheck();

                foreach (KeyValuePair<Client, string> item in loggedInPlayers)
                {

                    if (NAPI.Pools.GetAllPlayers().Contains(item.Key))
                    {
						Database.setUserArmor(item.Key, item.Key.Armor);
                        if(item.Key.Armor < 1)
                        {
                            item.Key.SetClothes(9, 0, 0);
                        }
                        Database.SavePlayer(item.Key);
                        item.Key.SetSharedData("FRAKTION", Database.getPlayerFraktion(item.Key.Name));
                        item.Key.SetSharedData("FRAKTION_SHORT", Functions.getFraktionFromName(item.Key.GetSharedData("FRAKTION")).shortName);
                        item.Key.SetSharedData("FRAKTION_RANK", Database.getUserFraktionRank(item.Key.Name));
						item.Key.SetSharedData("XP", Database.getXP(item.Key.Name));
						item.Key.SetSharedData("BUSINESS", Database.getPlayerBusiness(item.Key.Name));
						item.Key.SetSharedData("BUSINESS_RANK", Database.getUserBusinessRank(item.Key.Name));
						item.Key.SetSharedData("JOB", Database.getPlayerJob(item.Key.Name));
						item.Key.SetSharedData("SPIND_ITEMS", Database.getSpindItems(item.Key.Name));
						item.Key.SetSharedData("SPIND", Database.getPlayerSpind(item.Key.Name));
						item.Key.SetSharedData("NUMBER", Database.getUserPhoneNumber(item.Key.Name));
                        item.Key.SetSharedData("PLAYER_RANK", Database.getPlayerRank2(item.Key.Name));
                        item.Key.TriggerEvent("updateFraktion", item.Key.GetSharedData("FRAKTION"));
                    }
                    else
                    {
                        loggedInPlayers.Remove(item.Key);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }

		public static void OnHalfHourSpent(object unused)
		{
			try
			{
				foreach(KeyValuePair<Client, string> item in loggedInPlayers)
				{
					if (NAPI.Pools.GetAllPlayers().Contains(item.Key))
					{
						int tax = (int)Database.getVehicleTaxFromOwner(item.Key.Name);
						Database.changeMoney(item.Key.Name, tax, true);
						Notification.SendPlayerNotifcation(item.Key, "Deine Fahrzeugsteuern wurden dir abgezogen " + tax + "", 5000, "green", "STEUERN", "");
						Notification.SendGlobalNotification("In 30 Minuten werden alle Fahrzeuge eingeparkt", 6500, "yellow", Notification.icon.glob);
					}
				}


			} catch (Exception ex)
			{
				Log.Write(ex.Message + ex.StackTrace);
			}
		} 

        public static void OnHourSpend(object unused)
        {
            try
			{
				Notification.SendGlobalNotification("Alle Fahrzeuge werden jetzt eingeparkt", 6500, "yellow", Notification.icon.dev);

                foreach (KeyValuePair<Client, string> item in loggedInPlayers)
                {
					if (NAPI.Pools.GetAllPlayers().Contains(item.Key))
                    {
						if (item.Key.GetSharedData("FRAKTION") == "Los Santos Police Department")
						{
							Notification.SendPlayerNotifcation(item.Key, "Dein Gehalt ist auf dein Konto überwiesen worden", 5000, "green", "BANK", "green");
							Database.changeUserBank(loggedInPlayers[item.Key], 10000, false);
							Database.changePlaytime(item.Key.SocialClubName, 1, false);
							Notification.SendPlayerNotifcation(item.Key, "Du bist in Level aufgestiegen! Dein Aktuelles Level: " + Database.getPlaytime(item.Key.SocialClubName), 4500, "white", "VISUM", "grey");
						}
						else
						{
							Notification.SendPlayerNotifcation(item.Key, "Dein Gehalt ist auf dein Konto überwiesen worden", 5000, "green", "BANK", "green");
							Database.changeUserBank(loggedInPlayers[item.Key], 5000, false);
							int tax = (int)Database.getVehicleTaxFromOwner(item.Key.Name);
							Database.changeMoney(item.Key.Name, tax, true);
							Notification.SendPlayerNotifcation(item.Key, "Deine Fahrzeugsteuern wurden dir abgezogen " + tax + "", 5000, "green", "STEUERN", "");
							Database.changePlaytime(item.Key.SocialClubName, 1, false);
							Notification.SendPlayerNotifcation(item.Key, "Du bist in Level aufgestiegen! Dein Aktuelles Level: " + Database.getPlaytime(item.Key.SocialClubName), 4500, "white", "VISUM", "grey");

						}
                    }
                    else
                    {
                        loggedInPlayers.Remove(item.Key);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }




    }
}
