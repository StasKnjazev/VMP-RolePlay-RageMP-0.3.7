using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Routen
{
	class Kokain : Script
	{
		public static List<Client> farming = new List<Client>();
		public static List<Client> processing = new List<Client>();

		public static Timer OnFarmingSpentTimer;
		public static Timer OnProcessingSpentTimer;

		[ServerEvent(Event.ResourceStart)]
		public void ResourceStart()
		{

			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(3821.759, 4442.816, 1.703498), 5f, 3f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming7", "farmer", "Kokain"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Kokainblätter zu sammeln", "FARMING", "orange", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(260.1346, 2581.846, 43.86367), 2f, 2f, 0);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeProcessing7", "processing", "Kokain"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Kokainblätter zu verarbeiten", "FARMING", "orange", 4500));

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

		[RemoteEvent("changeFarming7")]
		public void changeFarmingKokain(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Kokain")
					{
						if (arg1 == "farmer")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu farmen...", 3500, "orange", "farming", "orange");
								Routen.Kokain.farming.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu farmen...", 3500, "orange", "farming", "orange");
								Routen.Kokain.farming.Add(p);
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

		[RemoteEvent("changeProcessing7")]
		public void changeProcessingKokain(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Kokain")
					{
						if (arg1 == "processing")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Kokain.processing.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Kokain.processing.Add(p);
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
						int count = new Random().Next(1, 2);
						NAPI.Task.Run(delegate
						{
							Database.changeInventoryItem(p.Name, "Kokainblätter", count, false);
							Notification.SendPlayerNotifcation(p, "+ " + count + " Kokainblätter", 3000, "orange", "farming", "orange");
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
						if (Database.getItemCount(p.Name, "Kokainblätter") > 10)
						{
							p.SetData("IS_FARMING", true);
							Database.changeInventoryItem(p.Name, "Kokain", 1, false);
							Database.changeInventoryItem(p.Name, "Kokainblätter", 50, true);
							Notification.SendPlayerNotifcation(p, "+1 Kokain", 3000, "orange", "farming", "orange");
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Du hast zu wenig Kokainblätter dabei.", 3500, "orange", "farming", "orange");
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
