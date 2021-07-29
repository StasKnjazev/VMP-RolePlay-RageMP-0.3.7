using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class BenutzKennzeichen : Item
    {

        public BenutzKennzeichen()
        {
            Id = 66;
            Name = "Benustes Kenzeichen";
            ImagePath = "BenutzKennzeichen.png";
            WeightInG = 300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}