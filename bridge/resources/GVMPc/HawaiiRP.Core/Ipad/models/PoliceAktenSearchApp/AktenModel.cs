using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Ipad
{
	public class AktenModel
	{
		public string title
		{
			get;
			set;
		}

		public string created
		{
			get;
			set;
		}

		public string closed
		{
			get;
			set;
		}

		public string officer
		{
			get;
			set;
		}

		public bool open
		{
			get;
			set;
		}

		public int actnumber
		{
			get;
			set;
		}

		public string text
		{
			get;
			set;
		}

		public AktenModel(string title, string created, string closed, string officer, bool open, int number, string text)
		{
			this.title = title;
			this.created = created;
			this.closed = closed;
			this.officer = officer;
			this.open = open;
			this.actnumber = number;
			this.text = text;
		}
	}
}
