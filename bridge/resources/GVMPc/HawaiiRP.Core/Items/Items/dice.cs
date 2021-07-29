using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class dice : Item
    {

        public dice()
        {
            Id = 113;
            Name = "Würfel";
            ImagePath = "dice.png";
            WeightInG = 150;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}