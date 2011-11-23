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
using System.Collections;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class MethodManagerTest
    {
        private MethodManager manager = new MethodManager();

        [TestSetup]
        public void TestSetup()
        {
        }

        [TestTearDown]
        public void TestTearDown()
        {
        }

        [UnitTest(UseCase = "All tests methods for a type will be located")]
        [ExceptionThrown(typeof(ArgumentException))]
        public void MethodManager_GetTestsInTestClass_Will_Throw_Exception_Argument_Exception_If_Test_Type_Is_Undefined_Test()
        {
            manager.GetTestsInTestClass(typeof(MethodManagerTest), "Undefined",null);
        }

        [UnitTest]
        public void MethodManager_GetTestsInTestClass_Will_Find_Test_Setup_Method_On_For_Every_Test()
        {
            IList<TestMethod> tests = manager.GetTestsInTestClass(typeof(MethodManagerTest), TestType.All, null);
            Confirm.IsTrue(tests.All(t => t.TestSetup != null));
        }

        [UnitTest]
        public void MethodManager_GetTestsInTestClass_Will_Find_Test_Tear_Down_Method_On_For_Every_Test()
        {
            IList<TestMethod> tests = manager.GetTestsInTestClass(typeof(MethodManagerTest), TestType.All, null);
            Confirm.IsTrue(tests.All(t => t.TestTearDown != null));
        }

        [UnitTest(UseCase = "All tests methods for a type will be located")]
        public void MethodManager_GetTestsInTestClass_Will_Find_Five_Unit_Tests_And_One_Integration_Test_In_Current_Type_Test()
        {
            IList<TestMethod> tests = manager.GetTestsInTestClass(typeof(MethodManagerTest), TestType.All, null);

            Confirm.Equal(6, tests.Count);

            TestMethod method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Test_Setup_Method_On_For_Every_Test").Single();
            EnsureCorrectTestResult(method);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Test_Tear_Down_Method_On_For_Every_Test").Single();
            EnsureCorrectTestResult(method);

            
            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_One_Integration_Test_In_Current_Type_Test").Single();
            EnsureCorrectTestResult(method);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Five_Unit_Tests_And_One_Integration_Test_In_Current_Type_Test").Single();
            EnsureCorrectTestResult(method);
            Confirm.Equal(System.Reflection.MethodBase.GetCurrentMethod(), method.MemberInfo);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Five_Unit_Tests_In_Current_Type_Test").Single();
            EnsureCorrectTestResult(method);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Throw_Exception_Argument_Exception_If_Test_Type_Is_Undefined_Test").Single();
            EnsureCorrectTestResult(method);
            
        }

        [UnitTest(UseCase = "All tests methods for a type will be located")]
        public void MethodManager_GetTestsInTestClass_Will_Find_Five_Unit_Tests_In_Current_Type_Test()
        {
            IList<TestMethod> tests = manager.GetTestsInTestClass(typeof(MethodManagerTest), TestType.UnitTest, null);

            Confirm.Equal(5, tests.Count);

            TestMethod method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Test_Setup_Method_On_For_Every_Test").Single();
            EnsureCorrectTestResult(method);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Test_Tear_Down_Method_On_For_Every_Test").Single();
            EnsureCorrectTestResult(method);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Five_Unit_Tests_And_One_Integration_Test_In_Current_Type_Test").Single();
            EnsureCorrectTestResult(method);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_Five_Unit_Tests_In_Current_Type_Test").Single();
            EnsureCorrectTestResult(method);
            Confirm.Equal(System.Reflection.MethodBase.GetCurrentMethod(), method.MemberInfo);

            method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Throw_Exception_Argument_Exception_If_Test_Type_Is_Undefined_Test").Single();
            EnsureCorrectTestResult(method);
        }

        [IntegrationTest(UseCase = "All tests methods for a type will be located")]
        public void MethodManager_GetTestsInTestClass_Will_Find_One_Integration_Test_In_Current_Type_Test()
        {
            IList<TestMethod> tests = manager.GetTestsInTestClass(typeof(MethodManagerTest), TestType.IntegrationTest, null);

            Confirm.Equal(1, tests.Count);
            TestMethod method = tests.Where(t => t.Name == "MethodManager_GetTestsInTestClass_Will_Find_One_Integration_Test_In_Current_Type_Test").Single();

            Confirm.Equal(System.Reflection.MethodBase.GetCurrentMethod(), method.MemberInfo);
            EnsureCorrectTestResult(method);
        }

        private void EnsureCorrectTestResult(TestMethod method)
        {
            Confirm.IsTrue(method.TestSetup.Name == "TestSetup");

            Confirm.IsTrue(method.TestTearDown.Name == "TestTearDown");

            Confirm.Equal(typeof(MethodManagerTest), method.Type);
            Confirm.Equal("Yellow", method.Color);
        }
    }
}
