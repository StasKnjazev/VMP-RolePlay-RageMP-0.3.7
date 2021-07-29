using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
    class Advancedrifle : Item
    {
        public Advancedrifle()
        {
            Id = 9;
            Name = "AdvancedRifle";
            ImagePath = "AdvanvcedRifle.png";
            WeightInG = 0;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }

    }
}
