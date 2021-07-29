using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Meth : Item
    {

        public Meth()
        {
            Id = 11;
            Name = "Meth";
            ImagePath = "meth.png";
            WeightInG = 3;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
