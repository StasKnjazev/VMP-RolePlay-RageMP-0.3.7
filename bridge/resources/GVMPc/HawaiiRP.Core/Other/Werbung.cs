using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
	class Werbung : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(280, new Vector3(-1082.032, -247.548, 36.66324), 1f, 1, "Lifeinvader", 255, 0, true, 0, 0);
			ColShape col = NAPI.ColShape.CreateCylinderColShape(new Vector3(-1082.032, -247.548, 36.66324), 2f, 2f, 0);
			col.SetData("COLSHAPE_FUNCTION", new FunctionModel("createAd"));
			col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um Werbung zu schalten", "LIFEINVADER", "red", 5000));
		}

		[RemoteEvent("createAd")]
		public void createAd(Client p)
		{
			p.TriggerEvent("openWindow", new object[2]
			{
				"TextInputBox",
				"{\"textBoxObject\":{\"Title\":\"Idk\",\"Message\":\"21091\",\"Callback\":\"CREATE_AD\",\"CustomData\":{\"Number\":\""+ Database.getUserPhoneNumber(p.Name).ToString() +"\"}}}"
			});
			p.TriggerEvent("componentReady", "TextInputBox");
		}

		[RemoteEvent("CREATE_AD")]
		public void CREATE_ADS(Client p, string text)
		{
			int price = new Random().Next(1500, 2700);

			foreach (Client client in NAPI.Pools.GetAllPlayers())
			{
				Notification.SendPlayerNotifcation(client, "Es wurde eine Werbung geschaltet. Checke die Lifeinvader App.", 5000, "yellow", "Lifeinvader", "");
			}

			Handy.LifeInvaderApp.addWerbung(p, text);
		}
	}
}
