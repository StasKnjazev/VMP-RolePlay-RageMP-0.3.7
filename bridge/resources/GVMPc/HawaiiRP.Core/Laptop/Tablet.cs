using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
    class Tablet : Script
    {
		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
		}
        
        [RemoteEvent("Mais")]
        public void OpenTablet(Client p, bool state)
        {
            try
            {
				if (!Database.isPlayerDeath(p.Name))
				{
					if (!Functions.handcuffed.Contains(p.Name))
					{
						if (state)
						{
							p.TriggerEvent("hatMais", true);
							p.TriggerEvent("componentServerEvent", "IpadDesktopApp", "responseIpadApps", "[{\"id\":\"SupportOverviewApp\",\"appName\":\"SupportOverviewApp\",\"name\":\"SupportApp\",\"icon\": \"SupportApp.svg\"}, {\"id\":\"SupportVehicleApp\",\"appName\":\"SupportVehicleApp\",\"name\":\"VehicleSupport\",\"icon\": \"234788.svg\"}]");
						}
						else
						{
							p.TriggerEvent("hatMais", false);
						}
					} else
					{
						return;
					}

				} else
				{
					return;
				}
            } catch(Exception ex) { Log.Write(ex.Message); }
        } 

	}
}
