using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Juwelier
{
	public class JuwelierRaub : Script
	{
		public static Dictionary<string, Vector3> kästen = new Dictionary<string, Vector3>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			ColShape col = NAPI.ColShape.CreateCylinderColShape(new Vector3(-630.2047, -236.4428, 36.95706), 2f, 2f, 0);
			col.SetData("COLSHAPE_FUNCTION", new FunctionModel("robJuwe"));
			col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Juwelier auszurauben", "JUWEILIER", "white", 4500));

			if(kästen.Count < 1)
			{
				kästen.Add("Kasten 1", new Vector3(-627.9609, -233.8935, 36.95706));

				foreach(KeyValuePair<string, Vector3> point in kästen)
				{
					if (NAPI.Data.GetWorldData("JUWEL_ROB") == true)
					{
						ColShape col1 = NAPI.ColShape.CreateCylinderColShape(point.Value, 1f, 2f, 0);
						col1.SetData("COLSHAPE_FUNCTION", new FunctionModel("getJuwels"));
						col1.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die Juwelen zu stehlen!", "JUWELIER", "white", 5000));
					}
				}
			}
		}

		[RemoteEvent("robJuwe")]
		public void robJuwe(Client p)
		{
			if (!p.HasData("IS_ROBBING"))
			{
				Notification.SendPlayerNotifcation(p, "Test", 4500, "red", "", "");
				NAPI.Data.SetWorldData("JUWEL_ROB", true);
			}
		}
	}
}
