using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class goldsteintaler : Item
    {

        public goldsteintaler()
        {
            Id = 137;
            Name = "Goldstein Taler";
            ImagePath = "goldsteintaler.png";
            WeightInG = 212;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}