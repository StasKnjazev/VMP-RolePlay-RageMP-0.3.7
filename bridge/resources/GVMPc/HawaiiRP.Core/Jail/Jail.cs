using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.ClothingShops;
using System.Timers;

namespace GVMPc.Jail
{
	public class Jail : Script
	{
		public static int jailtime = 0;
		public static Vector3 jail = new Vector3(1690.793, 2591.143, 44.81374);
		public static Vector3 prisonIn = new Vector3(1691.46, 2565.555, 44.46485);
		public static Vector3 prisonOut = new Vector3(1845.698, 2585.883, 44.57205);


		public static List<ClothingModel> jailClothes = new List<ClothingModel>();


		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(188, new Vector3(1846.464, 2603.645, 44.48911), 1.0f, 1, "Boilingbroke Jail", 255, 0, true, 0, 0);
		}

		[Command("jail")]
		public static void CMD_JAIL(Client p, string name, int jailtime)
		{
			try
			{
				if(Database.isPlayerInFrak(p, "Los Santos Police Department"))
				{
					if(p.Position.DistanceTo(jail) < 10f)
					{
						Client client = Database.getPlayerFromName(name);
						Database.updateJailtime(name, jailtime);
						Clothing.PlayerClothes.setClothes(client, 11, 0, 4);
						Clothing.PlayerClothes.setClothes(client, 4, 7, 15);
						Clothing.PlayerClothes.setClothes(client, 6, 7, 1);
						foreach(Client c in NAPI.Pools.GetAllPlayers())
						{
							if(Database.isPlayerInFrak(c, "Los Santos Police Department"))
							{
								Notification.SendPlayerNotifcation(c, "Der Spieler: " + name + " wurde für " + jailtime + " inhaftiert", 4500, "blue", "Police", "");
							}
						}
						Functions.handcuffed.Remove(client.Name);
						NAPI.Task.Run(delegate
						{
							Database.updateJailtime(name, 0);
							Notification.SendPlayerNotifcation(client, "Du bist aus dem Gefängnis befreit", 4500, "blue", "Police", "");
							Anticheat.Wait(client); client.Position = prisonOut.Add(new Vector3(0, 0, 1.5));

						}, jailtime * 60000);
						Anticheat.Wait(client); client.Position = prisonIn.Add(new Vector3(0, 0, 1.5));
						Notification.SendPlayerNotifcation(client, "Du hast " + jailtime + " bekommen", 4500, "blue", "Police", "");
					}

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung dazu", 4500, "blue", "Police", "");
				}


			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[Command("unjail")]
		public void CMD_UNJAIL(Client p, string name)
		{
			try
			{
				if(Database.isPlayerInFrak(p, "Los Santos Police Department"))
				{
					Client client = Database.getPlayerFromName(name);

					Anticheat.Wait(client); client.Position = prisonOut.Add(new Vector3(0, 0, 1.5));
					Notification.SendPlayerNotifcation(client, "Du wurdest von " + p.Name +" vorzeitig entlassen", 4500, "blue", "Police", "");
					Notification.SendPlayerNotifcation(p, "Du hast " + name + " vorzeitig entlassen", 4500, "blue", "Police", "");
					Database.updateJailtime(client.Name, 0);

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast keine Berechtigung dazu", 4500, "blue", "Gefaengniss", "");
				}

			} catch(Exception ex)
			{
				Log.Write("Error while unjail Player: " + ex.Message);
			}
		}

		[Command("jailtime")]
		public void CMD_JAILTIME(Client p)
		{
			try
			{
				if(Database.getUserJailtime(p.Name) > 0)
				Notification.SendPlayerNotifcation(p, "Du sitzt noch " + Database.getUserJailtime(p.Name) + " im Gefängnis", 4500, "blue", "JAIL", "");

			}catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		public static void getJailedPlayer(Client p)
		{
			if(Database.getUserJailtime(p.Name) > 0)
			{
				CMD_JAIL(p, p.Name, (int)Database.getUserJailtime(p.Name));
			}
		}
	}
}
