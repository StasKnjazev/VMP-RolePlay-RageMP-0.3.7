using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class iVZugang : Item
    {

        public iVZugang()
        {
            Id = 154;
            Name = "iVZugang";
            ImagePath = "iVZugang.png";
            WeightInG = 1300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}