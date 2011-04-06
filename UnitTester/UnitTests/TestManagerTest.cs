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
using System.Reflection;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class TestManagerTest
    {
        private bool testSetupRan = false;

        [TestSetup]
        public void TestSetup()
        {
            testSetupRan = true;
        }

        [UnitTest]
        public void TestManager_RunTest_Will_Run_Passing_Test_Test()
        {
            TestManager manager = new TestManager();
            TestMethod method = new TestMethod();
            method.Name = "PassingStub";
            method.Type = typeof(TestManagerTest);
            method.MemberInfo = PassingStub();

            TestResult res = manager.RunTest(method);
            Confirm.Equal(true, res.Passed);
            Confirm.Equal("PassingStub (PASSED)", res.ResultText);
        }

        [UnitTest]
        public void TestManager_RunTest_Will_Run_Failing_Test_When_Unhandled_Exception_Is_Thrown_Test()
        {
            TestManager manager = new TestManager();
            TestMethod method = new TestMethod();
            method.Name = "FailingStubWithUnhandledException";
            method.Type = typeof(TestManagerTest);
           
            Type t = typeof(TestManagerTest);
            var q = from m in t.GetMembers()
                    where m.Name == "FailingStubWithUnhandledException"
                    select m;

            method.MemberInfo = q.Single<MemberInfo>();

            TestResult res = manager.RunTest(method);
            Confirm.Equal(false, res.Passed);
            Confirm.Equal("Argument Invalid", res.ResultText);
        }

        

        [UnitTest]
        public void TestManager_RunTest_Will_Run_Failing_Test_When_Confirm_Equals_Fails_Test()
        {
            TestManager manager = new TestManager();
            TestMethod method = new TestMethod();
            method.Name = "FailingStubWithFailingConfirmEquals";
            method.Type = typeof(TestManagerTest);

            Type t = typeof(TestManagerTest);
            var q = from m in t.GetMembers()
                    where m.Name == "FailingStubWithFailingConfirmEquals"
                    select m;

            method.MemberInfo = q.Single<MemberInfo>();

            TestResult res = manager.RunTest(method);
            Confirm.Equal("The expected value is: [1], but the actual value is: [2]", res.ResultText);
            Confirm.Equal(false, res.Passed);
        }

        [UnitTest]
        public void TestManager_RunTest_Will_Run_Failing_Test_When_Confirm_Different_Fails_Test()
        {
            TestManager manager = new TestManager();
            TestMethod method = new TestMethod();
            method.Name = "FailingStubWithFailingConfirmDifferent";
            method.Type = typeof(TestManagerTest);

            Type t = typeof(TestManagerTest);
            var q = from m in t.GetMembers()
                    where m.Name == "FailingStubWithFailingConfirmDifferent"
                    select m;

            method.MemberInfo = q.Single<MemberInfo>();

            TestResult res = manager.RunTest(method);
            Confirm.Equal("The the actual value should not be equal to Not Expected Value", res.ResultText);
            Confirm.Equal(false, res.Passed);
        }

        [UnitTest]
        public void TestManager_RunTest_Will_Run_Failing_Test_When_Confirm_IsGreater_Fails_Test()
        {
            TestManager manager = new TestManager();
            TestMethod method = new TestMethod();
            method.Name = "FailingStubWithFailingConfirmIsGreater";
            method.Type = typeof(TestManagerTest);

            Type t = typeof(TestManagerTest);
            var q = from m in t.GetMembers()
                    where m.Name == "FailingStubWithFailingConfirmIsGreater"
                    select m;

            method.MemberInfo = q.Single<MemberInfo>();

            TestResult res = manager.RunTest(method);
            Confirm.Equal("2.3 is not greater than 4.2", res.ResultText);
            Confirm.Equal(false, res.Passed);
        }

        [UnitTest]
        public void TestManager_RunTest_Will_Run_Test_Setup()
        {
            Confirm.IsTrue(testSetupRan);
        }

        public void FailingStubWithFailingConfirmIsGreater()
        {
            Confirm.IsGreater(2.3, 4.2);
        }

        public void FailingStubWithFailingConfirmDifferent()
        {
            Confirm.Different("Not Expected Value", "Not Expected Value");
        }
 
        public void FailingStubWithFailingConfirmEquals()
        {
            Confirm.Equal(1, 2);
        }
       
        public void FailingStubWithUnhandledException()
        {
            throw new Exception("Argument Invalid");            
        }

        public System.Reflection.MethodBase PassingStub()
        {
            return System.Reflection.MethodBase.GetCurrentMethod();
        }
    }
}
