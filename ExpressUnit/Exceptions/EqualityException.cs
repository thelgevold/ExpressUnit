using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnitModel
{
    public class EqualityException : UnitTestFailedException
    {
        private string message;
        public EqualityException(object expected,object actual)
        {
            string expectedValue = "null";
            string actualValue = "null";
            
            if (expected != null)
            {
                expectedValue = expected.ToString();
            }
            if (expectedValue == string.Empty)
            {
                expectedValue = "\"\"";
            }

            if (actual != null)
            {
                actualValue = actual.ToString();
            }
            if (actualValue == string.Empty)
            {
                actualValue = "\"\"";
            }

            message = string.Format("The expected value is: [{0}], but the actual value is: [{1}]",expectedValue,actualValue);
                
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
