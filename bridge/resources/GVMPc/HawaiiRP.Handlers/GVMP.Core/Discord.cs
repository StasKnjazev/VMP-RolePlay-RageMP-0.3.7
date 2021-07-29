/*using System;
using System.Net.Http;
using System.Text;

namespace GVMP.Core
{
    public class Discord
    {
        public enum COLORS
        {
            RED = 13632027,
            GREEN = 4289797,
            BLUE = 4886754,
            ORANGE = 16098851,
            BLACK = 1,
            WHITE = 0xFFFFFF,
            GREY = 10197915,
            YELLOW = 16312092,
            BROWN = 9131818,
            CYAN = 5301186
        }

        public static async void SendMessage(string title, string msg, string fields = "[]", COLORS color = COLORS.GREY, string webhook = Webhook.ALL)
        {
            DateTime now = DateTime.Now;
            string stringPayload = "{\"username\":\"HawaiiRoleplay-Bot\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/814917803270078564/834399589737955368/G0zOGjQ_1.png\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"HawaiiRoleplay Bot\",\"icon_url\":\"https://cdn.discordapp.com/attachments/814917803270078564/834399589737955368/G0zOGjQ_1.png\"},\"description\":\"Es wurde am **" + now.Day + "." + now.Month + "." + now.Year + " | " + now.Hour + ":" + now.Minute + "** ein Admin-Log ausgelöst.\",\"color\":16711680,\"fields\":[{\"name\":\"" + title + "\",\"value\":\"" + msg + "\",\"inline\":true}],\"footer\":{\"text\":\"Hawaii Roleplay | (c) hawaiiroleplay.com 2021\"}}]}";
            StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using HttpClient httpClient = new HttpClient();
            await httpClient.PostAsync(webhook, httpContent);
        }

        public static async void SendServerStatusLog(string title, string msg, COLORS color = COLORS.BLACK, string webhook = Webhook.STATUS)
        {
            DateTime now = DateTime.Now;
            //string stringPayload = "{\"username\":\"Webhook\",\"avatar_url\":\"https://cdn.discordapp.com/icons/2ec92db9f314782653e5fad4af318b92.webp\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"Spidey Bot\",\"icon_url\":\"https://cdn.discordapp.com/icons/735669217705328817/3facf11255b39acb25771e40da6780b2.webp\"},\"title\":\"Admin-Log\",\"thumbnail\":{\"url\":\"https://cdn.discordapp.com/icons/2ec92db9f314782653e5fad4af318b92.webp\"},\"description\":\"Es wurde am **" + now.Day + "." + now.Month + "." + now.Year + " | " + now.Hour + ":" + now.Minute + "** ein Admin-Log ausgelöst.\",\"color\": 1,\"fields\":[{\"name\":\"" + title + "\",\"value\":\"" + msg + "\",\"inline\":true}],\"footer\":{\"text\":\"Perico Roleplay | Admin-Log Bot (c) 2020\"}}]}";
            string stringPayload = "{\"username\":\"Server Status\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/814917803270078564/834399589737955368/G0zOGjQ_1.png\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"HawaiiRoleplay\",\"icon_url\":\"https://cdn.discordapp.com/attachments/814917803270078564/834399589737955368/G0zOGjQ_1.png\"},\"title\":\"" + title+ "\",\"description\":\""+msg+ "\",\"color\": 1, \"footer\": {\"text\": \"Hawaii Roleplay | (c) hawaiiroleplay.com 2021\",\"icon_url\":\"https://cdn.discordapp.com/attachments/814917803270078564/834399589737955368/G0zOGjQ_1.png\"}}]}";
            StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using HttpClient httpClient = new HttpClient();
            await httpClient.PostAsync(webhook, httpContent);
        }
    }
} */
