using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Eisen : Item
    {

        public Eisen()
        {
            Id = 12;
            Name = "Eisen";
            ImagePath = "Eisen.png";
            WeightInG = 300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
