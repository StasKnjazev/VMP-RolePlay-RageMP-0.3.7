using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoMachinePistol : Item
    {

        public AmmoMachinePistol()
        {
            Id = 41;
            Name = "Machinepistol Ammo";
            ImagePath = "AmmoMachinePistol.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}