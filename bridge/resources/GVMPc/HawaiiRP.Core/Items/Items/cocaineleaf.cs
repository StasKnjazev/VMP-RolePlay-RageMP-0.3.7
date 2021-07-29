using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class cocaineleaf : Item
    {

        public cocaineleaf()
        {
            Id = 98;
            Name = "Cocain Blatt";
            ImagePath = "cocaineleaf.png";
            WeightInG = 250;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}