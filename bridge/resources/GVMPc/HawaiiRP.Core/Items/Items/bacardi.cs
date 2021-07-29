using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class bacardi : Item
    {

        public bacardi()
        {
            Id = 57;
            Name = "Bacardi";
            ImagePath = "bacardi.png";
            WeightInG = 350;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}