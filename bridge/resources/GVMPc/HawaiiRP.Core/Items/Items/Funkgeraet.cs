using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Funkgeraet : Item
    {

        public Funkgeraet()
        {
            Id = 126;
            Name = "Funkgerät";
            ImagePath = "Funkgeraet.png";
            WeightInG = 300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}