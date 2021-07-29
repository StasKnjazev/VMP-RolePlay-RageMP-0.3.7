using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Vehicles;

namespace GVMPc.Fraktionen
{
    class FraktionsGaragen : Script
    {
        [RemoteEvent("fraktionsgarage:requestvehicles")]
        public void requestVehicles(Client p, string type)
        {
            if (type == null)
                return;

            try
            {
                if (type == "ausparken")
                    p.TriggerEvent("fraktionsgarage:loadvehicles", NAPI.Util.ToJson(Database.getFraktionVehicles(p.GetSharedData("FRAKTION"))));

                if (type == "einparken")
                {
                    List<VehicleModel> vehicles = new List<VehicleModel>();

                    foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                    {
                        if (vehicle.Position.DistanceTo2D(p.Position) < 40.0f)
                        {
                            if (vehicle.HasSharedData("FRAKTION") && vehicle.GetSharedData("FRAKTION") == p.GetSharedData("FRAKTION"))
                                vehicles.Add(new VehicleModel(p.Name, vehicle.DisplayName, p.GetSharedData("FRAKTION")));
                        }
                    }

                    p.TriggerEvent("fraktionsgarage:loadvehicles", NAPI.Util.ToJson(vehicles));
                }
            } catch(Exception ex) { Log.Write(ex.Message); }

        }

        [RemoteEvent("fraktionsgarage:park")]
        public void parkVehicle(Client p, string name, string plate, string garage, int mode)
        {
            if (plate == null || garage == null)
                return;

            try
            {
                if (mode == 0)
                {
                    p.TriggerEvent("fraktionsgarage:closegarage");

                    foreach (Fraktion fraktion in FraktionRegister.fraktionList)
                    {
                        if (fraktion.fraktionName == p.GetSharedData("FRAKTION"))
                        {
                            VehicleHash vehHash = (VehicleHash)NAPI.Util.GetHashKey(name);
                            Vehicle veh = NAPI.Vehicle.CreateVehicle(vehHash, fraktion.garageSpawnPoint, fraktion.garageSpawnPointRotation, 5, 5, fraktion.shortName, 255, false, true, p.Dimension);
                            List<int> listKeys = new List<int>();

                            veh.SetData(VehicleData.VEHICLE_CAR_NAME, name);
                            veh.SetData(VehicleData.VEHICLE_LIST_KEYS, listKeys);
                            veh.SetSharedData(VehicleData.VEHICLE_LOCKED_STATUS, false);
							veh.SetSharedData(VehicleData.VEHICLE_FUEL_STATUS, 100);
                            veh.SetSharedData("FRAKTION", p.GetSharedData("FRAKTION"));
                            veh.NumberPlate = p.GetSharedData("FRAKTION_SHORT");
                            veh.CustomPrimaryColor = fraktion.rgbColor;
                            veh.CustomSecondaryColor = fraktion.rgbColor;
                            veh.SetMod(11, 2);
                            veh.SetMod(12, 2);
                            veh.SetMod(13, 2);
                            veh.SetMod(15, 2);
                            veh.SetMod(0, 1);
                            veh.SetMod(46, 1);
                            veh.SetMod(6, 2);
                            veh.SetMod(1, 2);
                            veh.SetMod(18, 0);
                            veh.WindowTint = 2;

                            Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug " + name + " ausgeparkt.", 5000, "white", p.GetSharedData("FRAKTION"), "white");
                        }
                    }

                }
                else
                {
                    p.TriggerEvent("fraktionsgarage:closegarage");

                    foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                    {
                        if (vehicle.GetSharedData("FRAKTION") == p.GetSharedData("FRAKTION"))
                        {
                            vehicle.Delete();
                            Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug " + name + " eingeparkt.", 5000, "white", garage, "orange");
                            return;
                        }
                    }

                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

    }
}
