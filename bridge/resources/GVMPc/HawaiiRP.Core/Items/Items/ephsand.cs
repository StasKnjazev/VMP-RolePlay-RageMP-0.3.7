using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class ephsand : Item
    {

        public ephsand()
        {
            Id = 74;
            Name = "Ephsand";
            ImagePath = "ephsand.png";
            WeightInG = 50;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}