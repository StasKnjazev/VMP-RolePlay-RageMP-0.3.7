using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Other
{
	class FIB : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(138.9803, -762.7933, 44.652), 1.5f, 2f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openElevator"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Dücke E um den Aufzug zu benutzen", "FIB", "grey", 4500));
			ColShape val1 = NAPI.ColShape.CreateCylinderColShape(new Vector3(136.1656, -761.6975, 241.052), 1.5f, 2f, 0);
			val1.SetData("COLSHAPE_FUNCTION", new FunctionModel("openElevator"));
			val1.SetData("COLSHAPE_MESSAGE", new Notification.Message("Dücke E um den Aufzug zu benutzen", "FIB", "grey", 4500));
			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(156.7546, -757.228, 257.052), 1.5f, 2f, 0);
			val2.SetData("COLSHAPE_FUNCTION", new FunctionModel("openElevator"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Dücke E um den Aufzug zu benutzen", "FIB", "grey", 4500));
		}

		[RemoteEvent("openElevator")]
		public void openElevator(Client p)
		{
			try
			{
				NativeMenu nativeMenu = new NativeMenu("Aufzug", "Stockwerke", new List<NativeItem>()
				{
					new NativeItem("Eingangshalle", "openhall"),
					new NativeItem("49 - Stock", "firstfloor"),
					new NativeItem("Oberster Stock", "topfloor")
				});
			      nativeMenu.showNativeMenu(p);

				}catch (Exception ex)
			{
				Log.Write(ex.Message);
				Log.Write(ex.StackTrace);
			}
		}

		[RemoteEvent("nM-Aufzug")]
		public void aufzug(Client p, string selection)
		{
			if(selection == "openhall")
			{
				if (Database.isPlayerInFrak(p, "Federal Investigation Bureau"))
				{
					p.Position = new Vector3(138.9803, -762.7933, 44.652).Add(new Vector3(0, 0, 1.5));
					return;

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht in der Fraktion", 4500, "grey", "", "");
				}

			} else if(selection == "firstfloor")
			{
				if (Database.isPlayerInFrak(p, "Federal Investigation Bureau"))
				{
					p.Position = new Vector3(136.1656, -761.6975, 241.052).Add(new Vector3(0, 0, 1.5));
				    return;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht in der Fraktion", 4500, "grey", "", "");
				}

			} else if(selection == "topfloor")
			{
				if (Database.isPlayerInFrak(p, "Federal Investigation Bureau"))
				{
					p.Position = new Vector3(156.7546, -757.228, 257.052).Add(new Vector3(0, 0, 1.5));
				    return;
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du bist nicht in der Fraktion", 4500, "grey", "", "");
				}
			}
		}

	}
}
