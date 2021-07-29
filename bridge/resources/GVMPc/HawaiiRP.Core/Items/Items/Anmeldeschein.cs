using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Anmeldeschein : Item
    {

        public Anmeldeschein()
        {
            Id = 53;
            Name = "Anmeldeschein";
            ImagePath = "Anmeldeschein.png";
            WeightInG = 130;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}