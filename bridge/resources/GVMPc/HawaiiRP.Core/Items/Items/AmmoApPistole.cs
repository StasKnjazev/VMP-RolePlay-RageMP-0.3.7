using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoApPistole : Item
    {

        public AmmoApPistole()
        {
            Id = 27;
            Name = "Appistole Ammo";
            ImagePath = "AmmoApPistole.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}