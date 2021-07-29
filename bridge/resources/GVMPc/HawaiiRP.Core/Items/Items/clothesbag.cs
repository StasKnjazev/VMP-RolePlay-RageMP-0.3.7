using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class clothesbag : Item
    {

        public clothesbag()
        {
            Id = 93;
            Name = "Kleidungs Tüte";
            ImagePath = "clothesbag.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}