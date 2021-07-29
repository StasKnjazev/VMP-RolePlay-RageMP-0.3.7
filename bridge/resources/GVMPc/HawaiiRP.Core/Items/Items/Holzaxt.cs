using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Holzaxt : Item
    {

        public Holzaxt()
        {
            Id = 149;
            Name = "Holzaxt";
            ImagePath = "Holzaxt.png";
            WeightInG = 1600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}