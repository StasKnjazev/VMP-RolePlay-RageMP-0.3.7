using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
    class CompactRifle : Item
    {
        public CompactRifle()
        {
            Id = 8;
            Name = "Compactrifle";
            ImagePath = "CompactRifle.png";
            WeightInG = 0;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }

    }
}
