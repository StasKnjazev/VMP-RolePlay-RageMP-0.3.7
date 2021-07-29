using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Players
{
	class Window
	{
		public Window(string windowName)
		{

			WindowName = windowName;
		}
		public bool Enabled { get; set; }
		public string WindowName { get; set; }
	}
}
