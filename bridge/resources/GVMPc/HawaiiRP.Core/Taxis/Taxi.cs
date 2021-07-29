using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Taxis
{
	public class Taxi : Script
	{
		public static Vector3 vehiclePoint = new Vector3(899.9651, -180.7854, 72.76451);

		public static float rotationPoint = 227.5014f;

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(56, vehiclePoint, 1f, 73, "Taxi Unternehmen", 255, 0, true, 0, 0);
			NAPI.Marker.CreateMarker(1, new Vector3(894.989, -179.1306, 73.60023), new Vector3(), new Vector3(), 2f, new Color(0, 122, 255), false, 0);
			ColShape col = NAPI.ColShape.CreateCylinderColShape(new Vector3(894.989, -179.1306, 73.60023), 2f, 2f, 0);
			col.SetData("COLSHAPE_FUNCTION", new FunctionModel("goDuty"));
			col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Gehe in den Dienst", "TAXI", "yellow", 5000));
		}

		[RemoteEvent("goDuty")]
		public void goInDuty(Client p)
		{
			if (!p.HasData("TAXI_DUTY"))
			{
				p.SetData("TAXI_DUTY", true);
				Notification.SendPlayerNotifcation(p, "Du bist nun im Dienst", 5000, "yellow", "TAXI", "");
				foreach(Vehicle vehs in NAPI.Pools.GetAllVehicles())
				{
					if(NAPI.Entity.GetEntityPosition(vehs) == vehiclePoint)
					{
						Notification.SendPlayerNotifcation(p, "Der Parkplatz ist Besetzt!", 5000, "yellow", "", "");

					}
				}
			}

			VehicleHash vehHash = NAPI.Util.VehicleNameToModel("taxi");
			Vehicle veh = NAPI.Vehicle.CreateVehicle(vehHash, vehiclePoint, rotationPoint, 0, 0, "TAXI", 255, false, true, p.Dimension);
			veh.SetSharedData(Vehicles.VehicleData.VEHICLE_FUEL_STATUS, 100);
			veh.SetSharedData(Vehicles.VehicleData.VEHICLE_MILEAGE_STATUS, 0);
			veh.SetSharedData(Vehicles.VehicleData.VEHICLE_ENGINE_STATUS, true);


		}

	}
}
