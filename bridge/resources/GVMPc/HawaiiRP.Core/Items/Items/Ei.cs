using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Ei : Item
    {

        public Ei()
        {
            Id = 120;
            Name = "Ei";
            ImagePath = "Ei.png";
            WeightInG = 250;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}