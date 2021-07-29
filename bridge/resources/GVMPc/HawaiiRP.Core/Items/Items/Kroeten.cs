using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Kroeten : Item
    {

        public Kroeten()
        {
            Id = 72;
            Name = "Kröten";
            ImagePath = "Kroeten.png";
            WeightInG = 60;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
