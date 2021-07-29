using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Kennzeichen : Item
    {

        public Kennzeichen()
        {
            Id = 162;
            Name = "Kennzeichen";
            ImagePath = "Kennzeichen.png";
            WeightInG = 1200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
			try
			{
				Vehicle veh = p.Vehicle;
				if(!(veh == null))
				{
					if(veh.NumberPlate != null && Database.isVehicleOwnedByPlayer(p.Name, veh.NumberPlate))
					{
						p.TriggerEvent("openWindow", "TextInputBox",
							   "{\"textBoxObject\":{\"Title\":\"Kennzeichen Registrierung\",\"Message\":\"Gebe bitte das gewünschte Kennezeichen ein und bestätige dies\",\"Callback\":\"registerPlate\"}}"
						 );
						p.TriggerEvent("componentReady", "TextInputBox");
					}
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
            return true;
        }

		[RemoteEvent("registerPlate")]
		public void registerPlate(Client P, string numberplate)
		{
			try
			{
				Vehicle veh = P.Vehicle;

				Database.changeVehiclePlate(P.Name, numberplate);
				veh.NumberPlate = numberplate;
				Notification.SendPlayerNotifcation(P, "Du hast das Kennzeichen erfolgreich registriert", 4500, "white", "", "");
				

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}
    }

}
