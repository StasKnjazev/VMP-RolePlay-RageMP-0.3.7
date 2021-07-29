using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc
{
    public class FunctionModel
    {
        public string functionName
        {
            get;
            set;
        }

        public string arg1
        {
            get;
            set;
        } = null;

        public string arg2
        {
            get;
            set;
        } = null;

        public FunctionModel(string functionName, string arg1, string arg2)
        {
            this.functionName = functionName;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public FunctionModel(string functionName, string arg1)
        {
            this.functionName = functionName;
            this.arg1 = arg1;
        }

        public FunctionModel(string functionName)
        {
            this.functionName = functionName;
        }
	}
}
