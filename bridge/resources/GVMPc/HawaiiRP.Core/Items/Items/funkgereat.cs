using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class funkgereat : Item
    {

        public funkgereat()
        {
            Id = 79;
            Name = "Funkgerät";
            ImagePath = "funkgereat.png";
            WeightInG = 500;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}