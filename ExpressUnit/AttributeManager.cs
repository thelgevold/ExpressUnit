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
using ExpressUnitModel;

namespace ExpressUnit
{
    public class AttributeManager
    {
        public static bool ExceptionIsExcepted(MemberInfo method, Exception ex)
        {
            object[] attribute = method.GetCustomAttributes(typeof(ExceptionThrown), true);
            foreach (ExceptionThrown exAtt in attribute)
            {
                if (ex.InnerException != null)
                {
                    if (exAtt.ExceptionType == ex.InnerException.GetType())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
