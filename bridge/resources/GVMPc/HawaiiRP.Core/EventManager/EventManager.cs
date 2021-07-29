using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Timers;
using GVMPc.Menus;

namespace GVMPc.EventManager
{
	public class EventManager : Script
	{
		public static List<Client> playerTraining = new List<Client>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(311, new Vector3(-1200.992, -1568.275, 3.511231), 1f, 0, "TrainingsCenter", 255, 0, true, 0, 0);
			NAPI.Blip.CreateBlip(546, new Vector3(-429.4155, 1109.45, 326.5824), 1f, 0, "Eventpunkte Dealer", 255, 0, true, 0, 0);

			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(-1200.992, -1568.275, 3.511231), 12f, 3f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("trainPlayer"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um zu Trainieren", "Trainingscenter", "white", 4500));
			ColShape col = NAPI.ColShape.CreateCylinderColShape(new Vector3(-429.4155, 1109.45, 326.5824), 2f, 2f, 0);
			col.SetData("COLSHAPE_FUNCTION", new FunctionModel("openEventDealer"));
			col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um mit dem Event Dealer zu sprechen", "EventDealer", "white", 4500));
		}

		[RemoteEvent("trainPlayer")]
		public void trainPlayer(Client p)
		{
			try
			{
				if (p.HasData("IS_TRAINING"))
					return;

				if (playerTraining.Contains(p))
					return;

				if (playerTraining.Count > 5)
					Notification.SendPlayerNotifcation(p, "Es sind zuviele Spieler im Event", 4500, "white", "Trainingscenter", "");

				NAPI.Player.PlayPlayerAnimation(p, 33, "timetable@michael@on_sofaidle_a", "sit_sofa_a", 8);
				playerTraining.Add(p);
				p.SetData("IS_TRAINING", true);
				NAPI.Task.Run(delegate
				{
					p.ResetData("IS_TRAINING");
					playerTraining.Remove(p);
					NAPI.Player.StopPlayerAnimation(p);
					Database.changeUserEventPoints(p.Name, 1, false);
					Notification.SendPlayerNotifcation(p, "Du hast 1 Eventpunkt erhalten", 4500, "green", "TrainingsCenter", "");
				}, 6 * 1000);

			} catch (Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("openEventDealer")]
		public void openEventDealer(Client p)
		{
			NativeMenu nativeMenu = new NativeMenu("EventDealer", "Angebote", new List<NativeItem>()
			{
				new NativeItem("Kleidung", "clothes"),
				new NativeItem("Fahrzeuge", "vehicles"),
				new NativeItem("Geschenkbox", "present")
			});
			nativeMenu.showNativeMenu(p);
		}

		[RemoteEvent("nM-EventDealer")]
		public void eventDealer(Client p, string selection)
		{
			if(selection == "clothes")
			{
				NativeMenu nativeMenu = new NativeMenu("Kleidung", "Angebote", new List<NativeItem>()
				{
					new NativeItem("", "")
				});
				nativeMenu.showNativeMenu(p);

			} else if(selection == "vehicles")
			{
				NativeMenu nativeMenu = new NativeMenu("Fahrzeuge", "Angebote", new List<NativeItem>()
				{
					new NativeItem("", ""),
				});
				nativeMenu.showNativeMenu(p);

			} else if(selection == "present")
			{
				NativeMenu nativeMenu = new NativeMenu("Geschenkbox", "Angebot", new List<NativeItem>()
				{
					new NativeItem("Gelbe Geschenkbox - 500 Eventpunkte", "yellowpresent"),
					new NativeItem("Blaue Geschenkbox - 1000 Eventpunkte", "bluepresent"),
					new NativeItem("Grüne Geschenkbox - 1500 Eventpunkte", "greenpresent"),
					new NativeItem("Rote Geschenkbox - 2000 Eventpunkte", "redpresent")
				});
				nativeMenu.showNativeMenu(p);
			}
		}

		[RemoteEvent("nM-Geschenkbox")]
		public void nMPresents(Client p, string selection)
		{
			if(selection == "yellowpresent")
			{
				if(Database.getEventPoints(p.Name) >= 500)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dir ein Gelbes Geschenk gekauft", 4500, "white", "EventDealer", "");
					Database.changeUserEventPoints(p.Name, 500, true);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Event Punkte", 4500, "red", "EventDealer", "");
				}

			} else if(selection == "bluepresent")
			{
				if (Database.getEventPoints(p.Name) >= 1000)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dir ein Blaues Geschenk gekauft", 4500, "white", "EventDealer", "");
					Database.changeUserEventPoints(p.Name, 1000, true);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Event Punkte", 4500, "red", "EventDealer", "");
				}

			} else if(selection == "greenpresent")
			{
				if (Database.getEventPoints(p.Name) >= 1500)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dir ein Grünes Geschenk gekauft", 4500, "white", "EventDealer", "");
					Database.changeUserEventPoints(p.Name, 1500, true);
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Event Punkte", 4500, "red", "EventDealer", "");
				}

			} else if(selection == "redpresent")
			{
				if (Database.getEventPoints(p.Name) >= 2000)
				{
					Notification.SendPlayerNotifcation(p, "Du hast dir ein Gelbes Geschenk gekauft", 4500, "white", "EventDealer", "");
					Database.changeUserEventPoints(p.Name, 2000, true);

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Event Punkte", 4500, "red", "EventDealer", "");
				}
			}
		}
	}
}
