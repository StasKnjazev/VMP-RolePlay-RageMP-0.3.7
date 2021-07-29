using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
	public class Teleports : Script
	{
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(-1045.831, -2751.853, 20.26318), 2f, 2f, 0);
			val.SetData("COLSSHAPE_FUNCTION", new FunctionModel("teleportAiportIn"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um in den Airport zu gelangen", "AIRPORT", "grey", 4500));

			ColShape val2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(-1065.417, -2797.874, 26.60876), 2f, 2f, 0);
			val2.SetData("COLSSHAPE_FUNCTION", new FunctionModel("teleportAiportOut"));
			val2.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um aus dem Airport zu gelangen", "AIRPORT", "grey", 4500));

		}

		[RemoteEvent("teleportAiportIn")]
		public void teleportAirportIn(Client p)
		{
			try
			{
				p.Position = new Vector3(-1065.417, -2797.874, 26.60876).Add(new Vector3(0, 0, 1.5));
				p.Dimension = 0;

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("teleportAiportOut")]
		public void teleportAirportOut(Client p)
		{
			try
			{
				p.Position = new Vector3(-1045.831, -2751.853, 20.26318).Add(new Vector3(0, 0, 1.5));
				p.Dimension = 0;

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

	}
}
