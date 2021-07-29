using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Fangkaefig : Item
    {

        public Fangkaefig()
        {
            Id = 122;
            Name = "Fang Käfig";
            ImagePath = "fangkaefig.png";
            WeightInG = 240;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}