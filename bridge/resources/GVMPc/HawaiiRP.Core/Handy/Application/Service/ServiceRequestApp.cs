using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Handy
{
	class ServiceRequestApp : Script
	{
		public static bool abgrebrochen = false;

		[RemoteEvent("addServiceRequest")]
		public void addServiceRequest(Client p, string category, string content, bool phonenumber)
		{
			try
			{
				if(category == "police")
				{

					if (phonenumber == true)
					{
						Ipad.ServiceOverviewApp.addService(p, content, 21211);
						Notification.SendPlayerNotifcation(p, "Anfrage erfolgreich verschickt", 5000, "yellow", "SERVICE", "");
						foreach(Client c in NAPI.Pools.GetAllPlayers())
						{
							if(c.GetSharedData("FRAKTION") == "Los Santos Police Department")
							{
								Notification.SendPlayerNotifcation(c, "Ein neuer Notruf ist eingekommen", 5000, "yellow", "SERVICE", "");
							}
						}

					} else
					{
						Ipad.ServiceOverviewApp.addService(p, content, 0);
						Notification.SendPlayerNotifcation(p, "Anfrage erfolgreich verschickt", 5000, "yellow", "SERVICE", "");
						foreach (Client c in NAPI.Pools.GetAllPlayers())
						{
							if (c.GetSharedData("FRAKTION") == "Los Santos Police Department")
							{
								Notification.SendPlayerNotifcation(c, "Ein neuer Notruf ist eingekommen", 5000, "yellow", "SERVICE", "");
							}
						}
					}

				} else if(category == "medic")
				{
					if (phonenumber == true)
					{
						Ipad.ServiceOverviewApp.addService(p, content, 0);
						Notification.SendPlayerNotifcation(p, "Anfrage erfolgreich verschickt", 5000, "yellow", "SERVICE", "");
						foreach (Client c in NAPI.Pools.GetAllPlayers())
						{
							if (c.GetSharedData("FRAKTION") == "Los Santos Medical Department")
							{
								Notification.SendPlayerNotifcation(c, "Ein neuer Notruf ist eingekommen", 5000, "yellow", "SERVICE", "");
							}
						}
					} else
					{
						Ipad.ServiceOverviewApp.addService(p, content, 0);
						Notification.SendPlayerNotifcation(p, "Anfrage erfolgreich verschickt", 5000, "yellow", "SERVICE", "");
						foreach (Client c in NAPI.Pools.GetAllPlayers())
						{
							if (c.GetSharedData("FRAKTION") == "Los Santos Medical Department")
							{
								Notification.SendPlayerNotifcation(c, "Ein neuer Notruf ist eingekommen", 5000, "yellow", "SERVICE", "");
							}
						}
					}
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[RemoteEvent("cancelServiceRequest")]
		public void cancelRequest(Client p)
		{
			try
			{
				if(p.HasData("IS_REQUESTING_SERVICE") == false)
				{
					Notification.SendPlayerNotifcation(p, "Du hast keine Anfrage verschickt", 4500, "red", "", "");

				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast deine Anfrage abgebrochen", 4500, "red", "", "");
					abgrebrochen = true;
					p.SetData("IS_REQUESTING_SERVICE", false);
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}
	}
}
