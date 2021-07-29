using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class cocaineleafdry : Item
    {

        public cocaineleafdry()
        {
            Id = 99;
            Name = "Trockenes Cocain Blatt";
            ImagePath = "cocaineleafdry.png";
            WeightInG = 250;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}