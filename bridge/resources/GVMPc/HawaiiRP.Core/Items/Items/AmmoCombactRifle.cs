using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoCombactRifle : Item
    {

        public AmmoCombactRifle()
        {
            Id = 37;
            Name = "Combactrifle Ammo";
            ImagePath = "AmmoCombactRifle.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}