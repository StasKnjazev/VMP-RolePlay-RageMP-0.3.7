using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Brooksten_Light : Item
    {

        public Brooksten_Light()
        {
            Id = 86;
            Name = "Malboro Blau";
            ImagePath = "Brooksten_Light.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}