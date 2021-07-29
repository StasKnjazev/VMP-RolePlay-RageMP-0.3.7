using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Players
{
	class LoginWindow : Window
	{
		public LoginWindow() : base("Login")
		{

		}
	}

	class LoginWindowObject
	{
		public LoginWindowObject(string name)
		{
			this.name = name;
		}

		public string name { get; set; }
	}
}
