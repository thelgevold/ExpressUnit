using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnitModel
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Ignore : Attribute
    {
    }
}
