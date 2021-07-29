using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Golddice : Item
    {

        public Golddice()
        {
            Id = 135;
            Name = "Golddice";
            ImagePath = "Golddice.png";
            WeightInG = 999;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}