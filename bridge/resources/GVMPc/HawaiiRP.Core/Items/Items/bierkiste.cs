using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class bierkiste : Item
    {

        public bierkiste()
        {
            Id = 70;
            Name = "Bier Kisste";
            ImagePath = "bierkiste.png";
            WeightInG = 1300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}