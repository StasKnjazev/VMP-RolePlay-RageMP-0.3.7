using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Kaufvertrag : Item
    {

        public Kaufvertrag()
        {
            Id = 161;
            Name = "Kaufvertrag";
            ImagePath = "Kaufvertrag.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}