using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class backpack : Item
    {

        public backpack()
        {
            Id = 59;
            Name = "Backpack";
            ImagePath = "backpack.png";
            WeightInG = 1000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}