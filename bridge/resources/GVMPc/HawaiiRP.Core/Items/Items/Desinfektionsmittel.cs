using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Desinfektionsmittel : Item
    {

        public Desinfektionsmittel()
        {
            Id = 109;
            Name = "Desinfektionsmittel";
            ImagePath = "Desinfektionsmittel.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}