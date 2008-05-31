using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnit
{

    public class ComparerException : UnitTestFailedException
    {
        private string message;

        public ComparerException()
        {
            message = "Object is not greater than compared object";
        }
        public override string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        
    }
}
