using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Alteisen : Item
    {

        public Alteisen()
        {
            Id = 23;
            Name = "Alteisen";
            ImagePath = "Alteisen.png";
            WeightInG = 300;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}