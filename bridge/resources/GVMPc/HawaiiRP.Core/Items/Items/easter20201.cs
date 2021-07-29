using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class easter20201 : Item
    {

        public easter20201()
        {
            Id = 118;
            Name = "Jack´s Ei 2021";
            ImagePath = "easter20201.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}