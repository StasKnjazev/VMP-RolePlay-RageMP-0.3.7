using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Dietrich : Item
    {

        public Dietrich()
        {
            Id = 76;
            Name = "Dietrich";
            ImagePath = "Dietrich.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}