using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Jobs
{
	class LKW : Script
	{
		public static Vector3 anfangsPunkt = new Vector3(947.1248, -1249.937, 25.97585);


		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Marker.CreateMarker(1, anfangsPunkt, new Vector3(), new Vector3(), 1f, new Color(255, 0, 0), false, 0);
			ColShape val = NAPI.ColShape.CreateCylinderColShape(anfangsPunkt, 2f, 2f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openTruck"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Ware auszuliefern", "LKW", "white", 4500));
		}

		[RemoteEvent("openTruck")]
		public void openTruck(Client p)
		{

			if(p.GetSharedData("JOB") == "LKWFahrer")
			{
				VehicleHash vehHash = NAPI.Util.VehicleNameToModel("phantom");
				VehicleHash vehHash2 = NAPI.Util.VehicleNameToModel("trailers4");
				Vehicle veh = NAPI.Vehicle.CreateVehicle(vehHash, new Vector3(915.5844, -1263.426, 24.46434), 31.38102f, 0, 0, "TRUCK", 255, false, true, 0);
				Vehicle veh2 = NAPI.Vehicle.CreateVehicle(vehHash2, new Vector3(862.8278, -1144.052, 23.04628), 180.2676f, 0, 0, "TRUCK", 255, false, true, 0);
				ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(62.83304, 125.4466, 78.08797), 2f, 2f, 0);
				val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("getMoney"));
				val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die Ware abzuliefern", "LKW", "white", 4500));
				p.SetData("IS_TRUCKING", true);
				p.TriggerEvent("setWayPoint", new Vector3(570.4526, 0, 0), new Vector3(0, -1226.024, 0));
				Notification.SendPlayerNotifcation(p, "Schaue auf die Karte um den Zielort zu finden", 4500, "white", "LKW", "");
				NAPI.Task.Run(delegate
				{
					NAPI.ColShape.DeleteColShape(val2);
					Notification.SendPlayerNotifcation(p, "Du hast das Ziel nicht in der entsprechenden Zeit erreicht", 4500, "red", "LKW", "");
					p.ResetData("IS_TRUCKING");
					p.ResetData("GOT_LKWMONEY");
					veh.Delete();
					veh2.Delete();

				}, 270000);
			} else
			{
				Notification.SendPlayerNotifcation(p, "Du hast diesen Job nicht", 4500, "red", "LKW", "");
			}
		}

		[RemoteEvent("getMoney")]
		public void getMoney(Client p)
		{
			if (p.HasData("GOT_LKWMONEY") == true) 
				return;

			try
			{
				Database.changeMoney(p.Name, 15000, false);
				Notification.SendPlayerNotifcation(p, "Du hast 15000$ für die Fahrt bekommen", 4500, "red", "LKW", "");
				p.ResetData("IS_TRUCKING");
				p.SetData("GOT_LKWMONEY", true);

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

	}
} 
