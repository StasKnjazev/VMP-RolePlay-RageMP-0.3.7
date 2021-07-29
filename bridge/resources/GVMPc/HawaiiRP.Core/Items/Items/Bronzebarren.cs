using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Bronzebarren : Item
    {

        public Bronzebarren()
        {
            Id = 84;
            Name = "Bronzebarren";
            ImagePath = "Bronzebarren.png";
            WeightInG = 999;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}