using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Glitzerlolly : Item
    {

        public Glitzerlolly()
        {
            Id = 134;
            Name = "Glitzerlolly";
            ImagePath = "Glitzerlolly.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}