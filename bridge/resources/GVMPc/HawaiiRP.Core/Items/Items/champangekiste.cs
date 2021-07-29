using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class champangekiste : Item
    {

        public champangekiste()
        {
            Id = 90;
            Name = "Champanger Kiste";
            ImagePath = "champangekiste.png";
            WeightInG = 1600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}