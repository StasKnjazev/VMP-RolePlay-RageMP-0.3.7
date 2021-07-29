using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class hacker : Item
    {

        public hacker()
        {
            Id = 140;
            Name = "Hacker Zeug";
            ImagePath = "hacker.png";
            WeightInG = 1000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}