using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Timers;

namespace GVMPc.Banking
{
	public class FleecaRaub : Script
	{
		public static List<Client> robbingPlayer = new List<Client>();

		public static Timer RobTimer;

		public static Dictionary<string, Vector3> points = new Dictionary<string, Vector3>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			RobTimer = new Timer(3 * 60000);
			RobTimer.AutoReset = true;
			RobTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
			{
				Robbing(null);
			};

			if (points.Count < 1)
			{
				points.Add("Fleecabank Burton", new Vector3(-353.2838, -54.10724, 47.93658));
			}

			foreach(KeyValuePair<string, Vector3> point in points)
			{
				NAPI.Marker.CreateMarker(1, point.Value, new Vector3(), new Vector3(), 2f, new Color(255, 0, 0), false, 0);
				ColShape val = NAPI.ColShape.CreateCylinderColShape(point.Value, 2f, 2f, 0);
				val.SetData("COLSHAPE_FUNCTION", new FunctionModel("robBank"));
				val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die Bank auszurauben", point.Key.ToString(), "green", 5000));
				ColShape val2 = NAPI.ColShape.CreateCylinderColShape(point.Value, 20f, 5f, 0);
				val2.SetData("IS_BANK", true);
			}

			Log.Write(points.Count + " Fleecaräube geladen");
		}

		[ServerEvent(Event.PlayerExitColshape)]
		public void playerExitColshape(ColShape colShape, Client p)
		{
			if (!p.HasData("IS_ROBBING"))
				return;
			RobTimer.Stop();
			p.SetData("IS_ROBBING", false);
			robbingPlayer.Remove(p);
			Notification.SendPlayerNotifcation(p, "Da du dich zuweit von der Bank entfernt hast wurde der Raub abgebrochen", 5000, "red", "Fleeca", "");
		}

		[RemoteEvent("robBank")]
		public void robBank(Client p)
		{
			if(!p.HasData("IS_ROBBING"))
			{
				p.SetData("IS_ROBBING", true);
				Notification.SendPlayerNotifcation(p, "Du Raubst gerade die Flecca Bank aus! Die Polizei wird gerade alamiert!", 5000, "red", "Fleeca", "");
				RobTimer.Start();
				robbingPlayer.Add(p);
				foreach(Client c in NAPI.Pools.GetAllPlayers())
				{
					if(Database.isPlayerInFrak(c, "Los Santos Police Department"))
					{
						foreach(KeyValuePair<string, Vector3> point in points)
						{
							if(p.Position.DistanceTo(point.Value) < 4f)
							{
								Notification.SendPlayerNotifcation(c, "Die " + point.Key + " wird gerade ausgeraubt!", 5000, "darkgreen", "Fleeca", "");
							}
						}
					}
				}
			}
		}

		public void Robbing(object unused)
		{
			int num = new Random().Next(35000, 20000);

			foreach(Client p in robbingPlayer)
			{
				if(robbingPlayer.Contains(p))
				{
					Database.changeBlackMoney(p.Name, num, false);
					Notification.SendPlayerNotifcation(p, "Du hast " + num + "$ Ungewaschenes Geld bekommen", 5000, "red", "Fleeca", "");
					RobTimer.Stop();
					p.SetData("IS_ROBBING", false);
					robbingPlayer.Remove(p);
				}
			}
		}
	}
}
