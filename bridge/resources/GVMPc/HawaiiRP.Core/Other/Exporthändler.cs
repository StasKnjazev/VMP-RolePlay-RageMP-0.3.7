/*using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Other
{
    class Exporthändler : Script
    {
        public Vector3 ExportPos = new Vector3(-1032.595, -421.6017, 38.51566);
        public int Preis = new Random().Next(8000, 14000);

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            ColShape val = NAPI.ColShape.CreateCylinderColShape(ExportPos, 2.0f, 2.0f, 0);
            val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openExport"));
            val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um den Exporthändler zu öffnen", "DEALER", "blue", 5000));

            NAPI.Blip.CreateBlip(458, ExportPos, 1.0f, 0, "Exporthändler", 0, 255, true, 0, 0);

            Log.Write("Exporthändler geladen.");
        }

        [RemoteEvent("openExport")]
        public void OpenDealer(Client p)
        {
            NativeMenu nativeMenu = new NativeMenu("Exporthändler", "Dealer", new List<NativeItem>()
            {
                new NativeItem("Verkaufe 1 Wein -" + Preis + "$", "1")
            });
            nativeMenu.showNativeMenu(p);
        }

        [RemoteEvent("SellExport")]
        public void SellExport(Client p, string selection, string countstring)
        {
            if (selection == null)
                return;

            try
            {
                int count = int.Parse(countstring);

                string[] strArray = selection.Split("-");

                string item = strArray[0];

                if (Database.getItemCount(p.Name, item) >= count)
                {
                    Database.changeInventoryItem(p.Name, item, -count, true);
                    Database.changeMoney(p.Name, count * Preis, false);
                    Notification.SendPlayerNotifcation(p, "-" + count + item, 5000, "blue", "DEALER", "blue");
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast zu wenig " + item + " dabei.", 5000, "blue", "DEDALER", "blue");
                }
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }
    }
} */
