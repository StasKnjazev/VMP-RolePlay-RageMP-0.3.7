using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoPistol50 : Item
    {

        public AmmoPistol50()
        {
            Id = 47;
            Name = "50Pistol Ammo";
            ImagePath = "AmmoPistol50.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}