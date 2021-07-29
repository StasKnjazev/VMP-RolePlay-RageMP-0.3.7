using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoSMG : Item
    {

        public AmmoSMG()
        {
            Id = 49;
            Name = "SMG Ammo";
            ImagePath = "AmmoSMG.png";
            WeightInG = 670;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}