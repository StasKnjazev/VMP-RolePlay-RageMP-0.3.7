using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class beer : Item
    {

        public beer()
        {
            Id = 65;
            Name = "Bier";
            ImagePath = "beer.png";
            WeightInG = 450;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}