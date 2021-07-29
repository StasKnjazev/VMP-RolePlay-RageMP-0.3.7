using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoMiniSMG : Item
    {

        public AmmoMiniSMG()
        {
            Id = 45;
            Name = "MiniSMG Ammo";
            ImagePath = "AmmoMiniSMG.png";
            WeightInG = 350;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}