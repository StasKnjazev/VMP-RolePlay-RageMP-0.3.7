using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoCombatMG : Item
    {

        public AmmoCombatMG()
        {
            Id = 34;
            Name = "CombatMG Ammo";
            ImagePath = "AmmoCombatMG.png";
            WeightInG = 800;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}