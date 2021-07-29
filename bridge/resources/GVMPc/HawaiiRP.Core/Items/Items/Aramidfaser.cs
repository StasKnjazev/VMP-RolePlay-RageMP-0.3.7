using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Aramidfaser : Item
    {

        public Aramidfaser()
        {
            Id = 55;
            Name = "Aramidfaser";
            ImagePath = "Aramidfaser.png";
            WeightInG = 25;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
