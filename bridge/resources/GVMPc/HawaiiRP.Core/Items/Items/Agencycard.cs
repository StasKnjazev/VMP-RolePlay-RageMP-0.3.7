using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Agencycard : Item
    {

        public Agencycard()
        {
            Id = 20;
            Name = "Agencycard";
            ImagePath = "Agencycard.png";
            WeightInG = 150;
            MaxStackSize = 1;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}