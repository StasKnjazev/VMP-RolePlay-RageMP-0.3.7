using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Jobs
{
	class Müllmann : Script
	{
		public static Vector3 ausparkpunkt = new Vector3(-621.0654, -1639.921, 25.25431);

		public static Dictionary<string, Vector3> points = new Dictionary<string, Vector3>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			ColShape val = NAPI.ColShape.CreateCylinderColShape(ausparkpunkt, 2f, 2f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openTrasher"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um dir ein Müllfahrzeug zu holen", "MÜLLABFUHR", "green", 4500));
		}

		[RemoteEvent("openTrasher")]
		public void openTrasher(Client p)
		{
			try
			{
				if(p.GetSharedData("JOB") == "Müllmann")
				{
					p.Dimension = 1212;
					VehicleHash vehicleHash = NAPI.Util.VehicleNameToModel("trash2");
					NAPI.Vehicle.CreateVehicle(vehicleHash, new Vector3(-622.4147, -1654.007, 24.72508), 62.37702f, 1, 1, "TRASHER", 255, false, true, 1212);
					Notification.SendPlayerNotifcation(p, "Du musst 5 Mülltonnen sammeln um dein Geld zu erhalten", 4500, "green", "MÜLLABFUHR", "");

					if (points.Count < 1)
					{
						points.Add("müll1", new Vector3(-34.23358, -2201.972, 7.143262));
						points.Add("müll2", new Vector3(-1233.58, -1408.633, 3.176463));
						points.Add("müll3", new Vector3(488.5421, -1283.34, 28.31015));
						points.Add("müll4", new Vector3(-176.7817, -1307.511, 30.16437));
						points.Add("müll5", new Vector3(492.9851, -633.1098, 23.78665));

						foreach (KeyValuePair<string, Vector3> item in points)
						{
							ColShape val = NAPI.ColShape.CreateCylinderColShape(item.Value, 2f, 2f, 1212);
							val.SetData("COLSHAPE_FUNCTION", new FunctionModel("getTrash"));
							val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Müll aufzusammeln", "", "green", 4500));
							NAPI.Blip.CreateBlip(46, item.Value, 1f, 2, "Müllstelle", 255, 0, true, 0, 1212);

							NAPI.Task.Run(delegate
							{
								NAPI.ColShape.DeleteColShape(val);
							}, 150000);
						}
					}

				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("getTrash")]
		public void getTrash(Client p)
		{
			try
			{
				NAPI.Player.PlayPlayerAnimation(p, 33, "mini@repair", "fixing_a_player", 8f);
				p.SetData("IS_FARMING", true);
				Notification.SendPlayerNotifcation(p, "Du sammelst Müll", 4500, "green", "", "");
				p.TriggerEvent("sendProgressbar", new object[1]
				{
					7500
				});
				Functions.disableAllPlayerControls(p, true);
				NAPI.Task.Run(delegate
				{
					NAPI.Player.StopPlayerAnimation(p);
					p.SetData("IS_FARMING", false);
					Notification.SendPlayerNotifcation(p, "Du hast Müll in deinen Müllwagen gelegt", 4500, "green", "", "");
					Functions.disableAllPlayerControls(p, false);


				}, 7500);

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}
	}
}
