using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc
{
    class Functions : Script
    {
        public static List<string> handcuffed = new List<string>();

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static void disableAllPlayerControls(Client p, bool value)
        {
            p.TriggerEvent("disableAllPlayerActions", value);
        }

        public static void XCM(Client p)
        {
            try
            {
                //Discord.DiscordWebhooks.SendMessage("Spieler gebannt", "Der Spieler " + p.Name + " hat durch das automatische System einen permanenten Communityausschluss erhalten.", Discord.DiscordWebhooks.banWebhook, "Ban-Log");
                Database.banPlayer(p.SocialClubName, "Automatischer Sicherheitsbann");
                Notification.SendGlobalNotification("Der Spieler " + p.Name + " hat durch das automatische System einen permanenten Communityausschluss erhalten.", 8000, "#f30202", Notification.icon.dev);
                p.Kick();
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
        
        public static void setPlayerOnGround(Client p)
        {
            p.TriggerEvent("setPlayerOnGround");
        }

        public static void setEnableHandcuffs(Client p, bool value)
        {
            p.TriggerEvent("setEnableHandcuffs", value);
        }

        public static Fraktionen.Fraktion getFraktionFromName(string name)
        {
            foreach(Fraktionen.Fraktion fraktion in Fraktionen.FraktionRegister.fraktionList)
            {
                if(name == fraktion.fraktionName)
                {
                    return fraktion;
                }
            }
            return new Fraktionen.Fraktion("", "", new Vector3(), new Vector3(), new Vector3(), 0, new Vector3(), new Vector3(), 0.0f, 1, new Color(0, 0, 0), new List<ClothingShops.ClothingModel>(), true);
        }

        public static void setHandcuff(Client p, bool value)
        {
            try
            {
                if (value == true)
                {
                    handcuffed.Add(p.Name);
                    NAPI.Player.PlayPlayerAnimation(p, 33, "mp_arresting", "idle");
                }

                if (value == false)
                {
                    if (handcuffed.Contains(p.Name))
                        handcuffed.Remove(p.Name);

                    p.StopAnimation();
                }


                p.TriggerEvent("setHandcuff", value);
				p.TriggerEvent("updateCuffed", value);
                disableAllPlayerControls(p, value);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
