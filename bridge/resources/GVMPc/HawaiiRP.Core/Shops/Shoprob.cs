using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Timers;


namespace GVMPc.Shops
{
	public class Shoprob : Script
	{
		public static Dictionary<string, Vector3> points = new Dictionary<string, Vector3>();

		public static List<Client> robbingPlayer = new List<Client>();

		public int price = (int)new Random().Next(15000, 25000);

		[ServerEvent(Event.ResourceStart)]
		public void onResourceSStart()
		{
			if (points.Count < 1)
			{
				points.Add("Vespucci Shop", new Vector3(-709.5929, -904.1713, 18.11559));

				foreach (KeyValuePair<string, Vector3> point in points)
				{
					NAPI.Marker.CreateMarker(1, point.Value, new Vector3(), new Vector3(), 1.5f, new Color(255, 0, 0), false, 0);
					ColShape col = NAPI.ColShape.CreateCylinderColShape(point.Value, 2f, 2f, 0);
					col.SetData("COLSHAPE_FUNCTION", new FunctionModel("robShop"));
					col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Shop auszurauben", point.Key + " - SHOP", "green", 5000));
				}
			}
		}

		[RemoteEvent("robShop")]
		public void robShop(Client p)
		{
			try
			{
				Notification.SendPlayerNotifcation(p, "Test", 5000, "red", "", "");
			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
		}
	}
}
