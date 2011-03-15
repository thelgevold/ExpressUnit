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

namespace ExpressUnitModel
{
    public class UnequalException : UnitTestFailedException
    {
        public UnequalException(string error)
            : base(error)
        {
        }

        public UnequalException(object notExpected, object actual, string errorMessage = null)
            : this(CreateErrorMessage(notExpected,actual,errorMessage))
        {
        }

        private static string CreateErrorMessage(object notExpected, object actual,string errorMessage = null)
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

            if (errorMessage == null)
            {
                return string.Format("The the actual value should not be equal to {0}", notExpectedValue);
            }
            else
            {
                return string.Format("The the actual value should not be equal to {0} Error: {1}", notExpectedValue,errorMessage);
            }
        }
    }
}
