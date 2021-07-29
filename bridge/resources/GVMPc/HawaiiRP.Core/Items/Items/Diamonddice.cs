using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Diamonddice : Item
    {

        public Diamonddice()
        {
            Id = 110;
            Name = "Diamond Ice";
            ImagePath = "Diamonddice.png";
            WeightInG = 6000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}