using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
    class Machinepistol : Item
    {
        public Machinepistol()
        {
            Id = 7;
            Name = "Machinepistol";
            ImagePath = "Machinepistol.png";
            WeightInG = 0;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }

    }
}
