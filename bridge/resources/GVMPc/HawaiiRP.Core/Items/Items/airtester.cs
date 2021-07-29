using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class airtester : Item
    {

        public airtester()
        {
            Id = 21;
            Name = "airtester";
            ImagePath = "airtester.png";
            WeightInG = 150;
            MaxStackSize = 3;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}