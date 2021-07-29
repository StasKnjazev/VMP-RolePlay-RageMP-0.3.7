using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class DiamondWater : Item
    {

        public DiamondWater()
        {
            Id = 112;
            Name = "DiamondWater";
            ImagePath = "DiamondWater.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}