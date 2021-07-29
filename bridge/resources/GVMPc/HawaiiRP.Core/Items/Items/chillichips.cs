using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class chillichips : Item
    {

        public chillichips()
        {
            Id = 91;
            Name = "Chilli Chips";
            ImagePath = "chillichips.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}