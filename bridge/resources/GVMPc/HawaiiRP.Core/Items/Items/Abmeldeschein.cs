using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Abmeldeschein : Item
    {

        public Abmeldeschein()
        {
            Id = 14;
            Name = "Abmeldeschein";
            ImagePath = "Abmeldeschein.png";
            WeightInG = 500;
            MaxStackSize = 10;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}