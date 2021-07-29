using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class cocapaste : Item
    {

        public cocapaste()
        {
            Id = 101;
            Name = "Cocaine Paste";
            ImagePath = "cocapaste.png";
            WeightInG = 400;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}