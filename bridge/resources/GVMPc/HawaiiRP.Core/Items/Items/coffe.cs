using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class coffe : Item
    {

        public coffe()
        {
            Id = 104;
            Name = "Coffe";
            ImagePath = "coffe.png";
            WeightInG = 500;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}