using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class blackrose : Item
    {

        public blackrose()
        {
            Id = 71;
            Name = "Schwarze Rose";
            ImagePath = "blackrose.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}