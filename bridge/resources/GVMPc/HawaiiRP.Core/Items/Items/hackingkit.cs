using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class hackingkit : Item
    {

        public hackingkit()
        {
            Id = 141;
            Name = "Hackingkit";
            ImagePath = "hackingkit.png";
            WeightInG = 1000;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}