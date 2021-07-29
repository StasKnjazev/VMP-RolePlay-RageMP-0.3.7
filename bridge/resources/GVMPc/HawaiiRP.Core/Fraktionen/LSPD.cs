using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Fraktionen
{
	class LSPD : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(60, new Vector3(433.5071, -982.0235, 29.60974), 1.0f, 29, "LSPD", 255, 0, true, 0, uint.MaxValue);
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(452.0649, -980.0495, 29.58961), 1.5f, 1.5f, uint.MaxValue);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openWaffenKammer"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um auf die Waffenkammer zuzugreifen", "LSPD", "blue", 4500));
			ColShape val1 = NAPI.ColShape.CreateCylinderColShape(new Vector3(450.0993, -992.91, 29.58959), 1.5f, 1.5f, uint.MaxValue);
			val1.SetData("COLSHAPE_FUNCTION", new FunctionModel("openUmkleide"));
			val1.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um auf den Kleiderschrank zuzugreifen", "LSPD", "blue", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(994.627, -3099.963, -40.09587), 1.5f, 1.5f, uint.MaxValue);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("openAservaten"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um auf die Aservatenkammer zuzugreifen", "LSPD", "blue", 4500));
			ColShape val3 = NAPI.ColShape.CreateCylinderColShape(new Vector3(1842.205, 2582.378, 44.79098), 1.5f, 1.5f, uint.MaxValue);
			val3.SetData("COLSHAPE_FUNCTION", new FunctionModel("openSpind"));
			val3.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um auf deinen Spind zuzugreifen", "LSPD", "blue", 4500));
		}

		[RemoteEvent("openWaffenKammer")]
		public void openWaffenKammer(Client p)
		{
			if(p.GetSharedData("FRAKTION") == "Los Santos Police Department")
			{
				NativeMenu nativeMenu = new NativeMenu("Waffen", "Waffen", new List<NativeItem>()
				{
					new NativeItem("Dienst Betreten", "gocduty"),
					new NativeItem("Dienst verlassen", "outcduty"),
					new NativeItem("Umziehen", "clothing"),
					new NativeItem("Schutzwesten", "armors"),
					new NativeItem("Waffen", "weapons"),
					new NativeItem("Items", "items"),
					new NativeItem("Munition", "munition")
				});
				nativeMenu.showNativeMenu(p);
			} else
			{
				Notification.SendPlayerNotifcation(p, "Du bist nicht berechtigt dazu", 4500, "blue", "LSPD", "");
			}
		}

		[RemoteEvent("nM-Waffen")]
		public void waffenKammer(Client p, string selection)
		{
			if(selection == "gocduty")
			{
				if (p.GetData("CDUTY") == true)
				return;

				if(p.GetSharedData("FRAKTION") == "Los Santos Police Department")
				{
					Notification.SendPlayerNotifcation(p, "Du bis in den Dienst gegangen", 4500, "blue", "LSPD", "blue");
					p.TriggerEvent("setPlayerCduty", true);
					p.SetData("CDUTY", true);
					foreach(Client client in NAPI.Pools.GetAllPlayers())
					{
						if(Database.isPlayerInFrak(client, "Los Santos Police Department"))
						{
							Notification.SendPlayerNotifcation(client, p.Name + " hat sich dem Dienst mit Rang " + Database.getUserFraktionRank(p.Name) + " angemeldet", 4500, "blue", "LSPD", "");
						}
					}
				}
			} else if(selection == "outcduty")
			{
				if (p.GetData("CDUTY") == true)
				{
					Notification.SendPlayerNotifcation(p, "Du bis aus den Dienst gegangen", 4500, "blue", "LSPD", "blue");
					p.TriggerEvent("setPlayerCduty", false);
					p.SetData("CDUTY", false);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht im Dienst", 4500, "blue", "LSPD", "blue");
				}

			} else if(selection == "clothing")
			{
				if(p.GetData("CDUTY"))
				{
					p.SetClothes(4, 24, 0);
					p.SetClothes(11, 55, 0);
					p.SetClothes(6, 7, 0);
					p.SetClothes(8, 58, 0);
					Notification.SendPlayerNotifcation(p, "Du hast dich umgezogen", 5000, "blue", "LSPD", "");
				}
			}
			else if(selection == "armors")
			{
				if (p.GetData("CDUTY") == true)
				{
					NativeMenu nativeMenu = new NativeMenu("Schutzwesten", "Westen", new List<NativeItem>()
				    {
					new NativeItem("Task-Force Weste", "gweste"),
					new NativeItem("Police Weste", "pweste"),
				    });
					nativeMenu.showNativeMenu(p);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht im Dienst", 4500, "blue", "LSPD", "blue");
				}

			} else if(selection == "weapons")
			{
				if (p.GetData("CDUTY") == true)
				{

					NativeMenu nativeMenu = new NativeMenu("Waffen", "Waffen", new List<NativeItem>()
				    {
					new NativeItem("Combat PDW", "pdw"),
					new NativeItem("Pistole", "pistol"),
					new NativeItem("Taser", "tazer")
				    });
					nativeMenu.showNativeMenu(p);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht im Dienst", 4500, "blue", "LSPD", "blue");
				}

			} else if(selection == "items")
			{
				if (p.GetData("CDUTY") == true)
				{
					NativeMenu nativeMenu = new NativeMenu("Items", "Items", new List<NativeItem>()
				    {
					new NativeItem("Handschellen", "cuffs"),
					new NativeItem("Schalldämpfer", "suppresor"),
					new NativeItem("Magazinerweiterung", "magazin")
				    });
					nativeMenu.showNativeMenu(p);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht im Dienst", 4500, "blue", "LSPD", "blue");
				}

			} else if(selection == "munition")
			{
				if (p.GetData("CDUTY") == true)
				{
					NativeMenu nativeMenu = new NativeMenu("Munition", "Munition", new List<NativeItem>()
				    {
					new NativeItem("Munition PDW", "mpdw"),
					new NativeItem("Munition Pistole", "mpistol"),
				    });
					nativeMenu.showNativeMenu(p);
				}
			}
		}

		[RemoteEvent("nM-Schutzwesten")]
		public void schutzwesten(Client p, string selection)
		{
			if(selection == "gweste")
			{
				Database.changeInventoryItem(p.Name, "TaskForce-Weste", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir eine Task Force Weste genommen", 4500, "blue", "LSPD", "");

			} else if(selection == "pweste")
			{
				Database.changeInventoryItem(p.Name, "Police-Weste", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir eine Police  Weste genommen", 4500, "blue", "LSPD", "");
			}
		}

		[RemoteEvent("nM-Waffen")]
		public void wepaons(Client p, string selection)
		{
			if(selection == "pdw")
			{
				Database.changeInventoryItem(p.Name, "PDW", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir eine Combat PDW genommen", 4500, "blue", "LSPD", "");

			} else if(selection == "pistol")
			{
				Database.changeInventoryItem(p.Name, "Pistol", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir eine Pistole genommen", 4500, "blue", "LSPD", "");

			} else if(selection == "tazer")
			{
				Database.changeInventoryItem(p.Name, "Taser", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir einen Taser genommen", 4500, "blue", "LSPD", "");

			}
		}

		[RemoteEvent("nM-Munition")]
		public void munition(Client p, string selection)
		{

			if(selection == "mpdw")
			{
				Database.changeInventoryItem(p.Name, "CombatPDWAmmo", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir Combat PDW Munition genommen", 4500, "blue", "LSPD", "");


			} else if(selection == "mpistol")
			{
				Database.changeInventoryItem(p.Name, "AmmoPistol", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir Pistolen Munition genommen", 4500, "blue", "LSPD", "");

			}
		}

		[RemoteEvent("nM-Items")]
		public void items(Client p, string selection)
		{
			if(selection == "cuffs")
			{
				Database.changeInventoryItem(p.Name, "Fesseln", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir Fesseln genommen", 4500, "blue", "LSPD", "");
			} else if(selection == "suppresor")
			{
				Database.changeInventoryItem(p.Name, "Schalldaempfer", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir einen Schalldämpfer genommen", 4500, "blue", "LSPD", "");
			} else if(selection == "magazin")
			{
				Database.changeInventoryItem(p.Name, "Magazin", 1, false);
				Notification.SendPlayerNotifcation(p, "Du hast dir eine Magazinerweiterung genommen", 4500, "blue", "LSPD", "");
			}
		}

		[RemoteEvent("openAservaten")]
		public void openAservaten(Client p)
		{
			try
			{
				if(p.GetSharedData("FRAKTION") == "Los Santos Police Department")
				{


					p.SetData("LAST_INVENTORY", "ASERVATENKAMMER");
					p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":12,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getFraktionSQLId(p.GetSharedData("FRAKTION")) + ",\"Name\":\"Aservatenkammer\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":44,\"Slots\":" + NAPI.Util.ToJson(Database.getFraklagerItems("Los Santos Police Department")) + "}]}");

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist kein Officer", 4500, "red", "LSPD", "");
				}


			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("openSpind")]
		public void openSpind(Client p)
		{
			try
			{
				p.SetData("LAST_INVENTORY", "SPIND");
				p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":12,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getSpindSQLId(p.Name) + ",\"Name\":\"Spind\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getSpindItems(p.Name)) + "}]}");

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

	}
}
