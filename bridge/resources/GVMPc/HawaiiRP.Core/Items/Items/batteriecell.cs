using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class batteriecell : Item
    {

        public batteriecell()
        {
            Id = 62;
            Name = "Batterie Stromkreis";
            ImagePath = "batteriecell.png";
            WeightInG = 190;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}