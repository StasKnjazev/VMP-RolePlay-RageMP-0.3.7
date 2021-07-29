using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Vehicles;

namespace GVMPc.Laptop
{
	public class VehicleSupport : Script
	{
		[RemoteEvent("requestVehicleData")]
		public void requestSupportVehicleList(Client p, int id)
		{
			VehicleModel vehicle = Database.getVehicleById(id);

			Vehicle vehicle2 = Database.getVehicleFromPlate(vehicle.plate);

			string parked = ""; 

			if(Database.isVehicleParked(id))
			{
				parked = "Eingeparkt";
			} else
			{
				parked = "Ausgeparkt";
			}

			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"SupportVehicleProfile",
				"responseVehicleData",
				"[{\"id\":\"" + id +"\",\"vehiclehash\":\"" + vehicle.name +"\",\"inGarage\":\"" + parked +"\",\"owner\":\"" + vehicle.owner +"\"}]"
			});

			Log.Write("Test");
		}

		[RemoteEvent("SupportGoToVehicle")]
		public void SupportGoToVehicle(Client p, int id)
		{
			VehicleModel vehicle = Database.getVehicleById(id);

			Vehicle vehicle2 = Database.getVehicleFromPlate(vehicle.plate);

			if (!Database.isVehicleParked(id))
			{
				Notification.SendPlayerNotifcation(p, "Du hast dich zu dem Fahrzeug " + vehicle.name + " teleportiert", 5000, "red", "SUPPORT", "");
				Anticheat.Wait(p); p.Position = vehicle2.Position.Add(new Vector3(0, 0, 1.5));
				return;
			} else
			{
				Notification.SendPlayerNotifcation(p, "Das Fahrzeug existiert nicht", 5000, "red", "SUPPORT", "");
			}
		}

		[RemoteEvent("SupportSetGarage")]
		public void SupportSetGarage(Client p, int id)
		{
			VehicleModel vehicle = Database.getVehicleById(id);

			Vehicle vehicle2 = Database.getVehicleFromPlate(vehicle.plate);

			if (!Database.isVehicleParked(id))
			{
				Database.changeVehicleState(vehicle.plate, 1);
				vehicle2.Delete();
				Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug mit der ID " + id + " eingeparkt", 5000, "red", "SUPPORT", "");
			} else
			{
				Notification.SendPlayerNotifcation(p, "Das Fahrzeug ist bereits eingeparkt", 5000, "red", "SUPPORT", "");
			}
		}
	}
}
