using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace GVMPc.Handy
{
	public class TaxiApp : Script
	{
		public static List<TaxiModel> drivers = new List<TaxiModel>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			Log.Write("TaxiApp geladen");
		}

		[RemoteEvent("requestTaxiList")]
		public void requestTaxiList(Client c)
		{
			try
			{
				foreach(Client p in NAPI.Pools.GetAllPlayers())
				{
					if(p.HasData("TAXI_DUTY"))
					{
						drivers.Add(new TaxiModel(p.Name, (int)Database.getUserPhoneNumber(p.Name), 22));
					}
				}

				c.TriggerEvent("componentServerEvent", new object[3]
				{
					"TaxiApp",
					"responseTaxiList",
					JsonConvert.SerializeObject(drivers)
				});

			} catch (Exception e)
			{
				Log.Write(e.ToString());
			}
		}

		[RemoteEvent("requestTaxiDriver")]
		public void requestTaxiDriver(Client p, string name, string message, int price)
		{
			Client target = Database.getPlayerFromName(name);

			Notification.SendPlayerNotifcation(target, message, 5000, "yellow", "Taxi", "");
			target.TriggerEvent("setPlayerGpsMarker", p.Position.X, p.Position.Y);
			Notification.SendPlayerNotifcation(p, "Deine Anfrage wurde erfolgreich abgeschickt", 5000, "yellow", "Taxi", "");
		}
	}
}
