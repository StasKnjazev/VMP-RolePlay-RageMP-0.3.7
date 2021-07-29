using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Geldkarte : Item
    {

        public Geldkarte()
        {
            Id = 130;
            Name = "Bankkarte";
            ImagePath = "Geldkarte.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}