using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class cocain : Item
    {

        public cocain()
        {
            Id = 97;
            Name = "Cocain";
            ImagePath = "cocain.png";
            WeightInG = 400;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}