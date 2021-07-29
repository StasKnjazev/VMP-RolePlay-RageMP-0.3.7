using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Dildo : Item
    {

        public Dildo()
        {
            Id = 77;
            Name = "Dildo";
            ImagePath = "Dildo.png";
            WeightInG = 69;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}