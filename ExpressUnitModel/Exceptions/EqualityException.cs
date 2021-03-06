﻿/*
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
    public class EqualityException : UnitTestFailedException
    {
        public EqualityException(string message) : base(message) { }

        public EqualityException(object expected,object actual,string errorMsg = null):this(CreateErrorMessage(expected,actual,errorMsg)){}
      
        private static string CreateErrorMessage(object expected, object actual, string errorMsg = null)
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

            if (errorMsg == null)
            {
                return string.Format("The expected value is: [{0}], but the actual value is: [{1}]", expectedValue, actualValue);
            }
            else
            {
                return string.Format("The expected value is: [{0}], but the actual value is: [{1}] Error: {2}", expectedValue, actualValue, errorMsg);
            }
        }
    }
}
