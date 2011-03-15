using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnitModel
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
