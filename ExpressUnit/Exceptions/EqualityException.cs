using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnit
{
    public class EqualityException : UnitTestFailedException
    {
        private string message;
        public EqualityException(object expected,object actual)
        {
            message = "Expected and actual value did not match";    
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
