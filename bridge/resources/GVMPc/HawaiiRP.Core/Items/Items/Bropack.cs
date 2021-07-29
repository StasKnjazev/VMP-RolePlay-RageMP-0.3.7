using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Bropack : Item
    {

        public Bropack()
        {
            Id = 87;
            Name = "Groﬂer Rucksack";
            ImagePath = "Bropack.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}