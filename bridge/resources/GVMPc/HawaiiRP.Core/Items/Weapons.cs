using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Weapons : Script
    {
        public static Dictionary<WeaponHash, string> weapons = new Dictionary<WeaponHash, string>();

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            if (weapons.Count < 1)
            {
                AddWeaponToList("WEAPON_ASSAULTRIFLE");
                AddWeaponToList("WEAPON_ADVANCEDRIFLE");
                AddWeaponToList("WEAPON_COMBATPDW");
                AddWeaponToList("WEAPON_PISTOL");
                AddWeaponToList("WEAPON_MICROSMG");
                AddWeaponToList("WEAPON_GUSENBERG");
                AddWeaponToList("WEAPON_HEAVYPISTOL");
                AddWeaponToList("WEAPON_COMPACTRIFLE");
                AddWeaponToList("WEAPON_MACHINEPISTOL");
                AddWeaponToList("WEAPON_BULLPUPRIFLE");
				AddWeaponToList("WEAPON_STUNGUN");
            }
        }

        public static void WeaponSync(Client p)
        {
            try
            {
                if (Other.Paintball.würfelparkPlayers.Contains(p) || Gangwar.GebietRegister.InGangwarMarker.Contains(p))
                    return;


                if (Start.loggedInPlayers.ContainsKey(p))
                {
                    List<WeaponHash> loadout = Database.getLoadout(p.Name);

                    p.RemoveAllWeapons();

                    foreach (WeaponHash weapon in loadout)
                    {
                        if (weapons.ContainsKey(weapon))
                        {
                            p.GiveWeapon(weapon, 222);
                        }

                    }

                    NAPI.Player.SetPlayerCurrentWeapon(p, WeaponHash.Unarmed);

                }

            } catch (Exception ex) {}
            
        }

        [RemoteEvent("checkWeapon")]
        public void checkWeapon(Client p, int weaponHash2)
        {
            if (weaponHash2 == 0)
                return;

            WeaponHash weaponHash = (WeaponHash)weaponHash2;

            if (weaponHash != WeaponHash.Unarmed && !weapons.ContainsKey(weaponHash) && weaponHash != (WeaponHash)0 && weaponHash == WeaponHash.Railgun && weaponHash == WeaponHash.StickyBomb && weaponHash == WeaponHash.Minigun && weaponHash == WeaponHash.Molotov)
            {
                Functions.XCM(p);
            }

            if (Start.loggedInPlayers.ContainsKey(p) && !Gangwar.GebietRegister.InGangwarMarker.Contains(p) && !Other.Paintball.würfelparkPlayers.Contains(p))
            {
                if (weapons.ContainsKey(weaponHash))
                {
                    if (!Database.getLoadout(p.Name).Contains(weaponHash))
                    {
                        p.RemoveWeapon(weaponHash);
                    }
                }
            }
        }

        public static void AddWeaponToList(string weapon)
        {
            try
            {
                weapons.Add((WeaponHash)NAPI.Util.GetHashKey(weapon), weapon.ToUpper().Replace("WEAPON_", "")[0].ToString().ToUpper() + weapon.ToUpper().Replace("WEAPON_", "").Substring(1).ToLower());
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
