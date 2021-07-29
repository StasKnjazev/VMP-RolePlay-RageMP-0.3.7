using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class CT : Item
    {

        public CT()
        {
            Id = 107;
            Name = "CT";
            ImagePath = "CT.png";
            WeightInG = 1800;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}