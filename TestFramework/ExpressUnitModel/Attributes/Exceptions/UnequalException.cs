using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnitModel
{
    public class UnequalException : UnitTestFailedException
    {
        public UnequalException(object notExpected, object actual)
        {
            string notExpectedValue = "null";
        
            if (notExpected != null)
            {
                notExpectedValue = notExpected.ToString();
            }

            if (notExpectedValue == string.Empty)
            {
                notExpectedValue = "\"\"";
            }

            Message = string.Format("The the actual value should not be equal to {0}", notExpectedValue);
                
        }

        public override string  Message
        {
            get;
            set;
	    }
    }
}
