using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class angel : Item
    {

        public angel()
        {
            Id = 52;
            Name = "Angel";
            ImagePath = "angel.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}