using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoRevolver : Item
    {

        public AmmoRevolver()
        {
            Id = 48;
            Name = "Revolver Ammo";
            ImagePath = "AmmoRevolver.png";
            WeightInG = 450;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}