using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Blockade : Item
    {

        public Blockade()
        {
            Id = 72;
            Name = "Blockade";
            ImagePath = "Blockade.png";
            WeightInG = 1600;
            MaxStackSize = 10;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}