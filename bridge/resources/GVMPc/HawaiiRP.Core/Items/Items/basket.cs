using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class basket : Item
    {

        public basket()
        {
            Id = 61;
            Name = "Korb";
            ImagePath = "basket.png";
            WeightInG = 180;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}