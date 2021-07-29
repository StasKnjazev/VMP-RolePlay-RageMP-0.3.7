using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Geldkoffer : Item
    {

        public Geldkoffer()
        {
            Id = 131;
            Name = "Geldkoffer";
            ImagePath = "Geldkoffer.png";
            WeightInG = 1600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}