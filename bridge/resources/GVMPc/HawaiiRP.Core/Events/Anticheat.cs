using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc
{
    class Anticheat : Script
    {
		public static List<WeaponHash> blacklist = new List<WeaponHash>() { WeaponHash.RPG, WeaponHash.Railgun, WeaponHash.GrenadeLauncher, WeaponHash.HomingLauncher, WeaponHash.Minigun, WeaponHash.Molotov, WeaponHash.StickyBomb };

		[RemoteEvent("server:CheatDetection")]
        public void cheatDetection(Client p, string flag)
        {
            try
            {
				if (Commands.Commands.isPlayerInAduty(p))
					return;


                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (Database.getPlayerRights(target.Name) > 0)
                    {
                        Notification.SendPlayerNotifcation(target, "Der Spieler " + p.Name + " ist verdächtig. Flag: " + flag, 8000, "white", "ANTICHEAT", "white");
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message);  }
        }

        [ServerEvent(Event.PlayerWeaponSwitch)]
        public void playerWeaponChange(Client p, WeaponHash oldweapon, WeaponHash newweapon)
        {
            try {
            p.TriggerEvent("client:weaponSwap");
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

        public static void Wait(Client p)
        {
            try {
                if(p != null && p.Exists)
                    p.TriggerEvent("client:respawning");

				if(blacklist.Contains(p.CurrentWeapon))
				{
					Functions.XCM(p);
				}
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

		[ServerEvent(Event.PlayerWeaponSwitch)]
		public void onAntiCheatDetectBlacklistedWeapon(Client p, WeaponHash oldWeapon, WeaponHash newweapon)
		{
			try
			{
				if(blacklist.Contains(p.CurrentWeapon))
				{
					NAPI.Task.Run(delegate
					{
						Functions.XCM(p);
					}, 3000);
				}

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}
		}

		[ServerEvent(Event.PlayerDamage)]
		public void onAnticheatDetectHealkey(Client p, float healloss, float armorloss)
		{
			if (Commands.Commands.adminDuty.Contains(p))
				return;

			if(healloss == 0 || armorloss == 0 || healloss == 0 && armorloss == 0)
			{
				Functions.XCM(p);
				p.Kick("Healkey|Godmode");
			}
		}
    }
}
