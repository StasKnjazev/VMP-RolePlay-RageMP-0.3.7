using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Ephidrin : Item
    {

        public Ephidrin()
        {
            Id = 10;
            Name = "Ephidrin";
            ImagePath = "Ephedrin.png";
            WeightInG = 130;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
