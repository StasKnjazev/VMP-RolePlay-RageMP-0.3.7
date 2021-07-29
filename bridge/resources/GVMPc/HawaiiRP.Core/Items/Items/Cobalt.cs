using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Cobalt : Item
    {

        public Cobalt()
        {
            Id = 97;
            Name = "Cobalt";
            ImagePath = "Cobalt.png";
            WeightInG = 250;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}