using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class blueberry : Item
    {

        public blueberry()
        {
            Id = 75;
            Name = "Blaubäre";
            ImagePath = "blueberry.png";
            WeightInG = 120;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}