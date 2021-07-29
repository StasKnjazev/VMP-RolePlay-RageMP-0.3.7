using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
    class Pistol : Item
    {
        public Pistol()
        {
            Id = 4;
            Name = "Pistol";
            ImagePath = "Pistol.png";
            WeightInG = 0;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }

    }
}
