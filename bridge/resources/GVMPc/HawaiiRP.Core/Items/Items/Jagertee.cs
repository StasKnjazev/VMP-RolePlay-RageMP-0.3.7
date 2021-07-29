using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Jagertee : Item
    {

        public Jagertee()
        {
            Id = 156;
            Name = "Jagertee";
            ImagePath = "Jagertee.png";
            WeightInG = 187;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}