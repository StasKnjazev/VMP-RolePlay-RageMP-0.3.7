using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Cocktail2 : Item
    {

        public Cocktail2()
        {
            Id = 103;
            Name = "Blauer Cocktail";
            ImagePath = "Cocktail2.png";
            WeightInG = 700;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}