using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Other
{
    public static class Utils
    {
        public static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val)
        {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict)
            {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }

        public static string FirstletterUpper(string str)
        {
            return str[0].ToString().ToUpper() + str.Substring(1).ToLower();
        }
	}
}
