using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Hookah : Item
    {

        public Hookah()
        {
            Id = 144;
            Name = "Shisha";
            ImagePath = "Hookah.png";
            WeightInG = 2000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}