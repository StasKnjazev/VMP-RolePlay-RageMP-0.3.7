using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Ipad
{
	class Ipad : Script
	{
		[RemoteEvent("computerCheck")]
		public void OpenComputer(Client p)
		{
			try
			{
				if(Database.getItemCount(p.Name, "Laptop") > 0 ) {

					if (!Database.isPlayerDeath(p.Name))
					{
						if (!Functions.handcuffed.Contains(p.Name))
						{

							string PoliceAktenSearchApp = "{\"id\":\"PoliceAktenSearchApp\",\"appName\":\"PoliceAktenSearchApp\",\"name\":\"Akten\",\"icon\": \"PoliceAktenSearchApp.svg\"},";
							string FraktionApp = "{\"id\":\"FraktionListApp\",\"appName\":\"FraktionListApp\",\"name\":\"Fraktion\",\"icon\": \"TeamApp.svg\"},";
							if (!Database.isPlayerInFrak(p, "Los Santos Police Department"))
							{
								PoliceAktenSearchApp = "";
							}
							if(Database.isPlayerInFrak(p, "Zivilist"))
							{
								FraktionApp = "";
							}
							p.TriggerEvent("openComputer");
							p.TriggerEvent("componentServerEvent", "DesktopApp", "responseComputerApps", "[" + PoliceAktenSearchApp + FraktionApp +"{\"id\":\"VehicleTaxApp\",\"appName\":\"VehicleTaxApp\",\"name\":\"Steuern\",\"icon\": \"234788.svg\"},  {\"id\":\"FahrzeugUebersichtApp\",\"appName\":\"FahrzeugUebersichtApp\",\"name\":\"Fahrzeugübersicht\",\"icon\": \"189088.svg\"}, {\"id\":\"MarketplaceApp\",\"appName\":\"MarketplaceApp\",\"name\":\"Gebay\",\"icon\": \"gebay.png\"}, {\"id\":\"KFZRentApp\",\"appName\":\"KFZRentApp\",\"name\":\"Autovermietung\",\"icon\":\"858320.svg\"}]");
						}
					} else
					{
						Notification.SendPlayerNotifcation(p, "Du bist Verstorben", 5000, "red", "", "");
					}
				} else
				{
					Notification.SendPlayerNotifcation(p, "Du hast keinen Laptop dabei", 5000, "red", "", "");
				}
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}

		[RemoteEvent("closeComputer")]
		public void CloseComputer(Client p)
		{
			try
			{
				p.TriggerEvent("closeComputer");
			}
			catch (Exception ex) { Log.Write(ex.Message); }
		}
	}
} 
