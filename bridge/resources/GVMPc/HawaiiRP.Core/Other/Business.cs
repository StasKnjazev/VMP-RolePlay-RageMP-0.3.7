using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
	class Business : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(106, new Vector3(-83.54047, -835.4444, 39.45777), 1f, 1, "Business Tower", 255, 0, true, 0, 0);

			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(-83.54047, -835.4444, 39.45777), 2f, 2f, 0);
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um einen Business zu erwerben", "BUSINESS", "white", 5000));
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("getBusiness"));

		}

		[RemoteEvent("getBusiness")]
		public void getBusiness(Client p)
		{
			try
			{
				if (Database.getMoney(p.Name) > 549999)
				{
			 		p.TriggerEvent("openWindow", "TextInputBox",
							"{\"textBoxObject\":{\"Title\":\"Business erstellen | 550.000$\",\"Message\":\"Gebe einen Namen für deinen Business ein. Du musst dafüür eine Summe von 550.000$ zahlen.\",\"Callback\":\"CREATE_BUSINESS\"}}"
					);
					p.TriggerEvent("componentReady", "TextInputBox");
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Geld dabei", 4500, "white", "BUSINESS", "");
				}

			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("CREATE_BUSINESS")]
		public void createBusiness(Client p, string name)
		{
			try
			{
				Database.createBusiness(name);
				Database.changeMoney(p.Name, 550000, true);
				Database.setUserBusiness(p.Name, name);
				Database.setUserBusinessRank(p.Name, 12);
				Notification.SendPlayerNotifcation(p, "Du hast für 550000$ einen Business erworben", 4500, "white", "BUSINESS", "");

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
		}
	}
}
