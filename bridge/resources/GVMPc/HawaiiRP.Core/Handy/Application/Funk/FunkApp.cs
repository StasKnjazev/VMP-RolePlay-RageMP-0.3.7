using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using GTANetworkAPI;

namespace GVMPc.Handy
{
    class FunkApp : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            Log.Write("Funkapp wurde geladen");
        }

        [RemoteEvent("changeSettings")]
        public void changeSettings(Client p, int setting)
        {
            if(setting == 0)
            {
                p.Eval("mp.events.callRemote('server:leaveradio')");
            }
            else if (setting == 1)
            {
                p.SetSharedData("isTalkingFunk", false);
            }
            else if (setting == 2)
            {
                p.SetSharedData("isTalkingFunk", true);
            }
        }

        [RemoteEvent("joinFunk")]
        public void JoinFunk(Client p, string radio)
        {
            try
            {
                bool encrypted = false;

                foreach (Fraktionen.Fraktion fraktion in Fraktionen.FraktionRegister.fraktionList)
                {
                    try
                    {
                        int radio2 = 0;
                        if (!String.IsNullOrWhiteSpace(radio) && int.TryParse(radio, out radio2) && Convert.ToDouble(radio2) == fraktion.fraktionsDimension)
                        {
                            encrypted = true;
                            if (p.GetSharedData("FRAKTION") == fraktion.fraktionName)
                            {
                                p.Eval("mp.events.callRemote('server:joinradio', " + radio2 + ")");
                                return;
                            }
                        }
                    }
                    catch (Exception ex) { Log.Write("[EXCEPTION " + new StackTrace().GetFrame(0).GetMethod() + "] " + ex.Message); Log.Write("[EXCEPTION " + new StackTrace().GetFrame(0).GetMethod() + "] " + ex.StackTrace); }
                }
                if (encrypted)
                {
                    Notification.SendPlayerNotifcation(p, "Dieser Funkkanal ist verschlüsselt.", 5000, "red", "FUNK", "");
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du bist Funkkanal " + radio + " MHz beigetreten.", 5000, "green", "FUNK", "");
                    p.Eval("mp.events.callRemote('server:joinradio', " + radio + ")");
                }
            }
            catch (Exception ex) 
            { 
                Log.Write("[EXCEPTION " + new StackTrace().GetFrame(0).GetMethod() + "] " + ex.Message); 
                Log.Write("[EXCEPTION " + new StackTrace().GetFrame(0).GetMethod() + "] " + ex.StackTrace); 
            }
        }
    }
}
