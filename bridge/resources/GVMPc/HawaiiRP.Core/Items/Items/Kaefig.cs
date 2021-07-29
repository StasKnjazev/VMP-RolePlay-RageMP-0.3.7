using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Kaefig : Item
    {

        public Kaefig()
        {
            Id = 160;
            Name = "Käfig";
            ImagePath = "Kaefig.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}