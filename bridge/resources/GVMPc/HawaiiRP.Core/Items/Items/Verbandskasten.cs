using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Verbandskasten : Item
    {
        public Verbandskasten()
        {
            Id = 1;
            Name = "Verbandskasten";
            ImagePath = "first-aid-kit.png";
            WeightInG = 200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client client)
        {
            if (!client.HasData("PLAYER_ISFARMING"))
            {
                client.TriggerEvent("disableAllPlayerActions", new object[1]
                {
                    true
                });
                client.TriggerEvent("sendProgressbar", new object[1]
                {
                    4000
                });
                client.SetData("PLAYER_ISFARMING", true);
                NAPI.Player.PlayPlayerAnimation(client, 33, "amb@medic@standing@tendtodead@idle_a", "idle_a", 8f);
                NAPI.Task.Run(delegate
                {
                    client.Health = 100;
                    client.TriggerEvent("client:respawning");
                    client.ResetData("PLAYER_ISFARMING");
                    NAPI.Player.StopPlayerAnimation(client);
                    client.TriggerEvent("componentServerEvent", new object[2]
                    {
                        "Progressbar",
                        "StopProgressbar"
                    });
                    client.TriggerEvent("disableAllPlayerActions", new object[1]
                    {
                        false
                    });
                }, 4000L);
                return true;
            }
            return false;
        }

        [RemoteEvent("Pressed_KOMMA")]
        public static void komma(Client client)
        {
            if (Database.getItemCount(client.Name, "Verbandskasten") >= 1)
            {
                if (!client.HasData("PLAYER_ISFARMING"))
                {
                    Database.changeInventoryItem(client.Name, "Verbandskasten", 1, true);
                    client.TriggerEvent("disableAllPlayerActions", new object[1]
                    {
                    true
                    });
                    client.TriggerEvent("sendProgressbar", new object[1]
                    {
                    4000
                    });
                    client.SetData("PLAYER_ISFARMING", true);
                    NAPI.Player.PlayPlayerAnimation(client, 33, "amb@medic@standing@tendtodead@idle_a", "idle_a", 8f);
                    NAPI.Task.Run(delegate
                    {
                        client.Health = 100;
                        client.TriggerEvent("client:respawning");
                        client.ResetData("PLAYER_ISFARMING");
                        NAPI.Player.StopPlayerAnimation(client);
                        client.TriggerEvent("componentServerEvent", new object[2]
                        {
                        "Progressbar",
                        "StopProgressbar"
                        });
                        client.TriggerEvent("disableAllPlayerActions", new object[1]
                        {
                        false
                        });
                    }, 4000L);
                }
            }
        }
    }
}
