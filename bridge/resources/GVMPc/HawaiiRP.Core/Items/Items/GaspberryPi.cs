using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class GaspberryPi : Item
    {

        public GaspberryPi()
        {
            Id = 127;
            Name = "Gaspberry Pi";
            ImagePath = "GaspberryPi.png";
            WeightInG = 800;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}