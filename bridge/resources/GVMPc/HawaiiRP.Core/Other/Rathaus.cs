using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
	class Rathaus : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(419, new Vector3(-549.4846, -190.1301, 37.12965), 1f, 0, "Anmeldeamt", 255, 0, true, 0, 0);
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(-549.4846, -190.1301, 37.12965), 2.5f, 2f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("getPerso"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um deinen Personalausweis anzufordern", "ANMELDEAMT", "grey", 4500));

		}

		[RemoteEvent("getPerso")]
		public void getPerso(Client p)
		{
			try
			{
				if(Database.getItemCount(p.Name, "Personalausweis") < 1)
				{
					p.TriggerEvent("openWindow", new object[2]
	                {
					    "Confirmation",
						"{\"confirmationObject\":{\"Title\":\" Beantragung | Personalausweis\",\"Message\":\"Möchtest du dir für 5.000$ einen Ausweis erstellen?\",\"Callback\":\"acceptPerso\",\"Arg1\":\"\",\"Arg2\":\"\"}}"
	                });

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast schon einen Personalausweis", 4500, "red", "ANMELDEAMT", "");
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("acceptPerso")]
		public void acceptPerso(Client p, object unused, object unuseable)
		{
			try
			{
				Database.changeInventoryItem(p.Name, "Personalausweis", 1, false);
				Database.changeMoney(p.Name, 5000, true);
				Notification.SendPlayerNotifcation(p, "Du hast dir für 5.000$ einen Ausweis erstellt", 4500, "grey", "ANMELDEAMT", "");

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

	}
}
