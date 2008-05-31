using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnit
{
    public partial class Confirm
    {
        /// <summary>
        /// Compares the two values.  ComparerException is thrown if comp1 isn't greater than comp2
        /// </summary>
        /// <param name="comp1"></param>
        /// <param name="comp2"></param>
        /// <returns></returns>
        public static bool IsGreater(long comp1, long comp2)
        {
            bool val = comp1 > comp2;

            if (val == false)
            {
                throw new ComparerException();
            }
           
            return true;
        }

        /// <summary>
        /// Compares the two values.  ComparerException is thrown if comp1 isn't greater than comp2
        /// </summary>
        /// <param name="comp1"></param>
        /// <param name="comp2"></param>
        /// <returns></returns>
        public static bool IsGreater(double comp1, double comp2)
        {
            bool val = comp1 > comp2;

            if (val == false)
            {
                throw new ComparerException();
            }

            return true;
        }

        /// <summary>
        /// Equals does its comparison based on object.Equals().  If both objects are null, they are 
        /// considered to be equal.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        public new static bool Equals(object expected, object actual)
        {
            if (expected == null && actual == null)
            {
                return true;
            }
            else if (expected == null)
            {
                throw new EqualityException(expected, actual);
            }
            else if (actual == null)
            {
                throw new EqualityException(expected, actual);
            }
            else
            {
                if (expected.Equals(actual))
                {
                    return true;
                }
            }

            throw new EqualityException(expected, actual);
        }

        
        
        public static bool ExceptionThrown(Type expectedExceptionType,TargetMethod target)
        {
            try
            {
                target();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == expectedExceptionType)
                {
                    return true;
                }
                throw new ExceptionNotThrownException(expectedExceptionType,ex);
            }

            throw new ExceptionNotThrownException(expectedExceptionType, null);
        }

    }
}
