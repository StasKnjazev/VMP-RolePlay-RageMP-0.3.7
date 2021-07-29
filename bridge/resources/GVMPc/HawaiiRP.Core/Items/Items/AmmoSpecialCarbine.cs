using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoSpecialCarbine : Item
    {

        public AmmoSpecialCarbine()
        {
            Id = 51;
            Name = "SpecialCarbine Ammo";
            ImagePath = "AmmoSpecialCarbine.png";
            WeightInG = 800;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}