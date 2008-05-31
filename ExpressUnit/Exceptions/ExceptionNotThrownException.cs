using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnit
{

    public class ExceptionNotThrownException : UnitTestFailedException
    {
        private string message;

        public ExceptionNotThrownException(Type expectedException, Exception actualException)
        {
            if (actualException == null)
            {
                message = string.Format("{0} was not thrown", expectedException.ToString());
            }
            else
            {
                message = string.Format("{0} was not thrown, but {1} was", expectedException.ToString(),expectedException.GetType().ToString());
            }
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
