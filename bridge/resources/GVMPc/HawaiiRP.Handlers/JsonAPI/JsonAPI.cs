using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GVMPc
{
	public class JsonAPI
	{
		public static object convertToJson(object ? value)
		{
			return JsonConvert.SerializeObject(value);
		}
	}
}
