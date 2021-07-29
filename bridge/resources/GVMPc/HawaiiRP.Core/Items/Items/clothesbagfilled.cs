using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class clothesbagfilled : Item
    {

        public clothesbagfilled()
        {
            Id = 94;
            Name = "Volle Kleidungs Tüte";
            ImagePath = "clothesbagfilled.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}