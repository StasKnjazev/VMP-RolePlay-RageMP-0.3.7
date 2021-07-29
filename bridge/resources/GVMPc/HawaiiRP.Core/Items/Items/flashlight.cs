using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class flashlight : Item
    {

        public flashlight()
        {
            Id = 125;
            Name = "Taschenlampe";
            ImagePath = "flashlight.png";
            WeightInG = 300;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}