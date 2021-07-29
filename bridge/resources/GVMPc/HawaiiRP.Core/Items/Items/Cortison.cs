using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Cortison : Item
    {

        public Cortison()
        {
            Id = 106;
            Name = "Cortison Spritze";
            ImagePath = "Cortison.png";
            WeightInG = 600;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}