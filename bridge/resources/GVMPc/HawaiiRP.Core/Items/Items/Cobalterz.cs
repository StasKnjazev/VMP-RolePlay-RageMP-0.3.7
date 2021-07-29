using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Cobalterz : Item
    {

        public Cobalterz()
        {
            Id = 98;
            Name = "Cobalterz";
            ImagePath = "Cobalterz.png";
            WeightInG = 600;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}