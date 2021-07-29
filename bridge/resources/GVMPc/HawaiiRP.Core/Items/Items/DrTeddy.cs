using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class DrTeddy : Item
    {

        public DrTeddy()
        {
            Id = 115;
            Name = "DrTeddy";
            ImagePath = "DrTeddy.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}