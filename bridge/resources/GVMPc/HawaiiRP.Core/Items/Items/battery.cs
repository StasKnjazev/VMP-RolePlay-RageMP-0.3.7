using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class battery : Item
    {

        public battery()
        {
            Id = 63;
            Name = "Batterie";
            ImagePath = "battery.png";
            WeightInG = 300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}