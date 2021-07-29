using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class bacardikiste : Item
    {

        public bacardikiste()
        {
            Id = 58;
            Name = "Bacardi Kiste";
            ImagePath = "bacardikiste.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}