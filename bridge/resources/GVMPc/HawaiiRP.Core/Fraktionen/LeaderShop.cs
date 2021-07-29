using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Fraktionen
{
    class LeaderShop : Script
    {

        [RemoteEvent("nM-Leadershop")]
        public void buyFraktionsCar(Client p, string value)
        {
            if (value == null)
                return;

            try
            {
                string[] splitted = value.Split("-");
                string name = splitted[0];
                int price = int.Parse(splitted[1]);

                if (Database.getFrakBank(p.GetSharedData("FRAKTION")) >= price)
                {
                    NativeMenu.closeNativeMenu(p);
                    Database.changeFraktionMoney(p.GetSharedData("FRAKTION"), price, true);
                    Database.giveFraktionVehicle(p.GetSharedData("FRAKTION"), name);
                    Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug " + name + " erfolgreich für deine Fraktion gekauft.", 5000, "white", p.GetSharedData("FRAKTION"), "rgb(" + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Red + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Green + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Blue + ")");
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Es ist zu wenig Geld auf der Fraktionsbank.", 5000, "white", p.GetSharedData("FRAKTION"), "rgb(" + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Red + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Green + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Blue + ")");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

    }
}
