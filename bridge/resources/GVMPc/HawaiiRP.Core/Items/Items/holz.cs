using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class holz : Item
    {

        public holz()
        {
            Id = 148;
            Name = "Holz";
            ImagePath = "holz.png";
            WeightInG = 200;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}