using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Routen
{
	class Westen : Script
	{
		public static List<Client> farming = new List<Client>();
		public static List<Client> processing = new List<Client>();
		public static List<Client> processing2 = new List<Client>();

		public static Timer OnFarmingSpentTimer;
		public static Timer OnProcessingSpentTimer;
		public static Timer OnFinishingSpentTimer;

		[ServerEvent(Event.ResourceStart)]
		public void ResourceStart()
		{
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(144.5813, 1015.57, 225.5598), 60f, 6f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming6", "farmer", "Aramid"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Aramidfasern zu sammeln", "FARMING", "grey", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(707.4152, -965.3637, 29.31282), 5f, 2f, 0);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeProcessing6", "processing", "Kevlar"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Aramidfasern zu verarbeiten", "FARMING", "grey", 4500));
			ColShape val3 = NAPI.ColShape.CreateCylinderColShape(new Vector3(744.3871, -539.407, 26.66805), 5f, 2f, 0);
			val3.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeProcessing6", "processing2", "Kevlar"));
			val3.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Kevlar zu verarbeiten", "FARMING", "grey", 4500));

			OnFarmingSpentTimer = new Timer(11000.0);
			OnFarmingSpentTimer.AutoReset = true;
			OnFarmingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnFarmingSpent(null);
			};
			OnFarmingSpentTimer.Start();

			OnProcessingSpentTimer = new Timer(9000.0);
			OnProcessingSpentTimer.AutoReset = true;
			OnProcessingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnProcessingSpent(null);
			};
			OnProcessingSpentTimer.Start();

			OnFinishingSpentTimer = new Timer(9000.0);
			OnFinishingSpentTimer.AutoReset = true;
			OnFinishingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnFinishingSpent(null);
			};
			OnFinishingSpentTimer.Start();

		}

		[RemoteEvent("changeFarming6")]
		public void changeFarmingKevlar(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Aramid")
					{
						if (arg1 == "farmer")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu farmen...", 3500, "orange", "farming", "orange");
								Routen.Westen.farming.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu farmen...", 3500, "orange", "farming", "orange");
								Routen.Westen.farming.Add(p);
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

		[RemoteEvent("changeProcessing6")]
		public void changeProcessingKevlar(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Kevlar")
					{
						if (arg1 == "processing")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Westen.processing.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								Notification.SendPlayerNotifcation(p, "Du fängst an zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Westen.processing.Add(p);
								p.TriggerEvent("sendProgressbar", new object[1]
								{
								9000.0
								});
								p.TriggerEvent("disableAllPlayerActions", true);
								p.SetData("IS_FARMING", true);
							}
						}
						else if (arg1 == "processing2")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Westen.processing2.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Westen.processing2.Add(p);
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
							Database.changeInventoryItem(p.Name, "Aramidfaser", count, false);
							Notification.SendPlayerNotifcation(p, "+ " + count + " Aramidfaser", 3000, "orange", "farming", "orange");
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
						if (Database.getItemCount(p.Name, "Aramidfaser") > 99)
						{
							p.SetData("IS_FARMING", true);
							Database.changeInventoryItem(p.Name, "Kevlar", 1, false);
							Database.changeInventoryItem(p.Name, "Aramidfaser", 100, true);
							Notification.SendPlayerNotifcation(p, "+1 Kevlar", 3000, "grey", "farming", "");
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Du hast zu wenig Aramidfasern dabei.", 3500, "grey", "farming", "");
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

		public static void OnFinishingSpent(object unused)
		{
			try
			{
				foreach(Client p in processing2.ToArray())
				{
					if(NAPI.Pools.GetAllPlayers().Contains(p))
					{
						if (Database.getItemCount(p.Name, "Kevlar") > 9)
						{
							p.SetData("IS_FARMING", true);
							Database.changeInventoryItem(p.Name, "Schutzweste", 1, false);
							Database.changeInventoryItem(p.Name, "Kevlar", 5, true);
							Notification.SendPlayerNotifcation(p, "+1 Schutzweste", 3000, "grey", "farming", "");
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Du hast zu wenig Kevlar dabei.", 3500, "grey", "farming", "");
							NAPI.Player.StopPlayerAnimation(p);
							p.TriggerEvent("disableAllPlayerActions", false);
							p.SetData("IS_FARMING", false);
							if (processing2.Contains(p))
								processing2.Remove(p);
						}
					} else
					{
						processing2.Remove(p);
					}
				}
			} catch(Exception ex) { Log.Write(ex.Message); }
		}

	}
}
