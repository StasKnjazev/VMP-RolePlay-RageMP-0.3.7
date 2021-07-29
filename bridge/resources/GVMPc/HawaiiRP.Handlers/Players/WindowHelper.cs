using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using GVMPc;

namespace GVMPc.Players
{
	class WindowHelper
	{
		public static WindowHelper Instance { get; } = new WindowHelper();

		public void Show(Client client, Window window, params object[] windowArguments)
		{
			if (!window.Enabled) return;

			client.TriggerEvent("openWindow", window.WindowName, NAPI.Util.ToJson(windowArguments));
		}
	}
}
