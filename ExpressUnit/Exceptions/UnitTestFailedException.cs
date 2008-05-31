using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnit
{
    public abstract class UnitTestFailedException : Exception
    {
        public new abstract string Message
        {
            get;

            set;

        }
    }
}
