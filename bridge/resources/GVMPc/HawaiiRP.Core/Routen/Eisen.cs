/*using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using GTANetworkAPI;
using GVMPc;

namespace GVMPc.Routen
{
    class Eisen : Script
    {
        public static List<Client> farming = new List<Client>();
        public static List<Client> processing = new List<Client>();
		public static List<Client> processing2 = new List<Client>();
		public static List<Client> processing3 = new List<Client>();

        public static Timer OnFarmingSpentTimer;
        public static Timer OnProcessingSpentTimer;
		public static Timer OnProcessingSpent2Timer;
		public static Timer OnProcessingSpent3Timer;

		[ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {

			NAPI.Blip.CreateBlip(171, new Vector3(2952.127, 2788.6, 40.38878), 1f, 1, "Eisenerz Farm", 255, 0, true, 0, 0);
			NAPI.Blip.CreateBlip(171, new Vector3(1551.969, 2189.698, 77.74232), 1f, 1, "Eisenerz Verarbeiter", 255, 0, true, 0, 0);
			NAPI.Blip.CreateBlip(171, new Vector3(1122.352, -1997.46, 34.33934), 1f, 1, "Eisenbarren Verarbeiter", 255, 0, true, 0, 0);

			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(2952.127, 2788.6, 40.38878), 5f, 4f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming2", "Eisen", "farmer"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Eisenerz zu sammeln", "EISEN", "orange", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(1551.969, 2189.698, 77.74232), 5f, 4f, 0);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming2", "Eisen", "processing"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um das Eisenerz zu verarbeiten", "EISEN", "orange", 4500));
			ColShape val3 = NAPI.ColShape.CreateCylinderColShape(new Vector3(895.9395, -896.244, 26.69646), 5f, 4f, 0);
			val3.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming2", "Eisen", "processing2"));
			val3.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die Eisenbarren zu verarbeiten", "EISEN", "orange", 4500));
			ColShape val4 = NAPI.ColShape.CreateCylinderColShape(new Vector3(1122.352, -1997.46, 34.33934), 5f, 4f, 0);
			val4.SetData("COLSHAPE_FUNCTION", new FunctionModel("changeFarming2", "Eisen", "processing3"));
			val4.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die Eisenbarren zu verarbeiten", "EISEN", "orange", 4500));


			OnFarmingSpentTimer = new Timer(11000.0);
            OnFarmingSpentTimer.AutoReset = true;
            OnFarmingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                OnFarmingSpent(null);
            };
            OnFarmingSpentTimer.Start();

            OnProcessingSpentTimer = new Timer(30000.0);
            OnProcessingSpentTimer.AutoReset = true;
            OnProcessingSpentTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                OnProcessingSpent(null);
            };
            OnProcessingSpentTimer.Start();

			OnProcessingSpent2Timer = new Timer(80000.0);
			OnProcessingSpent2Timer.AutoReset = true;
			OnProcessingSpent2Timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnProcessingSpent2(null);
			};
			OnProcessingSpent2Timer.Start();

			OnProcessingSpent3Timer = new Timer(6000.0);
			OnProcessingSpent3Timer.AutoReset = true;
			OnProcessingSpent3Timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OnProcessingSpent3(null);
			};
			OnProcessingSpent3Timer.Start();

			Log.Write("Eisen geladen.");
        }

		[RemoteEvent("changeFarming2")]
		public void changeFarmingEisen(Client p, string arg1, string arg2)
		{
			try
			{
				if(arg1 == "Eisen")
				{
					if(arg2 == "farmer")
					{
						if(p.GetData("IS_FARMING"))
						{
							Notification.SendPlayerNotifcation(p, "Du hörst auf zu sammeln...", 3500, "orange", "farming", "orange");
							Routen.Eisen.farming.Remove(p);
							p.SetData("IS_FARMING", false);
							NAPI.Player.StopPlayerAnimation(p);
							p.TriggerEvent("disableAllPlayerActions", false);

						} else
						{
							Notification.SendPlayerNotifcation(p, "Du hörst auf zu sammeln...", 3500, "orange", "farming", "orange");
							Routen.Eisen.farming.Add(p);
							p.SetData("IS_FARMING", false);
							NAPI.Player.StopPlayerAnimation(p);
							p.TriggerEvent("disableAllPlayerActions", false);
						}
					}
				}

			} catch(Exception ex)
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
                        int count = new Random().Next(5, 10);
                        NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
                        NAPI.Task.Run(delegate
                        {
                            Database.changeInventoryItem(p.Name, "Eisenerz", count, false);
                            Notification.SendPlayerNotifcation(p, "+" + count + " Eisenerz", 3000, "gray", "farming", "gray");
                        }, 10000);
                    }
                    else
                    {
                        if(farming.Contains(p))
                            farming.Remove(p);
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        public static void OnProcessingSpent(object unused)
        {
            try
            {
                foreach (Client p in processing.ToArray())
                {
                    if (NAPI.Pools.GetAllPlayers().Contains(p))
                    {
                        if (Database.getItemCount(p.Name, "Eisenerz") >= 10)
                        {
                            p.SetData("IS_FARMING", true);
                            Database.changeInventoryItem(p.Name, "Eisenbarren", 1, false);
                            Database.changeInventoryItem(p.Name, "Eisenerz", 10, true);
                            Notification.SendPlayerNotifcation(p, "+1 Eisenbarren", 3500, "gray", "farming", "gray");
                        }
                        else
                        {
							NAPI.Task.Run(delegate
							{

								Notification.SendPlayerNotifcation(p, "Du hast zu wenig Eisenerz dabei.", 3500, "gray", "farming", "gray");
								processing.Remove(p);

							}, 20000);
                        }
                    }
                    else
                    {
                        if(processing.Contains(p))
                            processing.Remove(p);
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

		public static void OnProcessingSpent2(object unused)
		{
			try
			{
				foreach (Client p in processing2.ToArray())
				{
					if (NAPI.Pools.GetAllPlayers().Contains(p))
					{
						if (Database.getItemCount(p.Name, "Eisenbarren") >= 20)
						{
							p.SetData("IS_FARMING", true);
							Database.changeInventoryItem(p.Name, "Stahl", 1, false);
							Database.changeInventoryItem(p.Name, "Eisenbarren", 20, true);
							Notification.SendPlayerNotifcation(p, "+1 Stahl", 3500, "gray", "farming", "gray");
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Du hast zu wenig Eisenbarren dabei.", 3500, "gray", "farming", "gray");
							processing2.Remove(p);
						}
					}
					else
					{
						if (processing2.Contains(p))
							processing2.Remove(p);
					}
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		public static void OnProcessingSpent3(object unused)
		{
			try
			{
				foreach (Client p in processing3.ToArray())
				{
					if (NAPI.Pools.GetAllPlayers().Contains(p))
					{
						if (Database.getItemCount(p.Name, "Stahl") >= 200)
						{
							p.SetData("IS_FARMING", true);
							Database.changeInventoryItem(p.Name, "Waffenteil", 1, false);
							Database.changeInventoryItem(p.Name, "Stahl", 200, true);
							Notification.SendPlayerNotifcation(p, "+1 Waffenteil", 3500, "gray", "farming", "gray");
						}
						else
						{
							Notification.SendPlayerNotifcation(p, "Du hast zu wenig Stahl dabei.", 3500, "gray", "farming", "gray");
							processing3.Remove(p);
						}
					}
					else
					{
						if (processing3.Contains(p))
							processing3.Remove(p);
					}
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}
	}
} */
