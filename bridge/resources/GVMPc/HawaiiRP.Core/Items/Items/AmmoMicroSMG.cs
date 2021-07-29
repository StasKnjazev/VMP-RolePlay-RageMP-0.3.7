using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoMicroSMG : Item
    {

        public AmmoMicroSMG()
        {
            Id = 44;
            Name = "MicroSMG Ammo";
            ImagePath = "AmmoMicroSMG.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}