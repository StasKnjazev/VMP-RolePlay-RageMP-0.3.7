using GTANetworkAPI;
using System;

namespace GVMPc
{
    public class Notification
    {
        public class Message
        {
            public string message
            {
                get;
                set;
            }

            public string title
            {
                get;
                set;
            }

            public string color
            {
                get;
                set;
            }

            public int duration
            {
                get;
                set;
            }

            public Message(string message, string title, string color, int duration)
            {
                this.message = message;
                this.title = title;
                this.color = color;
                this.duration = duration;
            }
        }

        public enum icon
        {
            glob,
            gov,
            dev,
            wed,
            casino
        }

        public static void SendGlobalNotification(Client player, string message, int durationInMS, string color, icon ico)
        {
            string text = "";
            if (ico == icon.glob)
            {
                text = "glob";
            }
            if (ico == icon.gov)
            {
                text = "gov";
            }
            if (ico == icon.dev)
            {
                text = "dev";
            }
            if (ico == icon.wed)
            {
                text = "wed";
            }
            if (ico == icon.casino)
            {
                text = "casino";
            }
            player.TriggerEvent("sendGlobalNotification", new object[4]
            {
                message,
                durationInMS,
                color,
                text
            });
        }

        public static void SendGlobalNotification(Client player, string message, int durationInMS, string color, string ico)
        {
            player.TriggerEvent("sendGlobalNotification", new object[4]
            {
                message,
                durationInMS,
                color,
                ico
            });
        }

        public static void SendGlobalNotification(string message, int durationInMS, string color, icon ico)
        {
            foreach(Client target in NAPI.Pools.GetAllPlayers())
                target.TriggerEvent("sendGlobalNotification", new object[4]
                {
                    message,
                    durationInMS,
                    color,
                    ico
                });
        }

        public static void SendPlayerNotifcation(Client player, string message, int durationInMS, string color, string title, string bgcolor)
        {
            try
            {
                player.TriggerEvent("sendPlayerNotification", new object[5]
                {
                message,
                durationInMS,
                color,
                color,
                title
                });
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
