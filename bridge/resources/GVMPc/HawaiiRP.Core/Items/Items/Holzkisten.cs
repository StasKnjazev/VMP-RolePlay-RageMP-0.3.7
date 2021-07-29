using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Holzkisten : Item
    {

        public Holzkisten()
        {
            Id = 150;
            Name = "Holzkisten";
            ImagePath = "Holzkisten.png";
            WeightInG = 3000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}