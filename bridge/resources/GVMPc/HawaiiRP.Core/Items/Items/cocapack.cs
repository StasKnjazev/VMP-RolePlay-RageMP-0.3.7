using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class cocapack : Item
    {

        public cocapack()
        {
            Id = 100;
            Name = "Cocaine Pack";
            ImagePath = "cocapack.png";
            WeightInG = 350;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}