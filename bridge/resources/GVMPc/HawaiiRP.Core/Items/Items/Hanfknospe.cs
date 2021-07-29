using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Hanfknospe : Item
    {

        public Hanfknospe()
        {
            Id = 145;
            Name = "Hanfknospe";
            ImagePath = "Hanfknospe.png";
            WeightInG = 50;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}