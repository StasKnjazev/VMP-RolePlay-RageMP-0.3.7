using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.XMenu
{
    class VehicleMenu : Script
    {
        [RemoteEvent("REQUEST_VEHICLE_INFORMATION")]
        public static void REQUEST_VEHICLE_INFORMATION(Client c, Vehicle veh = null)
        {
            Notification.SendPlayerNotifcation(c, "Auto-Modell: " + veh.DisplayName + " - ID: " + veh.GetData("VEHICLE_SQL_ID"), 3500, "lightblue", "Information", "");
        }

        [RemoteEvent("REQUEST_VEHICLE_TOGGLE_LOCK_OUTSIDE")]
        public static void REQUEST_VEHICLE_TOGGLE_LOCK_OUTSIDE(Client c, Vehicle veh = null)
        {
            if (!(veh == null))
            {
                if (veh.NumberPlate != null && Database.isVehicleOwnedByPlayer(c.Name, veh.NumberPlate) || Database.isVehicleRentedFromPlayer(c, veh.NumberPlate))
                {
					veh.Locked = !veh.Locked;
					veh.SetSharedData(Vehicles.VehicleData.VEHICLE_LOCKED_STATUS, veh.Locked);
					string text = veh.Locked ? "zugeschlossen" : "aufgeschlossen";
                    Notification.SendPlayerNotifcation(c, "Fahrzeug " + text, 3500, (text == "zugeschlossen") ? "red" : "green", "", "");
				} else if(veh.GetSharedData("FRAKTION") == c.GetSharedData("FRAKTION"))
				{
					veh.Locked = !veh.Locked;
					veh.SetSharedData(Vehicles.VehicleData.VEHICLE_LOCKED_STATUS, veh.Locked);
					string text = veh.Locked ? "zugeschlossen" : "aufgeschlossen";
					Notification.SendPlayerNotifcation(c, "Fahrzeug " + text, 3500, (text == "zugeschlossen") ? "red" : "green", "", "");
				}
            }
        }

        [RemoteEvent("REQUEST_VEHICLE_TOGGLE_LOCK")]
        public static void REQUEST_VEHICLE_TOGGLE_LOCK(Client c)
        {
            Vehicle vehicle = c.Vehicle;
            if (!(vehicle == null))
            {
                if (vehicle.NumberPlate != null && Database.isVehicleOwnedByPlayer(c.Name, vehicle.NumberPlate) || Database.isVehicleRentedFromPlayer(c, vehicle.NumberPlate))
                {
                    vehicle.Locked = !vehicle.Locked;
					vehicle.SetSharedData(Vehicles.VehicleData.VEHICLE_LOCKED_STATUS, vehicle.Locked);
					string text = vehicle.Locked ? "zugeschlossen" : "aufgeschlossen";
                    Notification.SendPlayerNotifcation(c, "Fahrzeug " + text, 3500, (text == "zugeschlossen") ? "red" : "green", "", "");

				}else if(vehicle.GetSharedData("FRAKTION") == c.GetSharedData("FRAKTION"))
				{
					vehicle.Locked = !vehicle.Locked;
					vehicle.SetSharedData(Vehicles.VehicleData.VEHICLE_LOCKED_STATUS, vehicle.Locked);
					string text = vehicle.Locked ? "zugeschlossen" : "aufgeschlossen";
					Notification.SendPlayerNotifcation(c, "Fahrzeug " + text, 3500, (text == "zugeschlossen") ? "red" : "green", "", "");
				}
            }
        }

        [RemoteEvent("REQUEST_VEHICLE_TOGGLE_DOOR")]
        public static void REQUEST_VEHICLE_TOGGLE_DOOR(Client c)
        {
            

            Vehicle vehicle = c.Vehicle;

            if (vehicle.GetSharedData("kofferraumStatus") == null)
                return;

            if (!(vehicle == null) && !vehicle.Locked)
            {
                vehicle.SetSharedData("kofferraumStatus", !vehicle.GetSharedData("kofferraumStatus"));
                string text = (vehicle.GetSharedData("kofferraumStatus") ? "aufgeschlossen" : "zugeschlossen");
                Notification.SendPlayerNotifcation(c, "Kofferraum " + text, 3500, (text == "zugeschlossen") ? "red" : "green", "", "");

            }
        }

        [RemoteEvent("REQUEST_VEHICLE_TOGGLE_DOOR_OUTSIDE")]
        public static void REQUEST_VEHICLE_TOGGLE_DOOR_OUTSIDE(Client c, Vehicle veh = null)
        {
            if (!(veh == null) && !veh.Locked)
            {
                if (veh.GetSharedData("kofferraumStatus") == null)
                    return;

                veh.SetSharedData("kofferraumStatus", !veh.GetSharedData("kofferraumStatus"));
                string text = veh.GetSharedData("kofferraumStatus") ? "aufgeschlossen" : "zugeschlossen";
                Notification.SendPlayerNotifcation(c, "Kofferraum " + text, 3500, (text == "zugeschlossen") ? "red" : "green", "", "");

            }
        }

        [RemoteEvent("REQUEST_VEHICLE_TOGGLE_ENGINE")]
        public static void REQUEST_VEHICLE_TOGGLE_ENGINE(Client c)
        {
            Vehicle vehicle = c.Vehicle;
            if (!(vehicle == null))
            {
                if (vehicle.NumberPlate != null)
                {
					if (vehicle.GetSharedData(Vehicles.VehicleData.VEHICLE_FUEL_STATUS) < 1)
					{
						Notification.SendPlayerNotifcation(c, "Das Fahrzeug hat kein Benzin mehr", 4500, "red", "", "");
					}
					else
					{
						vehicle.EngineStatus = !vehicle.EngineStatus;
						vehicle.SetSharedData(Vehicles.VehicleData.VEHICLE_ENGINE_STATUS, vehicle.EngineStatus);
						string text = vehicle.EngineStatus ? "eingeschaltet." : "ausgeschaltet.";
						Notification.SendPlayerNotifcation(c, "Motor " + text, 3500, (text == "ausgeschaltet.") ? "red" : "green", "", "");
					}
				}
            }
        }

		[RemoteEvent("REQUEST_VEHICLE_REPAIR")]
		public static void REQUEST_VEHICLE_REPAIR(Client p, Vehicle veh = null)
		{
			if(!(veh == null))
			{
				if(veh.NumberPlate != null)
				{
					if(Database.getItemCount(p.Name, "Reperaturkasten") > 0)
					{
							p.TriggerEvent("sendProgressbar", new object[1]
							{
							10000
							});
							Functions.disableAllPlayerControls(p, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "mini@repair", "fixing_a_player", 8f);
							NAPI.Task.Run(delegate
							{
								p.TriggerEvent("disableAllPlayerActions", new object[1]
								{
								false
								});
								Functions.disableAllPlayerControls(p, false);
								NAPI.Player.StopPlayerAnimation(p);
								Database.changeInventoryItem(p.Name, "Reperaturkasten", 1, true);
								Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug mit dem Kennzeichen " + veh.NumberPlate + " repariert", 4500, "grey", "", "");
								NAPI.Vehicle.RepairVehicle(veh);
							}, 10000);
					} else
					{
						Notification.SendPlayerNotifcation(p, "Du hast keinen Reperaturkasten dabei", 4500, "red", "", "");
					}
				}
			}
		}

		[RemoteEvent("REQUEST_VEHICLE_FILL_FUEL")]
		public void REQUEST_VEHICLE_FILL_FUEL(Client p)
		{
			try
			{
				foreach(Tankstellen.TankstellenModel tankstellen in Tankstellen.TankstellenRegister.tankstellen)
				{
					if(p.Position.DistanceTo(tankstellen.position) < 20f)
					{
						p.TriggerEvent("openWindow", "TextInputBox", "{\"textBoxObject\":{\"Title\":\"50\",\"Callback\":\"fillvehicle\",\"CustomData\":{\"MaxLiter\":\"100\",\"FuelTime\":\"500\",\"Price\":\"" + tankstellen.cost + "\",\"MaxPrice\":\"100\"}}}");
						p.TriggerEvent("componentReady", "TextInputBox");
					}
				}
			} catch (Exception ex)
			{
				Log.Write(ex.Message);
				Log.Write(ex.StackTrace);
			}
		}

		[RemoteEvent("fillvehicle")]
		public void fillvehicle(Client client, int liter)
		{
			Vehicle veh = Other.Inventory.GetClosestVehicle(client, 2f);

			foreach(Tankstellen.TankstellenModel tankstellen in Tankstellen.TankstellenRegister.tankstellen)
			{
				if(client.Position.DistanceTo(tankstellen.position) < 20f)
				{
					Database.changeMoney(client.Name, tankstellen.cost * liter, true);
					Notification.SendPlayerNotifcation(client, "Du hast " + tankstellen.cost * liter + "$ für " + liter + "/L gezahlt!", 5000, "grey", "FUELSTATION", "");
				}
			}

			Database.updateVehicleFuel(veh.NumberPlate, (int)Database.getVehicleFuel(veh.NumberPlate) + liter);
			veh.SetSharedData(Vehicles.VehicleData.VEHICLE_FUEL_STATUS, (int)Database.getVehicleFuel(veh.NumberPlate));
			client.TriggerEvent("updateVehicleData", liter, 0, 0, false, true);
		}


	}
}
