using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class bouquet : Item
    {

        public bouquet()
        {
            Id = 82;
            Name = "Blumen";
            ImagePath = "bouquet.png";
            WeightInG = 100;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}