using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Routen
{
	class Meth : Script
	{
		public static List<Client> farming = new List<Client>();
		public static List<Client> processing = new List<Client>();

		public static Timer OnFarmingSpentTimer;
		public static Timer OnProcessingSpentTimer;

		[ServerEvent(Event.ResourceStart)]
		public void ResourceStart()
		{
			NAPI.Marker.CreateMarker(1, new Vector3(-2306.372, 2555.979, 0.2602897), new Vector3(), new Vector3(), 160f, new Color(155, 255, 0), false, 0);
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(-2306.372, 2555.979, 0.2602897), 160f, 40f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming3", "farmer", "Ephidrin"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Kröten zu sammeln", "FARMING", "orange", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(-86.95163, 1880.298, 196.2199), 2.5f, 2f, 0);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeProcessing3", "processing", "Ephidrin"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Kröten zu Häuten zu verarbeiten", "FARMING", "orange", 4500));

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

		[RemoteEvent("changeFarming3")]
		public void changeFarmingCannabis(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Ephidrin")
					{
						if (arg1 == "farmer")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu farmen...", 3500, "orange", "farming", "orange");
								Routen.Meth.farming.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu farmen...", 3500, "orange", "farming", "orange");
								Routen.Meth.farming.Add(p);
								p.TriggerEvent("disableAllPlayerActions", true);
								p.SetData("IS_FARMING", true);
							}
						}
					}
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du bist in einem Fahrzeug", 4500, "orange", "farming", "");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("changeProcessing3")]
		public void changeProcessingCannabis(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Ephidrin")
					{
						if (arg1 == "processing")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Meth.processing.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu verarbeiten...", 3500, "orange", "farming", "orange");
								Routen.Meth.processing.Add(p);
								p.TriggerEvent("disableAllPlayerActions", true);
								p.SetData("IS_FARMING", true);
							}
						}
					}
				}
				else
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
						NAPI.Task.Run(delegate
						{
							Database.changeInventoryItem(p.Name, "Kröten", 25, false);
							Notification.SendPlayerNotifcation(p, "+ 25 Kröten", 3000, "orange", "farming", "orange");
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
						if (Database.getItemCount(p.Name, "Kroeten") > 50)
						{
							p.SetData("IS_FARMING", true);
							Database.changeInventoryItem(p.Name, "Kroeten", 50, true);
							Database.changeInventoryItem(p.Name, "Gehäutete_Kröten", 12, false);
							Notification.SendPlayerNotifcation(p, "+12 Gehäutete Kröten", 3000, "orange", "farming", "orange");
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Du hast zu wenig Kröten dabei.", 3500, "orange", "farming", "orange");
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
