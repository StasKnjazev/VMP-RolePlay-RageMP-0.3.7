using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class jewel : Item
    {

        public jewel()
        {
            Id = 158;
            Name = "Jewel";
            ImagePath = "jewel.png";
            WeightInG = 120;
            MaxStackSize = 250;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}