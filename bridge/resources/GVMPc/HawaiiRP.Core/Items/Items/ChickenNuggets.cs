using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class ChickenNuggets : Item
    {

        public ChickenNuggets()
        {
            Id = 96;
            Name = "Chicken Nuggets";
            ImagePath = "ChickenNuggets.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}