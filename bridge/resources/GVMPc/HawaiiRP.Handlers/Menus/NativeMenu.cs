using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Menus
{
	public class NativeMenu
	{
		public string Title
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public List<NativeItem> Items
		{
			get;
			set;
		}

		public NativeMenu(string Title, string Description, List<NativeItem> Items)
		{
			this.Title = Title;
			this.Description = Description;
			this.Items = Items;
		}

		

		public void showNativeMenu(Client p)
		{
			p.SetData("PLAYER_CURRENT_NATIVEMENU", (object)this);
			p.TriggerEvent("componentServerEvent", "NativeMenu", "showNativeMenu", NAPI.Util.ToJson((object)this), 0);

		}

		public static void closeNativeMenu(Client p)
        {
			p.TriggerEvent("componentServerEvent", "NativeMenu", "hide");
		}


		
	}
}
