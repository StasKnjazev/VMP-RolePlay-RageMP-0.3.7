using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
    class Inventory : Script
    {
        public static bool isNearby(Client client, Vehicle veh)
        {
            return veh.Position.DistanceTo(client.Position) <= 10f;
        }

		public static int InventoryWeight = 40000;

        public static Vehicle GetClosestVehicle(Client sender, float distance = 1000f)
        {
            Vehicle result = null;
            foreach (Vehicle allVehicle in NAPI.Pools.GetAllVehicles())
            {
                Vector3 entityPosition = NAPI.Entity.GetEntityPosition(allVehicle);
                float num = sender.Position.DistanceTo(entityPosition);
                if (num < distance)
                {
                    distance = num;
                    result = allVehicle;
                }
            }
            return result;
        }

		[RemoteEvent("requestInventory")]
		public static void requestItems(Client p)
		{
			try
			{
				if (!Start.deathTime.ContainsKey(p))
				{
					Vehicle closestVehicle = GetClosestVehicle(p, 5f);
					bool flag = false;
					if (closestVehicle != null)
					{
						if (closestVehicle.NumberPlate != null && closestVehicle.HasSharedData("kofferraumStatus"))
						{
							if (closestVehicle.Locked == false && closestVehicle.GetSharedData("kofferraumStatus") == true ? true : false)
							{
								flag = true;
							}
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
					if (!flag)
					{
						if (p.Position.DistanceTo(new Vector3(1712.6771, 4766.299, 13.110)) > 20.0f)
						{
							if (Items.Alicepack.AliceBool == false)
							{
								p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":" + Database.getBlackMoney(p.Name) + ",\"Weight\":" + Database.getItemWeight(p.Name) + ",\"MaxWeight\":15000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}]}");

							}
							else
							{
								p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":" + Database.getBlackMoney(p.Name) + ",\"Weight\":" + Database.getItemWeight(p.Name) + ",\"MaxWeight\":25000,\"MaxSlots\":8,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}]}");
							}
							return;
						}
						else if (p.Position.DistanceTo(new Vector3(1712.6771, 4766.299, 13.110)) < 20.0f)
						{
							if (p.GetSharedData("FRAKTION") == "Zivilist")
								return;

							p.SetData("LAST_INVENTORY", "Fraktionslager | " + p.GetSharedData("FRAKTION_SHORT"));
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getFraktionSQLId(p.GetSharedData("FRAKTION")) + ",\"Name\":\"Fraktionslager\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":22,\"Slots\":" + NAPI.Util.ToJson(Database.getFraklagerItems(p.GetSharedData("FRAKTION"))) + "}]}");

						}

					}
					else
					{
						if (closestVehicle.NumberPlate == null)
							return;

						if (closestVehicle.Model == (uint)VehicleHash.Benson)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":580000,\"MaxSlots\":22,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Nero2)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":50000,\"MaxSlots\":5,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Nero)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":50000,\"MaxSlots\":5,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Burrito)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":135000,\"MaxSlots\":12,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Speedo)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":100000,\"MaxSlots\":10,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Speedo2)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":100000,\"MaxSlots\":10,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Speedo4)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":100000,\"MaxSlots\":10,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Adder)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":170000,\"MaxSlots\":3,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Oracle)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":170000,\"MaxSlots\":3,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Panto)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":10000,\"MaxSlots\":2,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else if (closestVehicle.Model == (uint)VehicleHash.Schafter2)
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":450000,\"MaxSlots\":8,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");

						}
						else
						{
							p.SetData("LAST_INVENTORY", "Kofferraum | " + closestVehicle.NumberPlate);
							p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getVehicleSQLId(closestVehicle.NumberPlate) + ",\"Name\":\"Kofferraum\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":45000,\"MaxSlots\":5,\"Slots\":" + NAPI.Util.ToJson(Database.getKofferraumItems(closestVehicle.NumberPlate)) + "}]}");
						}
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist gestorben!", 5000, "red", "", "");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("moveItemInInventory")]
		public void moveItemInInventory(Client p, int oldSlot, int newSlot, string ItemName, int Amount)
		{
			try
			{
				if (Amount > 0)
				{
					if (Database.getItemCount(p.Name, ItemName) >= Amount)
					{
						Database.changeInventoryItemSlot(p.Name, ItemName, newSlot);
					}
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("requestPlayerKeys")]
		public void requestPlayerKeys(Client p)
		{
			try
			{
				p.TriggerEvent("openWindow", new object[2]
							{
				  "Keys",
				  "{\"id\":\"2\"}"
							});

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("packblackmoney")]
		public void packblackmoney(Client p)
		{
			if (p.GetData("IS_PACKINGBLACKMONEY") == true)
				return;

			p.SetData("IS_PACKINGBLACKMONEY", true);
			Functions.disableAllPlayerControls(p, true);
			p.TriggerEvent("sendProgressbar", 10000);
			NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
			NAPI.Task.Run(delegate
			{
				Functions.disableAllPlayerControls(p, false);
				Database.changeBlackMoney(p.Name, 25000, true);
				Database.changeInventoryItem(p.Name, "Schwarzgeld", 25000, false);
				Notification.SendPlayerNotifcation(p, "Du hast " + 25000 + " Schwarzgeld gepackt", 5000, "red", "", "");
				NAPI.Player.StopPlayerAnimation(p);
				p.SetData("IS_PACKINGBLACKMONEY", false);

			}, 10000);

		}

		[RemoteEvent("moveItemToInventory")]
        public void moveItemToInventory(Client p, int oldSlot, int newSlot, string ItemName, int Amount)
        {
            try
            {

				if (p.HasData("LAST_INVENTORY"))
				{
					if (p.GetData("LAST_INVENTORY").Contains("Fraktionslager"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{

							if (!p.HasSharedData("FRAKTION"))
								return;

							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							if (item.Weight * Amount >= 15000)
							{
								Notification.SendPlayerNotifcation(p, "Du hast zuviel im Rucksack", 4500, "red", "INVENTAR", "red");
							}
							else
							{
								Database.changeInventoryItem(p.Name, item.Name, Amount, true);
								Database.changeFraklagerItem(p.GetSharedData("FRAKTION"), item.Name, Amount, false);
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								NAPI.Task.Run(delegate
								{
									NAPI.Player.StopPlayerAnimation(p);
								}, 1800L);
							}

							p.TriggerEvent("closeWindow", "Inventory");
							requestItems(p);
						}
						else
						{
							//IN PLAYER INV
							if (!p.HasSharedData("FRAKTION"))
								return;

							Items.ItemModel item2 = Database.getFraklagerItems((string)p.GetSharedData("FRAKTION")).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeFraklagerItem(p.GetSharedData("FRAKTION"), item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							p.TriggerEvent("closeWindow", "Inventory");
							requestItems(p);
						}
					}
					else if (p.GetData("LAST_INVENTORY").Contains("Kofferraum"))
					{

						int InventoryId = int.Parse(ItemName);
						string numberPlate = (string)p.GetData("LAST_INVENTORY").Replace("Kofferraum | ", "");

						if (InventoryId == Database.getSQLId(p.Name))
						{

							if (!p.HasSharedData("FRAKTION") || numberPlate == null)
								return;

							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeKofferraumItem(numberPlate, item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							p.TriggerEvent("closeWindow", "Inventory");
							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							if (!p.HasSharedData("FRAKTION") || numberPlate == null)
								return;

							Items.ItemModel item2 = Database.getKofferraumItems(numberPlate).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeKofferraumItem(numberPlate, item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
					}
					else if (p.GetData("LAST_INVENTORY").Contains("ASERVATENKAMMER"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{

							if (!p.HasSharedData("FRAKTION"))
								return;

							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeFraklagerItem("Los Santos Police Department", item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV
							if (!p.HasSharedData("FRAKTION"))
								return;

							Items.ItemModel item2 = Database.getFraklagerItems("Los Santos Police Department").Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeFraklagerItem("Los Santos Police Department", item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
					}
					else if (p.GetData("LAST_INVENTORY").Contains("SPIND"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{
							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeSpindItem(p.Name, item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							Items.ItemModel item2 = Database.getSpindItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeSpindItem(p.Name, item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
					}
					else if (p.GetData("LAST_INVENTORY").Contains("STAATSBANK"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{
							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeTresorItem("Staatsbank", item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							Items.ItemModel item2 = Database.getTresorItems("Staatsbank").Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeTresorItem("Staatsbank", item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}

					}
					else if (p.GetData("LAST_INVENTORY").Contains("HOUSELAGER"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{
							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeHouseItem(p.Name, item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							Items.ItemModel item2 = Database.getHouseItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeHouseItem(p.Name, item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}

					}
					else if (p.GetData("LAST_INVENTORY").Contains("KELLER"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{
							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeKellerItem(p.Name, item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							Items.ItemModel item2 = Database.getKellerItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeKellerItem(p.Name, item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}

					}
					else if (p.GetData("LAST_INVENTORY").Contains("WAREHOUSE"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{
							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeWareHouseItem(p.Name, item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							Items.ItemModel item2 = Database.getWareHouseItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeWareHouseItem(p.Name, item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}

					}
					else if (p.GetData("LAST_INVENTORY").Contains("RESOURCEN"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{
							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeLabResourceItem(Database.getPlayerFraktion(p.Name), item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							Items.ItemModel item2 = Database.getLabResourceItems(Database.getPlayerFraktion(p.Name)).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeLabResourceItem(Database.getPlayerFraktion(p.Name), item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}

					}
					else if (p.GetData("LAST_INVENTORY").Contains("PRODUKT"))
					{

						int InventoryId = int.Parse(ItemName);

						if (InventoryId == Database.getSQLId(p.Name))
						{
							Items.ItemModel item = Database.getUserItems(p.Name).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item == null)
								return;

							Database.changeInventoryItem(p.Name, item.Name, Amount, true);
							Database.changeLabProduktItem(Database.getPlayerFraktion(p.Name), item.Name, Amount, false);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}
						else
						{
							//IN PLAYER INV

							Items.ItemModel item2 = Database.getLabProduktItems(Database.getPlayerFraktion(p.Name)).Find((Items.ItemModel itemModel) => itemModel.Slot == oldSlot);

							if (item2 == null)
								return;

							Database.changeInventoryItem(p.Name, item2.Name, Amount, false);
							Database.changeLabProduktItem(Database.getPlayerFraktion(p.Name), item2.Name, Amount, true);
							NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
							NAPI.Task.Run(delegate
							{
								NAPI.Player.StopPlayerAnimation(p);
							}, 1800L);

							requestItems(p);
						}

					}
				}
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("useInventoryItem")]
        public void useInventoryItem(Client p, int Slot)
        {
            List<Items.ItemModel> itemModels = Database.getUserItems(p.Name);

            Items.ItemModel itemToUse = itemModels.Find((Items.ItemModel x) => x.Slot == Slot);

            if(itemToUse != null && itemToUse.Amount >= 1)
            {
                Items.ItemManager.useItem(p, itemToUse.Name);
            }
        }

        [RemoteEvent("dropInventoryItem")]
        public void dropInventoryItem(Client p, int Slot, int amount)
        {
            List<Items.ItemModel> itemModels = Database.getUserItems(p.Name);

            Items.ItemModel itemToUse = itemModels.Find((Items.ItemModel x) => x.Slot == Slot);

            if (itemToUse != null && itemToUse.Amount >= amount)
            {
                Database.changeInventoryItem(p.Name, itemToUse.Name, amount, true);
                Notification.SendPlayerNotifcation(p, "Du hast " + amount + (amount < 2 ? " Item" : " Items") + " weggeworfen.", 3500, "black", "", "");
                NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
                NAPI.Task.Run(delegate
                {
                    NAPI.Player.StopPlayerAnimation(p);
                }, 1800L);
            }
        }

        [RemoteEvent("fillInventorySlots")]
        public void fillInventorySlots(Client p, string oldInventar)
        {
            requestItems(p);
        }


        [RemoteEvent("giveInventoryItem")]
        public void giveItems(Client p, int Slot, int amount)
        {

            try
            {
                List<Items.ItemModel> itemModels = Database.getUserItems(p.Name);

                Items.ItemModel itemToUse = itemModels.Find((Items.ItemModel x) => x.Slot == Slot);

				foreach (Items.ItemModel playerItems in Database.getUserItems(p.Name)) {

					if (itemToUse != null && itemToUse.Amount >= amount)
					{


						Client target = null;
						float distance = 9999999.0f;

						foreach (Client p2 in NAPI.Pools.GetAllPlayers())
						{
							float distance2 = p.Position.DistanceTo(p2.Position);
							if (distance2 < distance && p2 != p && distance2 < 3)
							{
								target = p2;
								distance = distance2;
							}
						}

						if (playerItems.Weight > InventoryWeight)
						{
							Notification.SendPlayerNotifcation(p, "Du hast keine Platz mehr", 4500, "red", "INVENTAR", "red");
						}
						else
						{
							if (target != null)
							{
								//Discord.DiscordWebhooks.SendMessage("Spieler gibt Item.", "Der Spieler " + p.Name + " gibt Spieler " + target.Name + " " + count + "x " + name + ".", Discord.DiscordWebhooks.itemWebhook, "Item-Log");
								Database.changeInventoryItem(p.Name, itemToUse.Name, amount, true);
								Database.changeInventoryItem(Start.loggedInPlayers[target], itemToUse.Name, amount, false);
								Notification.SendPlayerNotifcation(p, "Du hast " + amount + "x " + itemToUse.Name + " an " + Start.loggedInPlayers[target] + " gegeben.", 3500, "green", "", "white");
								Notification.SendPlayerNotifcation(target, "Du hast " + amount + "x " + itemToUse.Name + " von " + p.Name + " bekommen.", 3500, "black", "", "white");
								NAPI.Player.PlayPlayerAnimation(p, 33, "anim@mp_snowball", "pickup_snowball");
								NAPI.Task.Run(delegate
								{
									NAPI.Player.StopPlayerAnimation(p);
								}, 1800L);
							}
							else
							{
								Notification.SendPlayerNotifcation(p, "Es wurde kein Spieler gefunden.", 3500, "red", "", "white");
							}
						}
					}
					else
					{
						Notification.SendPlayerNotifcation(p, "Du hast nicht genug Items.", 3500, "red", "", "white");
					}
				}
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
