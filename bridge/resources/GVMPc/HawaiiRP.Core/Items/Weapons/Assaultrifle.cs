using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
    class Assaultrifle : Item
    {
        public Assaultrifle()
        {
            Id = 5;
            Name = "Assaultrifle";
            ImagePath = "Assaultrifle.png";
            WeightInG = 0;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }

    }
}
