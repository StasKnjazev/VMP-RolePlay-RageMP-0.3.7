using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Timers;

namespace GVMPc.Business
{
	public class ÖlPumpenRegister : Script
	{
		public static List<ÖlPumpenModel> oilpumps = new List<ÖlPumpenModel>();

		public static Timer oilTimer;

		[ServerEvent(Event.ResourceStart)]
		public void Start()
		{
			oilTimer = new Timer(1 * 30000);
			oilTimer.AutoReset = true;
			oilTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				OilTimerSpent(null);
			};
			oilTimer.Start();

			if (oilpumps.Count < 1)
			{
				foreach (ÖlPumpenModel pumpenModel in Database.getOilList())
					oilpumps.Add(pumpenModel);
			}

			foreach (ÖlPumpenModel ölPumpen in oilpumps)
				loadOilPumps(ölPumpen);
		}

		public void loadOilPumps(ÖlPumpenModel pumpenModel)
		{
			ColShape col = NAPI.ColShape.CreateCylinderColShape(pumpenModel.position, 3f, 2f, 0);
			col.SetData("COLSHAPE_FUNCTION", new FunctionModel("buyRaffiner"));
			if (pumpenModel.businessname == "STAAT")
			{
				col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die Pumpe zu kaufen", "OILPUMPE", "black", 5000));
			} else
			{
				col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um mit der Pumpe zu interagieren", "OILPUMPE", "black", 5000));
			}
		}

		[RemoteEvent("buyRaffiner")]
		public void buyRaffinere(Client p)
		{
			foreach(ÖlPumpenModel pumpenModel in oilpumps)
			{
				if(p.Position.DistanceTo(pumpenModel.position) < 3f)
				{
					if(pumpenModel.businessname != p.GetSharedData("BUSINESS") && pumpenModel.businessname == "STAAT")
					{
						if (p.GetSharedData("BUSINESS_RANK") > 10 && p.GetSharedData("BUSINESS_RANK") != 10 && Database.getPlayerBusiness(p.Name) != "")
						{
							p.TriggerEvent("openWindow", new object[2]
							{
								"Confirmation",
								"{\"confirmationObject\":{\"Title\":\"Ölpumpenkauf | " + pumpenModel.cost +"$\",\"Message\":\"Möchtest du die Ölpumpe für " + pumpenModel.cost +"$ kaufen?\",\"Callback\":\"buyOilPump\",\"Arg1\":\"\",\"Arg2\":\"\"}}"
							});
						} else
						{
							Notification.SendPlayerNotifcation(p, "Du bist nicht berechtigt dazu!", 5000, "red", "ÖLPUMPE", "");
						}

					} else if(pumpenModel.businessname == p.GetSharedData("BUSINESS"))
					{
						p.SetData("LAST_INVENTORY", "OIL");
						p.TriggerEvent("openWindow", "Inventory", "{\"inventory\":[{\"Id\":" + Database.getSQLId(p.Name) + ",\"Name\":\"Inventar\",\"Money\":" + Database.getMoney(p.Name) + ",\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":40000,\"MaxSlots\":6,\"Slots\":" + NAPI.Util.ToJson(Database.getUserItems(p.Name)) + "}, {\"Id\":" + Database.getSpindSQLId(p.Name) + ",\"Name\":\"Ölpumpe - Resourcen\",\"Money\":0,\"Blackmoney\":0,\"Weight\":0,\"MaxWeight\":275000,\"MaxSlots\":12,\"Slots\":" + NAPI.Util.ToJson(Database.getOilItems(p.GetSharedData("BUSINESS"))) + "}]}");
					}
				}
			}
		}

		[RemoteEvent("buyOilPump")]
		public void buyRaffiner(Client p)
		{
			foreach(ÖlPumpenModel pumpenModel in oilpumps)
			{
				if(p.Position.DistanceTo(pumpenModel.position) < 3f)
				{
					if (Database.getBusinessMoney(p.GetSharedData("BUSINESS")) >= pumpenModel.cost)
					{
						Database.updateOilOwner(pumpenModel.id, p.GetSharedData("BUSINESS"));
						Database.changeBusinessMoney(p.Name, pumpenModel.cost, true);
						pumpenModel.businessname = p.GetSharedData("BUSINESS");
						foreach(Client c in NAPI.Pools.GetAllPlayers())
						{
							if (c.GetSharedData("BUSINESS") == p.GetSharedData("BUSINESS"))
							{
								Notification.SendPlayerNotifcation(c, "Das Business besitzt nun eine Ölpumpe", 5000, "white", "BUSINESS", "");
							}
						}
					} else
					{
						Notification.SendPlayerNotifcation(p, "Dein Business hat nicht genug Geld!", 5000, "red", "BUSINESS", "");
					}
				}
			}
		}

		public void OilTimerSpent(object unused)
		{
			foreach (Client p in NAPI.Pools.GetAllPlayers())
			{
				if (Database.getPlayerBusiness(p.Name) != "null")
				{
					Database.changeOilItem(p.GetSharedData("BUSINESS"), "Benzin", 1, false);
				}
			}
		}
	}
}
