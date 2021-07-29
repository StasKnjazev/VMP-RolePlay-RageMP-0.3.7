using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace GVMPc.Ipad
{
	public class KFZRentApp : Script
	{
		public static List<RentModel> renters = new List<RentModel>();

		[ServerEvent(Event.ResourceStart)]
		public void Start()
		{
			renters.Add(new RentModel("Jack_Hawaii", 742122, "BMW M8", "Gelber BMW M8"));
		}

		[RemoteEvent("requestkfzrent")]
		public void requestkfzrent(Client p)
		{
			try
			{
				p.TriggerEvent("componentServerEvent", new object[3]
				{
				"KFZRentApp",
				"responsekfzrent",
				JsonConvert.SerializeObject(renters)
				});
				Log.Write(JsonConvert.SerializeObject(renters));
			}
			catch (Exception e)
			{
				Log.Write(e.ToString());
			}
		}

		[RemoteEvent("cancelkfzrent")]
		public void cancelkfzrent(Client p, int vehId)
		{
			Notification.SendPlayerNotifcation(p, "DU hast den Vertrag beendet", 5000, "grey", "KFZRent", "");
			requestkfzrent(p);
			renters.Remove(renters.Find((RentModel renter) => renter.name == p.Name));
		}
	}
}
