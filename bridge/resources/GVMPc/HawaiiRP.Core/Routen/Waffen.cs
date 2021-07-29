using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Routen
{
	class Waffen : Script
	{
		public static List<Client> farming = new List<Client>();
		public static List<Client> processing = new List<Client>();

		public static Timer OnFarmingSpentTimer;
		public static Timer OnProcessingSpentTimer;

		[ServerEvent(Event.ResourceStart)]
		public void ResourceStart()
		{
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(-179.3972, 6153.733, 30.10637), 2f, 3f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming5", "farmer", "Waffenteile"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Waffenteile zu sammeln", "FARMING", "grey", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(-472.4484, 6287.462, 12.51341), 5f, 2f, 0);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeProcessing5", "processing", "Waffenteile"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Waffenteile zu verarbeiten", "FARMING", "grey", 4500));

			OnFarmingSpentTimer = new Timer(4 * 6000);
			OnFarmingSpentTimer.AutoReset = true;
			OnFarmingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnFarmingSpent(null);
			};
			OnFarmingSpentTimer.Start();

			OnProcessingSpentTimer = new Timer(2 * 6000);
			OnProcessingSpentTimer.AutoReset = true;
			OnProcessingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnProcessingSpent(null);
			};
			OnProcessingSpentTimer.Start();

		}

		[RemoteEvent("changeFarming5")]
		public void changeFarmingWaffenteile(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Waffenteile")
					{
						if (arg1 == "farmer")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu farmen...", 3500, "grey", "farming", "grey");
								Routen.Waffen.farming.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu farmen...", 3500, "grey", "farming", "grey");
								Routen.Waffen.farming.Add(p);
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

		[RemoteEvent("changeProcessing5")]
		public void changeProcessingWaffen(Client p, string arg1, string arg2)
		{
			if (arg1 == null || arg2 == null)
				return;

			try
			{
				if (!p.IsInVehicle)
				{

					if (arg2 == "Waffenteile")
					{
						if (arg1 == "processing")
						{
							if (p.GetData("IS_FARMING"))
							{
								Notification.SendPlayerNotifcation(p, "Du hörst auf zu verarbeiten...", 3500, "grey", "farming", "grey");
								Routen.Waffen.processing.Remove(p);
								p.SetData("IS_FARMING", false);
								NAPI.Player.StopPlayerAnimation(p);
								p.TriggerEvent("disableAllPlayerActions", false);
							}
							else
							{
								NAPI.Player.StopPlayerAnimation(p);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								Notification.SendPlayerNotifcation(p, "Du fängst an zu verarbeiten...", 3500, "grey", "farming", "grey");
								Routen.Waffen.processing.Add(p);
								p.TriggerEvent("disableAllPlayerActions", true);
								p.SetData("IS_FARMING", true);
							}
						}
					}
				}  else
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
						int count = new Random().Next(1, 3);
						NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
						NAPI.Task.Run(delegate
						{
							Database.changeInventoryItem(p.Name, "Waffenteile", count, false);
							Notification.SendPlayerNotifcation(p, "+" + count + " Waffenteile", 3000, "gray", "farming", "gray");
						}, 4 * 6000);
						if (farming.Contains(p))
							farming.Remove(p);
						else
						{
							NAPI.Player.StopPlayerAnimation(p);
							p.TriggerEvent("disableAllPlayerActions", false);
							p.SetData("IS_FARMING", false);
							if (farming.Contains(p))
								farming.Remove(p);
						}
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
						if(Database.getItemCount(p.Name, "Waffenteile") > 500)
						{
							int random = new Random().Next(1, 3);

							switch(random)
							{
								case 1:
									Database.changeInventoryItem(p.Name, "Assaultrifle", 1, false);
									Database.changeInventoryItem(p.Name, "Waffenteile", 500, true);
									Notification.SendPlayerNotifcation(p, "+1 Assaultrifle", 4500, "grey", "farming", "grey");
									break;
								case 2:
									Database.changeInventoryItem(p.Name, "MicroSMG", 1, false);
									Database.changeInventoryItem(p.Name, "Waffenteile", 500, true);
									Notification.SendPlayerNotifcation(p, "+1 MicroSMG", 4500, "grey", "farming", "grey");
									break;
								case 3:
									Notification.SendPlayerNotifcation(p, "Deine Waffe ist bei der Verarbeiteung zerbrochen", 4500, "grey", "farming", "grey");
									Database.changeInventoryItem(p.Name, "Waffenteile", 500, true);
									break;
							}


						} else
						{

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
