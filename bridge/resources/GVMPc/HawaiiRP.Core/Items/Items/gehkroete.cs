using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class gehkroete : Item
    {

        public gehkroete()
        {
            Id = 73;
            Name = "Gehäutete Kröten";
            ImagePath = "gehkroete.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}