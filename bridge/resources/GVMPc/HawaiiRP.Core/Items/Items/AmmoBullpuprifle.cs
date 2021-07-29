using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoBullpuprifle : Item
    {

        public AmmoBullpuprifle()
        {
            Id = 32;
            Name = "Bullpuprifle Ammo";
            ImagePath = "AmmoBullpuprifle.png";
            WeightInG = 1000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}