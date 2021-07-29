using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Timers;

namespace GVMPc.Jobs
{
	public class GardenJob : Script
	{
		public static Timer jobTimer;

		public static List<Client> gardenJob = new List<Client>();

		public static int count = 98;

		public static VehicleHash vehHash = NAPI.Util.VehicleNameToModel("mower");

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(541, new Vector3(-949.217, 332.021, 70.23936), 1f, 52, "Gärtner", 255, 0, true, 0, 0);
			jobTimer = new Timer(45000.0);
			jobTimer.AutoReset = true;
			jobTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				gardenJobTimer(null);
			};
			ColShape col = NAPI.ColShape.CreateCylinderColShape(new Vector3(-949.217, 332.021, 70.23936), 2f, 2f, 0);
			col.SetData("COLSHAPE_FUNCTION", new FunctionModel("openGardenJob"));
			col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Gärtnerjob zu starten", "GÄRTNER", "darkgreen", 5000));
			ColShape col2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(-948.1132, 321.316, 70.25189), 4f, 3f, 0);
			col2.SetData("COLSHAPE_FUNCTION", new FunctionModel("emptyMower"));
			col2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Rasenmäher zu leeren", "GÄRTNER", "darkgreen", 5000));
		}

		[RemoteEvent("openGardenJob")]
		public void openGardenJob(Client p)
		{
			if(!p.HasData("IS_MOWING"))
			{
				jobTimer.Start();
				Notification.SendPlayerNotifcation(p, "Du hast ein Job angenommen", 5000, "darkgreen", "GÄRTNER", "");
				Vehicle vehicle = NAPI.Vehicle.CreateVehicle(vehHash, new Vector3(-960.5166, 330.4075, 70.20253), 83.54461f, 29, 29, "GÄRTNER", 255, false, true, 0);
				vehicle.SetSharedData(Vehicles.VehicleData.VEHICLE_FUEL_STATUS, 100);
				vehicle.SetSharedData(Vehicles.VehicleData.VEHICLE_MILEAGE_STATUS, 0);
				p.SetIntoVehicle(vehicle, -1);
				p.SetData("IS_MOWING", true);
				gardenJob.Add(p);
			} else
			{
				Notification.SendPlayerNotifcation(p, "Du machst bereits einen Job", 5000, "darkgreen", "GÄRTNER", "");
			}
		}

		public static void gardenJobTimer(object unused)
		{
			foreach(Client p in gardenJob.ToArray())
			{
				if(p.HasData("IS_MOWING"))
				{
					if(p.IsInVehicle)
					{
						if(count < 99)
						{
							count ++;
							Notification.SendPlayerNotifcation(p, count + "% / 100%", 5000, "darkgreen", "GÄRTNER", "");
						} else
						{
							jobTimer.Stop();
							Notification.SendPlayerNotifcation(p, "Du hast 100% erreicht! Leere deinen Rasenmäher", 5000, "darkgreen", "GÄRTNER", "");
						}
					} else
					{
						Notification.SendPlayerNotifcation(p, "Du hast deine Rasenmäher verlassen. Dein Job wurde dadurch beendet", 5000, "darkgreen", "GÄRTNER", "");
						jobTimer.Stop();
						p.SetData("IS_MOWING", false);
					}
				}
			}
		}

		[RemoteEvent("emptyMower")]
		public void emptyMower(Client p)
		{
			if (p.IsInVehicle)
			{
				int money = new Random().Next(1500, 20000);
				Database.changeUserXP(p.Name, 2, false);
				Database.changeUserEventPoints(p.Name, 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast " + money + "$ und 2XP bekommen.", 5000, "darkgreen", "GÄRTNER", "");
				p.SetData("IS_MOWING", false);
				gardenJob.Remove(p);
				count = 0;
				Vehicle veh = p.Vehicle;
				veh.Delete();
			} else
			{
				Notification.SendPlayerNotifcation(p, "Du musst auf deinen Rasenmäher steigen", 5000, "darkgreen", "GÄRTNER", "");
			}
		}
	}
}
