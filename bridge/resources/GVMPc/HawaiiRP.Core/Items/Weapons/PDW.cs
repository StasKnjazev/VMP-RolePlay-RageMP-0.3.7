using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
	class PDW : Item
	{
		public PDW()
		{
			Id = 200;
			Name = "PDW";
			ImagePath = "PDW.png";
			WeightInG = 0;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			try
			{
				WeaponHash hash = WeaponHash.CombatPDW;

				Database.changeInventoryItem(p.Name, "PDW", 1, true);
				p.GiveWeapon(hash, 0);
				Database.addLoadout(p.Name, hash);

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}

			return true;
		}

	}
}
