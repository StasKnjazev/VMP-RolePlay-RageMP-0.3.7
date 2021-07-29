using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Benzinfaesser: Item
    {

        public Benzinfaesser()
        {
            Id = 68;
            Name = "Benzin";
            ImagePath = "rohoel.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
