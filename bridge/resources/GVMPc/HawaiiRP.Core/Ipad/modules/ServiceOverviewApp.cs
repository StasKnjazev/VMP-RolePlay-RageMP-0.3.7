using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Ipad;

namespace GVMPc.Ipad
{
	public class ServiceOverviewApp : Script
	{
		public static List<openService> openServices = new List<openService>();

		public static List<acceptedService> acceptedServices = new List<acceptedService>();

		public static List<ownService> ownServices = new List<ownService>();

		[RemoteEvent("requestOpenServices")]
		public void requestOpenServices(Client p)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"ServiceListApp",
				"responseOpenServiceList",
				NAPI.Util.ToJson(openServices)
			});
		}

		public static void addService(Client p, string text, int phonenumber)
		{
			openServices.Add(new openService(p.Name, text, phonenumber, p.Position));
		}

		[RemoteEvent("acceptOpenService")]
		public void acceptOpenService(Client p, string name)
		{
			Client target = Database.getPlayerFromName(name);

			foreach(openService openService in openServices.ToArray())
			{
				if(openService.name == name)
				{
					openServices.Remove(openService);
					ownServices.Add(new ownService(openService.name, openService.message, openService.phonenumber, target.Position));
					Notification.SendPlayerNotifcation(p, "Du hast den Service von " + openService.name + " angenommen", 5000, "yellow", "SERVICE", "");
					Notification.SendPlayerNotifcation(target, "Dein Service wird nun von " + p.Name + " bearbeitet", 5000, "yellow", "SERVICE", "");
				}
			}
		}

		[RemoteEvent("RequestTeamServiceList")]
		public void RequestTeamServiceList(Client p)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"ServiceAcceptedApp",
				"responseTeamServiceList",
				NAPI.Util.ToJson(acceptedServices)
			});
		}

		[RemoteEvent("finishOwnAcceptedService")]
		public void finishOwnAcceptedService(Client p, string name)
		{
			Client target = Database.getPlayerFromName(name);

			foreach (ownService ownService in ownServices.ToArray())
			{
				if (ownService.name == name)
				{
					ownServices.Remove(ownService);
					acceptedServices.Add(new acceptedService(name, ownService.message, ownService.phonenumber, p.Name, target.Position));
					Notification.SendPlayerNotifcation(p, "Du hast den Service von " + ownService.name + " geschlossen", 5000, "yellow", "SERVICE", "");
					Notification.SendPlayerNotifcation(target, "Dein Service wurde nun von " + p.Name + " geschlossen", 5000, "yellow", "SERVICE", "");
				}
			}
		}

		[RemoteEvent("requestOwnServices")]
		public void requestOwnServices(Client p)
		{
			p.TriggerEvent("componentServerEvent", new object[3]
			{
				"ServiceOwnApp",
				"responseOwnServiceList",
				NAPI.Util.ToJson(ownServices)
			});
		}
	}
}
