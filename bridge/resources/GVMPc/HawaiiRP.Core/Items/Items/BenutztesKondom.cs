using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class BenutztesKondom : Item
    {

        public BenutztesKondom()
        {
            Id = 67;
            Name = "Benutztes Kondom";
            ImagePath = "BenutztesKondom.png";
            WeightInG = 35;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}