/*
Copyright (C) 2009  Torgeir Helgevold

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using ExpressUnitModel;
using System.Collections;
using XPathItUp;

namespace ExpressUnitModel
{
    public partial class Confirm
    {
       
        /// <summary>
        /// Compares the two values.  ComparerException is thrown if comp1 isn't greater than comp2
        /// </summary>
        /// <param name="comp1"></param>
        /// <param name="comp2"></param>
        /// <returns></returns>
        public static bool IsGreater(long comp1, long comp2,string errorMessage = null)
        {
            bool val = comp1 > comp2;

            if (val == false)
            {
                if (errorMessage == null)
                {
                    throw new ComparerException(string.Format("{0} is not greater than {1}", comp1, comp2));
                }
                else
                {
                    throw new ComparerException(string.Format("{0} is not greater than {1} Error:{2}", comp1, comp2,errorMessage));
                }
            }
           
            return true;
        }

        /// <summary>
        /// Compares the two values.  ComparerException is thrown if comp1 isn't greater than comp2
        /// </summary>
        /// <param name="comp1"></param>
        /// <param name="comp2"></param>
        /// <returns></returns>
        public static bool IsGreater(double comp1, double comp2, string errorMessage = null)
        {
            bool val = comp1 > comp2;

            if (val == false)
            {
                if (errorMessage == null)
                {
                    throw new ComparerException(string.Format("{0} is not greater than {1}", comp1, comp2));
                }
                else
                {
                    throw new ComparerException(string.Format("{0} is not greater than {1} Error:{2}", comp1, comp2, errorMessage));
                }
            }

            return true;
        }

        public static bool SameCollections(IEnumerable comp1, IEnumerable comp2)
        {
            if (comp1 == null && comp2 == null)
            {
                return true;
            }
            else if (comp1 == null)
            {
                return false;
            }
            else if (comp2 == null)
            {
                return false;
            }

            IList list1 = new List<object>();
            IList list2 = new List<object>();

            IEnumerator e1 = comp1.GetEnumerator();

            while (e1.MoveNext())
            {
                list1.Add(e1.Current);
            }

            IEnumerator e2 = comp2.GetEnumerator();

            while (e2.MoveNext())
            {
                list2.Add(e2.Current);
            }

            if (CompareCollections(list1, list2) == false)
            {
                throw new EqualityException(comp1, comp2);
            }

            return true;
        }

        
        private static bool CompareCollections(IList col1, IList col2)
        {
            if (col1.Count != col2.Count)
            {
                return false;
            }

            for (int i = 0; i < col1.Count; i++)
            {
                if (col1[i].Equals(col2[i]) == false)
                {
                    return false;
                }
            }
            
            return true;
        }

        public static bool IsTrue(bool value,string errorMessage = null)
        {
            if (value != true)
            {
                throw new EqualityException(true, false,errorMessage);
            }

            return true;
        }

        public static bool IsFalse(bool value,string errorMessage = null)
        {
            if (value != false)
            {
                throw new EqualityException(false, true,errorMessage);
            }

            return true;
        }

        public static bool IsNull(object value, string errorMessage = null)
        {
            if (value != null)
            {
                throw new EqualityException(null, value, errorMessage);
            }

            return true;
        }

        public static bool IsNotNull(object value, string errorMessage = null)
        {
            if (value == null)
            {
                throw new UnequalException(null, value, errorMessage);
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
        public static bool Equal(object expected, object actual, string errorMessage = null)
        {
            if (expected == null && actual == null)
            {
                return true;
            }
            else if (expected == null)
            {
                throw new EqualityException(expected, actual,errorMessage);
            }
            else if (actual == null)
            {
                throw new EqualityException(expected, actual,errorMessage);
            }
            else
            {
                if (expected.Equals(actual))
                {
                    return true;
                }
            }

            throw new EqualityException(expected, actual, errorMessage);
        }

        [Obsolete("Use Equal instead")]
        public static bool Equals(object expected, object actual, string errorMessage = null)
        {
            return Equal(expected, actual, errorMessage);
        }

        public static bool Different(object notExpected, object actual, string errorMessage = null)
        {
            if (notExpected == null && actual == null)
            {
                throw new UnequalException(notExpected, actual,errorMessage);
            }
            else if (notExpected == null)
            {
                return true;
            }
            else if (actual == null)
            {
                return true;
            }
            else
            {
                if (notExpected.Equals(actual) == false)
                {
                    return true;
                }
            }

            throw new UnequalException(notExpected, actual,errorMessage);
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
