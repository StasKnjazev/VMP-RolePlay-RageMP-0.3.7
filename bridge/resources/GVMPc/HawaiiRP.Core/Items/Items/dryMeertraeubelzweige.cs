using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class dryMeertraeubelzweige : Item
    {

        public dryMeertraeubelzweige()
        {
            Id = 116;
            Name = "Trockene Meeres Blätter";
            ImagePath = "dryMeertraeubelzweige.png";
            WeightInG = 400;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}