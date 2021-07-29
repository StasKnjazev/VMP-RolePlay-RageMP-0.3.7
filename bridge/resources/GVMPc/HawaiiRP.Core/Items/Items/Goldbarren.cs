using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Goldbarren : Item
    {

        public Goldbarren()
        {
            Id = 80;
            Name = "Goldbarren";
            ImagePath = "goldbarren.png";
            WeightInG = 999;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
