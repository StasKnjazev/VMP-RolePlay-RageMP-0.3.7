using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Tattoo
{
	public class TattooRegister : Script
	{
		public static Dictionary<string, Vector3> points = new Dictionary<string, Vector3>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			if(points.Count < 1)
			{
				points.Add("Vespucci Tattooladen", new Vector3(-1155.644, -1427.01, 3.854458));
			}

			foreach(KeyValuePair<string, Vector3> point in points)
			{
				NAPI.Blip.CreateBlip(71, point.Value, 1f, 0, point.Key, 255, 0, true, 0, 0);
				ColShape col = NAPI.ColShape.CreateCylinderColShape(point.Value, 2f, 2f, 0);
				col.SetData("COLSHAPE_FUNCTION", new FunctionModel("openTattooLaden"));
				col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Tattooladen zu benutzen", point.Key, "orange", 5000));
			}
		}

		[RemoteEvent("openTattooLaden")]
		public void openTattooLaden(Client p)
		{
			p.TriggerEvent("openWindow", new object[2]
			{
				"Barber",
				"[{\"hairs\":[{\"id\":\"1\",\"name\":\"male_Glatze\",\"CustomisationId\":\"1\",\"Price\":\"0\",\"Gender\":\"0\"}, {\"id\":\"2\",\"name\":\"female_Glatze\",\"CustomisationId\":\"2\",\"Price\":\"100\",\"Gender\":\"0\"}],\"colors\":[{\"id\":\"1\",\"Name\":\"Braun\",\"CustomisationId\":\"1\",\"Price\":\"5000\"}]}]"
			});
		}
	}
}
