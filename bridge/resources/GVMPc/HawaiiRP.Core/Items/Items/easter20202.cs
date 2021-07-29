using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class easter20202 : Item
    {

        public easter20202()
        {
            Id = 119;
            Name = "Buntes Ei";
            ImagePath = "easter20202.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}