using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Cocktail : Item
    {

        public Cocktail()
        {
            Id = 102;
            Name = "Cocktail Rot";
            ImagePath = "Cocktail.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}