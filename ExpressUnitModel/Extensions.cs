using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPathItUp;

namespace ExpressUnitModel
{
    public static class Extensions
    {
        public static bool IsPresentNow(this IBase ibase)
        {
            ibase.ToXPathExpression();
            return true;
        }

        public static bool IsNotPresentNow(this IBase ibase)
        {
            ibase.ToXPathExpression();
            return true;
        }

        public static bool IsPresentInLessThan(this IBase ibase,int seconds)
        {
            ibase.ToXPathExpression();
            return true;
        }

    }
}
