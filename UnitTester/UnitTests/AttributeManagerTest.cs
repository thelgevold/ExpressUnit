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
using ExpressUnitModel;
using ExpressUnit;

namespace UnitTester.Tests
{
    [TestClass]
    public class AttributeManagerTest
    {
        [UnitTest(UseCase="Test Use Case")]
        [ExceptionThrown(typeof(DivideByZeroException))]
        public void ExceptionIsExcepted_Returns_True_For_Expected_Exception_Test()
        {
            Exception ex = new Exception("", new DivideByZeroException());
            Confirm.Equal(true,AttributeManager.ExceptionIsExcepted(System.Reflection.MethodBase.GetCurrentMethod(), ex));

            // must do this to satisfy DivideByZeroException check
            throw new DivideByZeroException();
        }
    }
}
