using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Menus
{
    public class NativeManager : Script
    {

        public static Dictionary<Client, Vector3> openMenus = new Dictionary<Client, Vector3>();

        public static void PosCheck()
        {
            try
            {
                foreach (KeyValuePair<Client, Vector3> item in NativeManager.openMenus)
                {
                    if (!NAPI.Pools.GetAllPlayers().Contains(item.Key))
                    {
                        NativeManager.openMenus.Remove(item.Key);
                        return;
                    }

                    if (item.Key.Position.DistanceTo(NativeManager.openMenus[item.Key]) > 10.0f)
                    {
                        NativeMenu.closeNativeMenu(item.Key);
                    }
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

    }
}
