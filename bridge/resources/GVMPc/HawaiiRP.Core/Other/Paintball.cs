using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Other
{
    class Paintball : Script
    {
        public static List<Vector3> würfelparkPoints = new List<Vector3> {
            new Vector3(170.8255, -915.5659, 30.69199),
            new Vector3(211.5563, -944.5418, 30.68113),
            new Vector3(241.9889, -886.0068, 30.48896),
            new Vector3(159.5039, -969.2851, 30.09191)
        };

        public static List<Client> würfelparkPlayers = new List<Client>();
        public static Dictionary<string, Vector3> paintballs = new Dictionary<string, Vector3>();

        public static string PAINTBALL_KILLS = "PAINTBALL_KILLS";
        public static string PAINTBALL_DEATHS = "PAINTBALL_DEATHS";

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            if (paintballs.Count < 1)
            {
                paintballs.Add("Würfelpark", new Vector3(570.46, 2796.68, 41.01));

                foreach (KeyValuePair<string, Vector3> paintball in paintballs)
                {
                    NAPI.Blip.CreateBlip(432, paintball.Value, 1.0f, 0, "Paintball", 255, 0, true, 0, 0);
                    NAPI.Marker.CreateMarker(1, paintball.Value, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1.0f, new Color(255, 140, 0), false, 0);

                    ColShape val = NAPI.ColShape.CreateCylinderColShape(paintball.Value, 1.4f, 1.4f, 0);
                    val.SetData("COLSHAPE_FUNCTION", new FunctionModel("enterPaintball", paintball.Key));
                    val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um Paintball zu betreten.", "PAINTBALL", "white", 5000));

                }
            }
        }

		[RemoteEvent("enterPaintball")]
        public static void enterPaintball(Client p, string name)
        {
            if (name == null)
                return;

            try
            {
                NativeMenu nativeMenu = new NativeMenu("Paintball", "Arenen", new List<NativeItem>
            {
                new NativeItem(name + " ( " + Other.Paintball.würfelparkPlayers.Count + " / 1000 )", name)
            });
                nativeMenu.showNativeMenu(p);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        public static Vector3 getRandomSpawnpoint(List<Vector3> list)
        {
            var random = new Random();
            int index = random.Next(list.Count);
            return list[index];
        }

        [RemoteEvent("nM-Paintball")]
        public void joinPaintball(Client p, string value)
        {
            if (value == null)
                return;

            try
            {
                if (value == "Würfelpark")
                {
                    Menus.NativeMenu.closeNativeMenu(p);
                    p.TriggerEvent("initializePaintball");
                    würfelparkPlayers.Add(p);
                    Notification.SendPlayerNotifcation(p, "Du bist Paintball - Würfelpark beigetreten.", 5000, "white", "INFORMATION", "white");
                    Anticheat.Wait(p); p.Position = new Vector3(200.89671, -928.4661, 30.592678);
                    p.Dimension = 3;
                    Anticheat.Wait(p); p.Armor = 100;

                    if (p.HasData(PAINTBALL_DEATHS))
                        p.ResetData(PAINTBALL_DEATHS);

                    if (p.HasData(PAINTBALL_KILLS))
                        p.ResetData(PAINTBALL_KILLS);

                    p.TriggerEvent("updatePaintballScore", 0, 0, 0);

                    p.SetData(PAINTBALL_DEATHS, 0);
                    p.SetData(PAINTBALL_KILLS, 0);
                    p.RemoveAllWeapons();



                    NAPI.Task.Run(() =>
                    {
                        p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_ADVANCEDRIFLE"), 9999);
                        p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_GUSENBERG"), 9999);
                        p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_HEAVYPISTOL"), 9999);
                        p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_ASSAULTRIFLE"), 9999);
                        p.GiveWeapon((WeaponHash)NAPI.Util.GetHashKey("WEAPON_BULLPUPRIFLE"), 9999);
                    }, delayTime: 100);

                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
