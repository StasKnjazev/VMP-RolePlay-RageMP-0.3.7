using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class hanfpulver : Item
    {

        public hanfpulver()
        {
            Id = 147;
            Name = "Hanfpulver";
            ImagePath = "hanfpulver.png";
            WeightInG = 220;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}