using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoMG : Item
    {

        public AmmoMG()
        {
            Id = 43;
            Name = "MG Ammo";
            ImagePath = "AmmoMG.png";
            WeightInG = 1300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}