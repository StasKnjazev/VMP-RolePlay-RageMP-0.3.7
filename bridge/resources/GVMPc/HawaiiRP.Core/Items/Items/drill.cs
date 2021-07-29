using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class drill : Item
    {

        public drill()
        {
            Id = 114;
            Name = "Bohrer";
            ImagePath = "drill.png";
            WeightInG = 2000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}