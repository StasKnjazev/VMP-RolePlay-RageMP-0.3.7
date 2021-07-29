using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class apple : Item
    {

        public apple()
        {
            Id = 54;
            Name = "Apple";
            ImagePath = "apple.png";
            WeightInG = 60;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}