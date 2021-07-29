using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoCarbineRifle : Item
    {

        public AmmoCarbineRifle()
        {
            Id = 33;
            Name = "Carbinerifle Ammo";
            ImagePath = "AmmoCarbineRifle.png";
            WeightInG = 1000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}