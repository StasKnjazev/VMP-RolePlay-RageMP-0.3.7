using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Duenger : Item
    {

        public Duenger()
        {
            Id = 117;
            Name = "D�nger";
            ImagePath = "Duenger.png";
            WeightInG = 400;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}