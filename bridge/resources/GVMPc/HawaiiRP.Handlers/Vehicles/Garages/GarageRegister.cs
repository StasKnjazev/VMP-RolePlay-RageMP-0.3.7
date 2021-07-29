using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Fraktionen;
using GVMPc.Menus;
using GVMPc.Vehicles;
using GVMPc.Vehicles.Garages;

namespace GVMPc.Vehicles
{
    public class GarageRegister : Script
    {
        public static List<GarageModel> garages = new List<GarageModel>();

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            if (garages.Count < 1)
            {

                garages.Add(new GarageModel("Vespucci Garage", new Vector3(-1184.2845, -1509.452, 4.5480242), new Vector3(-1184.7214, -1492.8828, 3.2796707), 126.469376f));
                garages.Add(new GarageModel("Pillbox Garage", new Vector3(-313.81174, -1034.3071, 30.430506), new Vector3(-305.34454, -1012.9885, 29.285078), -113.43376f));
                garages.Add(new GarageModel("Meeting Point Garage", new Vector3(100.46749, -1073.2855, 29.274118), new Vector3(121.32157, -1082.3136, 28.093328), -1.5121901f));
                garages.Add(new GarageModel("Flughafen Garage", new Vector3(-984.2674, -2640.798, 13.882), new Vector3(-986.9277, -2647.4, 12.87818), 60.03492f));
                garages.Add(new GarageModel("Hafen Garage", new Vector3(-331.3111, -2779.0078, 5.0451927), new Vector3(-338.7875, -2782.18, 3.900241), 92.16411f));
                garages.Add(new GarageModel("Universität Garage", new Vector3(-1681.067, 62.73859, 63.92909), new Vector3(-1678.641, 70.34713, 62.91925), 290.5141f));
                garages.Add(new GarageModel("Sandy Shores Garage", new Vector3(1738.134, 3709.187, 34.03833), new Vector3(1721.663, 3713.879, 33.11208), 20.89267f));
				garages.Add(new GarageModel("Paleto Garage", new Vector3(53.40103, 6337.846, 30.39694), new Vector3(37.99773, 6336.321, 30.13011), 15.64236f));

				foreach (GarageModel garage in garages)
                {
                    ColShape val = NAPI.ColShape.CreateCylinderColShape(garage.position, 1.4f, 1.4f, 0);
                    val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openGarage", "0", garage.name.Replace(" Garage", "")));
                    val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um die Garage zu öffnen.", garage.name, "orange", 3500));


                    NAPI.Blip.CreateBlip(473, garage.position, 1.0f, 0, garage.name, 255, 0, true, 0, 0);
                    NAPI.Marker.CreateMarker(36, garage.position, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1.0f, new Color(255, 140, 0), false, 0);
                }
            }
            Log.Write(garages.Count +" Garagen geladen.");
        }

        [RemoteEvent("openGarage")]
        public static void openGarage(Client player, string garageId, string garage)
        {
            try
            {
                if (garageId == null || garage == null)
                    return;

                int num = garage.Length;
                player.TriggerEvent("openWindow", new object[2]
                {
                "Garage",
                "{\"id\": " + num + ", \"name\": \"" + garage + "\"}"
                });
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("openFraktionsGarage")]
        public static void openFraktionsGarage(Client player, string fraktionsName)
        {
            try
            {
                if (fraktionsName == null)
                    return;

                if (player.GetSharedData("FRAKTION") != fraktionsName)
                    return;

                player.TriggerEvent("openWindow", new object[2]
                {
                "Garage",
                "{\"id\": " + Database.getFraktionSQLId(fraktionsName) + ", \"name\": \"" + player.GetSharedData("FRAKTION_SHORT") + "\"}"
                });
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("requestVehicleList")]
        public static void requestVehicleList(Client client, int id, string val)
        {
            try
            {
                int num = ((val == "takeout") ? 1 : 0);
                List<GarageVehicleModel> list = new List<GarageVehicleModel>();
                switch (num)
                {
                    case 1:
                        {
                            if (id == Database.getFraktionSQLId(client.GetSharedData("FRAKTION")))
                            {

                                foreach (VehicleModel vehicleModel in Database.getFraktionVehicles(client.GetSharedData("FRAKTION")))
                                {
                                    list.Add(new GarageVehicleModel((int)Database.getFraktionVehicleSQLId(vehicleModel.name.ToLower(), client.GetSharedData("FRAKTION")), (int)Database.getSQLId(client.Name), vehicleModel.name, vehicleModel.plate));
                                }

                                client.TriggerEvent("componentServerEvent", new object[3]
                                {
                                "Garage",
                                "responseVehicleList",
                                NAPI.Util.ToJson(list)
                                });
                            }
                            else
                            {
                                list = Database.getParkedVehicles(client.Name);

                                client.TriggerEvent("componentServerEvent", new object[3]
                                {
                                "Garage",
                                "responseVehicleList",
                                NAPI.Util.ToJson(list)
                                });
                            }
                            break;
                        }
                    case 0:
                        {
                            List<Vehicle> list2 = NAPI.Pools.GetAllVehicles().FindAll((Vehicle v) => isNearby(client, v));
                            foreach (Vehicle item2 in list2)
                            {
                                VehicleModel vehicleModel = item2.GetData("VEHICLE_VEHICLEMODEL");
                                if (item2.NumberPlate != null)
                                {
                                    if (item2.HasSharedData("FRAKTION") && item2.GetSharedData("FRAKTION") == client.GetSharedData("FRAKTION"))
                                    {
                                        string vehicleModel2 = item2.GetData(VehicleData.VEHICLE_CAR_NAME);

                                        list.Add(new GarageVehicleModel((int)Database.getFraktionVehicleSQLId(vehicleModel2, client.GetSharedData("FRAKTION")), (int)Database.getSQLId(client.Name), vehicleModel2, client.GetSharedData("FRAKTION_SHORT")));
                                    }
                                    else if (Database.isVehicleOwnedByPlayer(client.Name, vehicleModel.plate) && vehicleModel != null)
                                    {
                                        list.Add(new GarageVehicleModel((int)Database.getVehicleSQLId(vehicleModel.plate), (int)Database.getSQLId(client.Name), vehicleModel.name, vehicleModel.plate));
                                    }
                                }
                            }
                            client.TriggerEvent("componentServerEvent", new object[3]
                            {
                    "Garage",
                    "responseVehicleList",
                    NAPI.Util.ToJson((object)list)
                            });
                            break;
                        }
                }
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("requestVehicle")]
        public static void requestVehicle(Client client, string state, int id, string vehid)
        {
            try
            {
                GarageModel garageModel = garages.Find((GarageModel g) => g.name.Replace(" Garage", "").Length == id);
                int num = ((state == "takeout") ? 1 : 0);
                List<VehicleModel> list = new List<VehicleModel>();
                switch (num)
                {
                    case 1:
                        {

                            if (id == Database.getFraktionSQLId(client.GetSharedData("FRAKTION")))
                            {
                                string vehicleModel = Database.getFraktionsVehicleModelById(int.Parse(vehid));
                                foreach (Fraktion fraktion in FraktionRegister.fraktionList)
                                {
                                    if (fraktion.fraktionName == client.GetSharedData("FRAKTION"))
                                    {
                                        VehicleHash vehHash = (VehicleHash)NAPI.Util.GetHashKey(vehicleModel);
                                        Vehicle veh = NAPI.Vehicle.CreateVehicle(vehHash, fraktion.garageSpawnPoint, fraktion.garageSpawnPointRotation, 5, 5, fraktion.shortName, 255, false, true, client.Dimension);
                                        List<int> listKeys = new List<int>();

                                        veh.SetData(VehicleData.VEHICLE_CAR_NAME, vehicleModel);
                                        veh.SetData(VehicleData.VEHICLE_LIST_KEYS, listKeys);
                                        veh.SetSharedData(VehicleData.VEHICLE_LOCKED_STATUS, false);
                                        veh.SetSharedData("FRAKTION", client.GetSharedData("FRAKTION"));
                                        veh.NumberPlate = client.GetSharedData("FRAKTION_SHORT");
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

                                        Notification.SendPlayerNotifcation(client, "Du hast das Fahrzeug " + vehicleModel + " ausgeparkt.", 5000, "white", client.GetSharedData("FRAKTION"), "white");
                                    }
                                }
                            }
                            else
                            {
                                VehicleModel vehicleModel = Database.getVehicleById(int.Parse(vehid));
                                if (Database.isVehicleParked(int.Parse(vehid)))
                                {
                                    Vehicle val7 = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(vehicleModel.name), garageModel.ausparkPunkt, garageModel.ausparkRotation, 0, 0, vehicleModel.plate, byte.MaxValue, true, true, client.Dimension);
                                    val7.SetData("VEHICLE_SQL_ID", int.Parse(vehid));
                                    val7.SetData("VEHICLE_LIST_KEYS", new List<int>());
                                    val7.SetSharedData("kofferraumStatus", false);
                                    Database.restoreVehicleTuning(val7);
                                    Database.restoreVehicleColor(val7);
                                    val7.SetData(VehicleData.VEHICLE_CAR_NAME, vehicleModel.name);
                                    val7.SetSharedData(VehicleData.VEHICLE_LOCKED_STATUS, true);
                                    val7.SetSharedData(VehicleData.VEHICLE_FUEL_STATUS, 100);
                                    val7.NumberPlate = vehicleModel.plate;
                                    Database.changeVehicleState(vehicleModel.plate, 0);
                                    val7.SetData("VEHICLE_VEHICLEMODEL", vehicleModel);

                                    Notification.SendPlayerNotifcation(client, "Du hast das Fahrzeug " + vehicleModel.name + " ausgeparkt.", 5000, "orange", "GARAGE", "orange");
                                }
                            }
                            break;
                        }
                    case 0:
                        {
                            if (id == Database.getFraktionSQLId(client.GetSharedData("FRAKTION")))
                            {
                                Vehicle val = NAPI.Pools.GetAllVehicles().Find((Vehicle v) => !v.HasData(VehicleData.VEHICLE_CAR_NAME) || ((int)Database.getFraktionVehicleSQLId((string)v.GetData(VehicleData.VEHICLE_CAR_NAME).ToLower(), client.GetSharedData("FRAKTION"))) == int.Parse(vehid));

                                if (val == null)
                                    return;

                                val.Delete();

                                Notification.SendPlayerNotifcation(client, "Du hast das Fahrzeug eingeparkt.", 5000, "orange", "GARAGE", "orange");
                            }
                            else
                            {
                                Vehicle val = NAPI.Pools.GetAllVehicles().Find((Vehicle v) => isSameVehicle(v, Convert.ToInt32(vehid)));

                                if (val == null)
                                    return;

                                Database.changeVehicleState(val.NumberPlate, 1);
                                val.Delete();

                                Notification.SendPlayerNotifcation(client, "Du hast das Fahrzeug mit Kennzeichen " + val.NumberPlate + " eingeparkt.", 5000, "orange", "GARAGE", "orange");
                            }
                            break;
                        }
                }
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

        public static bool isSameVehicle(Vehicle v, int id)
        {
            VehicleModel vehicleModel = v.GetData("VEHICLE_VEHICLEMODEL");
            if (vehicleModel != null)
            {
                return Database.getVehicleSQLId(vehicleModel.plate) == id;
            }
            return false;
        }

        public static bool isNearby(Client client, Vehicle veh)
        {
            return veh.Position.DistanceTo(client.Position) <= 30f;
        }

        [RemoteEvent("garage:requestvehicles")]
        public void requestVehicles(Client p, string type)
        {
            if (type == null)
                return;

            try
            {
                if (type == "ausparken")
                    p.TriggerEvent("garage:loadvehicles", NAPI.Util.ToJson(Database.getParkedVehicles(p.Name)));

                if (type == "einparken")
                {
                    List<VehicleModel> vehicles = new List<VehicleModel>();

                    foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                    {
                        if (vehicle.Position.DistanceTo2D(p.Position) < 25.0f)
                        {
                            if (vehicle.NumberPlate != null && vehicle.Dimension == p.Dimension && Database.isVehicleOwnedByPlayer(p.Name, vehicle.NumberPlate))
                                vehicles.Add(new VehicleModel(p.Name, vehicle.DisplayName, vehicle.NumberPlate));
                        }
                    }

                    p.TriggerEvent("garage:loadvehicles", NAPI.Util.ToJson(Database.getUserVehicles(p.Name)));
                }
            }
            catch (Exception ex) { Log.Write(ex.Message); }

        }

        [RemoteEvent("garage:park")]
        public void parkVehicle(Client p, string name, string plate, string garage, int mode)
        {
            if (name == null || plate == null || garage == null)
                return;

            try
            {
                if (mode == 0)
                {
                    Database.changeVehicleState(plate, 0);
                    p.TriggerEvent("garage:closegarage");

                    foreach (GarageModel garageModel in garages)
                    {
                        if (garage == garageModel.name)
                        {
                            VehicleHash vehHash = (VehicleHash)NAPI.Util.GetHashKey(name);
                            Vehicle veh = NAPI.Vehicle.CreateVehicle(vehHash, garageModel.ausparkPunkt, garageModel.ausparkRotation, 5, 5, plate, 255, false, true, 0);
                            List<int> listKeys = new List<int>();
                            veh.Locked = true;
                            veh.NumberPlate = plate;

                            Database.restoreVehicleTuning(veh);
                            Database.restoreVehicleColor(veh);

                            veh.SetData(VehicleData.VEHICLE_CAR_NAME, name);
                            veh.SetData(VehicleData.VEHICLE_LIST_KEYS, listKeys);
                            veh.SetSharedData(VehicleData.VEHICLE_LOCKED_STATUS, false);
                            veh.SetSharedData(VehicleData.VEHICLE_SQL_ID, plate);

                            Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug " + name + " ausgeparkt.", 5000, "white", garage, "orange");
                        }
                    }
                }
                else
                {
                    Database.changeVehicleState(plate, 1);
                    p.TriggerEvent("garage:closegarage");

                    foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                    {
                        if (vehicle.NumberPlate == plate)
                        {
                            Database.changeVehiclePrimaryColor(plate, vehicle.CustomPrimaryColor);
                            Database.changeVehicleSecondaryColor(plate, vehicle.CustomSecondaryColor);

                            vehicle.Delete();
                            Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug " + name + " eingeparkt.", 5000, "white", garage, "orange");
                        }
                    }

                }
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }


    }
}
