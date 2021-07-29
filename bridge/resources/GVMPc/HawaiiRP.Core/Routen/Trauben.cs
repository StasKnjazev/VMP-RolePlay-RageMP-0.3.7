using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Routen
{
	class Trauben : Script
	{
		public static List<Client> farming = new List<Client>();
		public static List<Client> processing = new List<Client>();

		public static Timer OnFarmingSpentTimer;
		public static Timer OnProcessingSpentTimer;

		[ServerEvent(Event.ResourceStart)]
		public void ResourceStart()
		{

			NAPI.Blip.CreateBlip(238, new Vector3(355.1094, 6524.974, 27.31252), 1f, 7, "Trauben Farm", 255, 0, true, 0, 0);
			NAPI.Blip.CreateBlip(499, new Vector3(-1043.928, 5327.196, 43.48028), 1f, 7, "Trauben Verarbeiter", 255, 0, true, 0, 0);

			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(355.1094, 6524.974, 27.31252), 40f, 3f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming4", "farmer", "Trauben"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Trauben zu sammeln", "FARMING", "purple", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(-1043.928, 5327.196, 43.48028), 5f, 2f, 0);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeProcessing4", "processing", "Trauben"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Trauben zu verarbeiten", "FARMING", "purple", 4500));

			OnFarmingSpentTimer = new Timer(11000.0);
			OnFarmingSpentTimer.AutoReset = true;
			OnFarmingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnFarmingSpent(null);
			};
			OnFarmingSpentTimer.Start();

			OnProcessingSpentTimer = new Timer(6000.0);
			OnProcessingSpentTimer.AutoReset = true;
			OnProcessingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnProcessingSpent(null);
			};
			OnProcessingSpentTimer.Start();

		}

		[RemoteEvent("changeFarming4")]
		public void changeFarmingTrauben(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Trauben")
					{
						if (arg1 == "farmer")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu farmen...", 3500, "purple", "FARMING", "");
								Routen.Trauben.farming.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu farmen...", 3500, "purple", "FARMING", "");
								Routen.Trauben.farming.Add(p);
								p.TriggerEvent("disableAllPlayerActions", true);
								p.SetData("IS_FARMING", true);
							}
						}
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist in einem Fahrzeug", 4500, "orange", "farming", "");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("changeProcessing4")]
		public void changeProcessingOrange(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Trauben")
					{
						if (arg1 == "processing")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu verarbeiten...", 3500, "purple", "FARMING", "");
								Routen.Trauben.processing.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu verarbeiten...", 3500, "purple", "FARMING", "");
								Routen.Trauben.processing.Add(p);
								p.TriggerEvent("disableAllPlayerActions", true);
								p.SetData("IS_FARMING", true);
							}
						}
					}

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist in einem Fahrzeug", 4500, "orange", "farming", "");
				}

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		public static void OnFarmingSpent(object unused)
		{
			try
			{
				foreach (Client p in farming.ToArray())
				{
					if (NAPI.Pools.GetAllPlayers().Contains(p))
					{
						p.SetData("IS_FARMING", true);
						NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
						int count = new Random().Next(5, 10);
						NAPI.Task.Run(delegate
						{
							Database.changeInventoryItem(p.Name, "Trauben", count, false);
							Notification.SendPlayerNotifcation(p, "+ " + count + " Trauben", 3000, "purple", "FARMING", "");
						}, 10000);
					}
					else
					{
						if (farming.Contains(p))
							farming.Remove(p);
					}
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		public static void OnProcessingSpent(object unused)
		{
			try
			{
				foreach (Client p in processing.ToArray())
				{
					if (NAPI.Pools.GetAllPlayers().Contains(p))
					{
						if (Database.getItemCount(p.Name, "Trauben") > 50)
						{
							p.SetData("IS_FARMING", true);
							Database.changeInventoryItem(p.Name, "Traubensaft", 12, false);
							Database.changeInventoryItem(p.Name, "Trauben", 50, true);
							Notification.SendPlayerNotifcation(p, "+12 Trauben Saft", 3000, "purple", "FARMING", "");
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Du hast zu wenig Trauben dabei.", 3500, "purple", "FARMING", "");
							NAPI.Player.StopPlayerAnimation(p);
							p.TriggerEvent("disableAllPlayerActions", false);
							p.SetData("IS_FARMING", false);
							if (processing.Contains(p))
								processing.Remove(p);
						}
					}
					else
					{
						processing.Remove(p);
					}
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

	}
}
