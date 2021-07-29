using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc
{
    class Daten
    {
        public static string database = "";
        public static string username = "";
        public static string password = "";
        public static string host = "";

        public static void setDatabaseData()
        {
            if(Start.ReleaseBuild)
            {
                database = "gvmp";
                username = "gvmp";
                password = "gvmp";
                host = "localhost";
            } else {
				database = "gvmp";
				username = "gvmp";
				password = "gvmp";
				host = "localhost";
			}
        }

    }
}
