using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Ipad
{
	class VehicleTaxApp : Script
	{
		[RemoteEvent("requestVehicleTaxByModel")]
		public void requestVehicleTaxByModel(Client p, string plate)
		{
			List<object> VehicleList = new List<object>();

			string parked = "Ausgeparkt";

			Vehicle veh = Database.getVehicleFromPlate(plate);

			foreach (Vehicles.VehicleModel vehicles in Database.getUserVehicles(p.Name))
			{
				if (plate == vehicles.plate)
				{
					if (Database.isVehicleOwnedByPlayer(p.Name, plate))
					{
						VehicleList.Add(
							new
							{
								model = Database.getVehicleName(plate),
								tax = (int)Database.getVehicleTax(plate),
								parked = parked,
								preis = "8000",
								fuel = (int)Database.getVehicleFuel(veh.NumberPlate)

							});

						object JSONobject = new
						{
							overview = VehicleList,
							searchString = ""
						};

						p.TriggerEvent("componentServerEvent", new object[3]
						{
					      "VehicleTaxApp",
					      "responseVehicleTax",
					      NAPI.Util.ToJson((object)VehicleList)
						});
					}
					else
					{
						Notification.SendPlayerNotifcation(p, "Dieses Fahrzeug gehört dir nicht", 4500, "red", "FAHRZEUGÜBERSICHT", "");
					}
				}
			}
		}
	}
}
