using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Geschenkrot : Item
    {

        public Geschenkrot()
        {
            Id = 132;
            Name = "Geschenkrot";
            ImagePath = "Geschenkrot.png";
            WeightInG = 500;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}