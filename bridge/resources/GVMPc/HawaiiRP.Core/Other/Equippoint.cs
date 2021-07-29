/*using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
    class Equippoint : Script
    {
        public static List<string> alreadyEquipped = new List<string>();
        public static Dictionary<string, Vector3> equippoints = new Dictionary<string, Vector3>();

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            if (equippoints.Count < 1)
            {
                equippoints.Add("Equippunkt", new Vector3(186.21584, -914.0723, 23.040178));

                foreach (KeyValuePair<string, Vector3> equippoint in equippoints)
                {
                    ColShape val = NAPI.ColShape.CreateCylinderColShape(equippoint.Value, 7.0f, 1.4f, 0);
                    val.SetData("COLSHAPE_FUNCTION", new FunctionModel("useEquippoint"));
                    val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um dein Equip abzuholen.", "EQUIPPOINT", "white", 5000));

                    NAPI.Blip.CreateBlip(313, equippoint.Value, 1.0f, 0, "Equippoint", 255, 0, true, 0, 0);
                    NAPI.Marker.CreateMarker(1, equippoint.Value, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 7.0f, new Color(255, 140, 0), false, 0);
                }
            }
            Log.Write("Equippoints geladen.");
        }


        [RemoteEvent("useEquippoint")]
        public static void equipPlayer(Client p)
        {
            try
            {
                if (alreadyEquipped.Contains(p.Name))
                {
                    Notification.SendPlayerNotifcation(p, "Du hast dein Equip für die jetzige Wende bereits abgeholt.", 5000, "white", "Equippoint", "white");
                    return;
                }

                alreadyEquipped.Add(p.Name);
                p.TriggerEvent("sendProgressbar", new object[1]
                {
                    5000
                });
                Functions.disableAllPlayerControls(p, true);
                NAPI.Player.PlayPlayerAnimation(p, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
                NAPI.Task.Run((() =>
                {
                    switch (new Random().Next(0, 2))
                    {
                        case 0:
                            Database.changeInventoryItem(p.Name, "Assaultrifle", 1, false);
                            Database.changeInventoryItem(p.Name, "Pistol", 1, false);
                            break;
                        case 1:
                            Database.changeInventoryItem(p.Name, "Compactrifle", 1, false);
                            Database.changeInventoryItem(p.Name, "Heavypistol", 1, false);
                            break;
                        case 2:
                            Database.changeInventoryItem(p.Name, "Heavypistol", 1, false);
                            Database.changeInventoryItem(p.Name, "Pistol", 1, false);
                            break;
                    }
                    p.TriggerEvent("componentServerEvent", new object[2]
                    {
                        "Progressbar",
                        "StopProgressbar"
                    });
                    Notification.SendPlayerNotifcation(p, "Du hast dein Equip für die jetzige Wende abgeholt.", 5000, "white", "Equippoint", "white");
                    p.StopAnimation();
                    Functions.disableAllPlayerControls(p, false);
                }), 5000);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
} */
