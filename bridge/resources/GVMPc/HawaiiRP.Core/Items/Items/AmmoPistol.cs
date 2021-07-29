using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class AmmoPistol : Item
    {

        public AmmoPistol()
        {
            Id = 46;
            Name = "AmmoPistol";
            ImagePath = "AmmoPistol.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

		public override bool getItemFunction(Client p)
		{
			if (NAPI.Player.GetPlayerCurrentWeapon(p) == WeaponHash.Pistol)
			{

				WeaponHash weapon = p.CurrentWeapon;
				p.TriggerEvent("sendProgressbar", new object[1]
				{
					5000
				});
				p.TriggerEvent("disableAllPlayerActions", new object[1]
				{
					true
				});
				//NAPI.Player.PlayPlayerAnimation(p, 33, "ac_ig_3_p3_b-0", "w_ar_assaultrifle_mag1-0", 8);
				NAPI.Player.PlayPlayerAnimation(p, 33, "weapons@submg@micro_smg_str", "reload_aim", 8);
				NAPI.Task.Run(delegate
				{
					p.GiveWeapon(weapon, 222);
					Database.changeInventoryItem(p.Name, "AmmoPistol", 1, true);
					Notification.SendPlayerNotifcation(p, "Du hast 222 Kugeln in deine Pistol gefüllt", 4500, "green", "WAFFE", "");
					p.TriggerEvent("disableAllPlayerActions", new object[1]
					{
							false
					});
					NAPI.Player.StopPlayerAnimation(p);
					p.ResetData("IS_FARMING");

				}, 5000);
			}
			else
			{
				Notification.SendPlayerNotifcation(p, "Diese Munition ist nicht für deine Waffe geeignet", 4500, "red", "WAFFE", "");
				Database.changeInventoryItem(p.Name, "AmmoPistol", 1, false);
			}
			return true;
		}
	}
}
