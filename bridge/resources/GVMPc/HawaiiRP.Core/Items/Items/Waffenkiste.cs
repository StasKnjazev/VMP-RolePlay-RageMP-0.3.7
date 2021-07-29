using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Waffenkiste : Item
    {

        public Waffenkiste()
        {
            Id = 3;
            Name = "Waffenkiste";
            ImagePath = "KisteAdvancedRifle.png";
            WeightInG = 1000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            int random = new Random().Next(5);

            switch (random)
            {
                case 0:
                    Database.changeInventoryItem(p.Name, "Assaultrifle", 1, false);
                    Notification.SendPlayerNotifcation(p, "+1 Assaultrifle", 5000, "white", "INFORMATION", "white");
                    break;

                case 1:
                    Database.changeInventoryItem(p.Name, "Compactrifle", 1, false);
                    Notification.SendPlayerNotifcation(p, "+1 Compactrifle", 5000, "white", "INFORMATION", "white");
                    break;

                case 2:
                    Database.changeInventoryItem(p.Name, "Heavypistol", 1, false);
                    Notification.SendPlayerNotifcation(p, "+1 Heavypistol", 5000, "white", "INFORMATION", "white");
                    break;

                case 3:
                    Database.changeInventoryItem(p.Name, "Advancedrifle", 1, false);
                    Notification.SendPlayerNotifcation(p, "+1 Advancedrifle", 5000, "white", "INFORMATION", "white");
                    break;

                case 4:
                    Database.changeInventoryItem(p.Name, "Assaultrifle", 1, false);
                    Notification.SendPlayerNotifcation(p, "+1 Assaultrifle", 5000, "white", "INFORMATION", "white");
                    break;

                case 5:
                    Database.changeInventoryItem(p.Name, "Pistol", 1, false);
                    Notification.SendPlayerNotifcation(p, "+1 Pistol", 5000, "white", "INFORMATION", "white");
                    break;
            }
            return true;
        }
    }
}
