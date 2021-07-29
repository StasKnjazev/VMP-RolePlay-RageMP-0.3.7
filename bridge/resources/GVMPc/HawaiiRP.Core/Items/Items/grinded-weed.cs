using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class grindedweed : Item
    {

        public grindedweed()
        {
            Id = 139;
            Name = "grinded Weed";
            ImagePath = "grindedweed.png";
            WeightInG = 3;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}