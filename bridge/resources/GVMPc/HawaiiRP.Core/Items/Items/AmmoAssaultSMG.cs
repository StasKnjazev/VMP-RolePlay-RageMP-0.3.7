using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoAssaultSMG : Item
    {

        public AmmoAssaultSMG()
        {
            Id = 30;
            Name = "AssaultSMG Ammo";
            ImagePath = "AmmoAssaultSMG.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}