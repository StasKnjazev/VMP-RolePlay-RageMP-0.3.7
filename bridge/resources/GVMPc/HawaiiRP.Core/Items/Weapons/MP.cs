using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
	class MP : Item
	{
		public MP()
		{
			Id = 170;
			Name = "MP";
			ImagePath = "MicroSMG.png";
			WeightInG = 0;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			try
			{
				WeaponHash hash = WeaponHash.SMG;

				Database.changeInventoryItem(p.Name, "MP", 1, true);
				p.GiveWeapon(hash, 0);
				Database.addLoadout(p.Name, hash);

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message);
			}

			return true;
		}

	}
}
