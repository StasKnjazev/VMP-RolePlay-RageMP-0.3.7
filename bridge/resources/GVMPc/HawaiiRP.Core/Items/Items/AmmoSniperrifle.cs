using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoSniperrifle : Item
    {

        public AmmoSniperrifle()
        {
            Id = 50;
            Name = "Sniperrifle Ammo";
            ImagePath = "AmmoSniperrifle.png";
            WeightInG = 1400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}