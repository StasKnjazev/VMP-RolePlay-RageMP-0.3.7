using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Fraktionen
{
    class FraktionsLager : Script
    {

        [RemoteEvent("fraklager:inlager")]
        public void FraklagerInlager(Client p, string item, int count)
        {
            if (item == null || count == 0)
                return;

            if (count < 1)
                return;

            try
            {
                if (Database.getItemCount(p.Name, item) >= count)
                {
                    //Discord.DiscordWebhooks.SendMessage("Spieler legt Item in Fraklager.", "Der Spieler " + p.Name + " legt " + count + "x " + item + " im das Fraklager von Fraktion " + p.GetSharedData("FRAKTION_SHORT") + ".", Discord.DiscordWebhooks.fraklagerWebhook, "Fraklager-Log");
                    Database.changeInventoryItem(p.Name, item, count, true);
                    Database.changeFraklagerItem(p.GetSharedData("FRAKTION"), item, count, false);
                    Notification.SendPlayerNotifcation(p, "Du hast " + count + "x " + item + " in das Fraktionslager gelegt.", 5000, "white", p.GetSharedData("FRAKTION"), "rgb(" + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Red + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Green + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Blue + ")");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("fraklager:ininventory")]
        public void FraklagerInInventar(Client p, string item, int count)
        {
            if (item == null || count == 0)
                return;

            if (count < 1)
                return;

            try
            {
                if (Database.getFraklagerItems(p.GetSharedData("FRAKTION")).ContainsKey(item) && Database.getFraklagerItems(p.GetSharedData("FRAKTION"))[item] >= count)
                {
                    if (p.GetSharedData("FRAKTION_RANK") > 9)
                    {
                        //Discord.DiscordWebhooks.SendMessage("Spieler holt Item aus Fraklager.", "Der Spieler " + p.Name + " holt " + count + "x " + item + " aus dem Fraklager von Fraktion " + p.GetSharedData("FRAKTION_SHORT") + ".", Discord.DiscordWebhooks.fraklagerWebhook, "Fraklager-Log");
                        Database.changeFraklagerItem(p.GetSharedData("FRAKTION"), item, count, true);
                        Database.changeInventoryItem(p.Name, item, count, false);
                        Notification.SendPlayerNotifcation(p, "Du hast " + count + "x " + item + " aus dem Fraktionslager genommen.", 5000, "white", p.GetSharedData("FRAKTION"), "rgb(" + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Red + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Green + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Blue + ")");
                    }
                    else
                    {
                        Notification.SendPlayerNotifcation(p, "Du bist nicht dazu berechtigt, etwas aus dem Fraktionslager zu nehmen.", 5000, "white", p.GetSharedData("FRAKTION"), "rgb(" + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Red + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Green + ", " + Database.getFraktionByName(p.GetSharedData("FRAKTION")).rgbColor.Blue + ")");
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("fraklager:requestitems")]
        public void requestItems(Client p)
        {
            try
            {
                Dictionary<string, decimal> items = Database.getFraklagerItems(p.GetSharedData("FRAKTION"));

                foreach (KeyValuePair<string, decimal> item in items)
                {
                    p.TriggerEvent("fraklager:additem", item.Key, item.Value, Database.getItemURL(item.Key));
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

    }
}
