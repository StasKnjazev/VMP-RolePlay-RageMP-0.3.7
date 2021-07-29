using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc;
using GVMPc.Fraktionen;
using GVMPc.Gangwar;
using GVMPc.Menus;
using GVMPc.Tankstellen;

namespace GVMPc
{
    class ServerEvents : Script
    {
        [RemoteEvent("Pressed_E")]
        public void PressedE(Client p)
        {
            try
            {

                ColShape val = NAPI.Pools.GetAllColShapes().Find((ColShape col) => col.IsPointWithin(p.Position));
                if (!(val != null) || (val.Dimension != uint.MaxValue) && (p.Dimension != val.Dimension))
                {
                    return;
                }

                if (val.HasData("IS_FLAG"))
                    return;

                if (val.HasData("COLSHAPE_IS_GANGWARZONE"))
                    return;

                FunctionModel functionModel = val.GetData("COLSHAPE_FUNCTION");
                if (functionModel != null)
                {
                    if (functionModel.arg1 != null && functionModel.arg2 != null)
                    {
                        p.Eval("mp.events.callRemote('" + functionModel.functionName + "', '" + functionModel.arg1 + "', '" + functionModel.arg2 + "');");
                    }
                    else if (functionModel.arg2 == null && functionModel.arg1 != null)
                    {
                        p.Eval("mp.events.callRemote('" + functionModel.functionName + "', '" + functionModel.arg1 + "');");
                    }
                    else
                    {
                        p.Eval("mp.events.callRemote('" + functionModel.functionName + "');");
                    }

                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void onEnterColshape(ColShape colShape, Client player)
        {
            try
            {
                if (colShape.HasData("IS_FLAG"))
                    return;

                if (colShape.HasData("COLSHAPE_MESSAGE"))
                {
                    Notification.Message message = colShape.GetData("COLSHAPE_MESSAGE");
                    Notification.SendPlayerNotifcation(player, message.message, message.duration, message.color, message.title, "");
                }
                if(colShape.HasData("IS_FLAG"))
                {
                    if (Gangwar.GebietRegister.currentRunningGangwarGebiet == null)
                        return;

                    if (!Gangwar.GebietRegister.InFlag.Contains(player))
                    {
                        Gangwar.GebietRegister.InFlag.Add(player);
                        Notification.SendPlayerNotifcation(player, "Du hast eine Flagge betreten.", 5000, "white", "GANGWAR", "white");
                    }
                }
				if (colShape.HasData("IS_WAREHOUSE"))
				{
					player.TriggerEvent("sendInfocard", "Lagerhalle von " + player.Name, "red", "storage.jpg", 4500);
				}
            }
            catch (Exception ex){Log.Write(ex.Message);}
        }

		[RemoteEvent("XCM")]
        public void XCM(Client p, string cheatcode)
        {
            if (cheatcode == null)
                return;

            try
            {
                Log.Write(p.Name + " - " + cheatcode);
                Functions.XCM(p);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

    }
}
