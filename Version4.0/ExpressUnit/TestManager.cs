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
using System.IO;
using System.Collections;
using ExpressUnitModel;

namespace ExpressUnit
{
    public class TestManager : ITestManager
    {
        private MethodManager methodManager = new MethodManager();
        public TestResult RunTest(TestMethod testMethod)
        {
            TestResult result;
            try
            {
                var instance = Activator.CreateInstance(testMethod.Type);
                if (testMethod.TestSetup != null)
                {
                    testMethod.Type.InvokeMember(testMethod.TestSetup.Name, BindingFlags.InvokeMethod, null, instance, null);
                }

                DateTime start = DateTime.Now;
                testMethod.Type.InvokeMember(testMethod.Name, BindingFlags.InvokeMethod, null, instance, null);
                TimeSpan timeSpan = DateTime.Now - start;
                result = EnsureCorrectTestResult(testMethod.MemberInfo);
                result.Duration = timeSpan;

                if (testMethod.TestTearDown != null)
                {
                    testMethod.Type.InvokeMember(testMethod.TestTearDown.Name, BindingFlags.InvokeMethod, null, instance, null);
                }
            }
            catch (System.Exception ex)
            {
                result = HandleFailedTest(testMethod.MemberInfo, ex);
            }
            result.TestName = testMethod.Name;
            result.UseCase = testMethod.UseCase;
            return result;
        }

        private TestResult HandleFailedTest(MemberInfo m, Exception ex)
        {
            TestResult result = new TestResult();
            if (AttributeManager.ExceptionIsExcepted(m, ex))
            {
                result.Passed = true;
                result.ResultText = GetTestPassedMessage(m.Name);
                return result;
            }

            if (ex.InnerException != null)
            {
                result.Passed = false;
                UnitTestFailedException failEx = ex.InnerException as UnitTestFailedException;

                if (failEx != null)
                {
                    result.ResultText = failEx.Message;
                    result.Exception = failEx;
                }
                else
                {
                    result.ResultText = ex.InnerException.Message;
                    result.Exception = ex.InnerException;
                }
            }
            else
            {
                result.ResultText = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        private TestResult EnsureCorrectTestResult(MemberInfo method)
        {
            TestResult result = new TestResult();

            object[] attribute = method.GetCustomAttributes(typeof(ExceptionThrown), true);

            if (attribute.Length > 0)
            {
                ExceptionThrown expectedException = attribute[0] as ExceptionThrown;
                result.Passed = false;
                result.ResultText = string.Format("Exception of type {0} was expected", expectedException.ExceptionType);
                return result;
            }

            result.Passed = true;
            result.ResultText = GetTestPassedMessage(method.Name);
            return result;
        }

        private string GetTestPassedMessage(string testName)
        {
            return string.Format("{0} {1}", testName, "(PASSED)");
        }

        public IList<TestFixture> GetTests(string testType)
        {
            List<Type> allTypes = new List<Type>();

            Assembly currentAssembly = Assembly.GetEntryAssembly();
            Type[] types = currentAssembly.GetTypes();

            var q1 = from t in types
                     where Attribute.IsDefined(t, typeof(TestClass)) == true orderby t.Name
                     select t;

            allTypes.AddRange(q1.ToArray());

            string[] dlls = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");

            foreach (string name in dlls)
            {
                currentAssembly = Assembly.LoadFile(name);
                types = currentAssembly.GetTypes();

                var q2 = from t in types
                         where Attribute.IsDefined(t, typeof(TestClass)) == true
                         select t;

                allTypes.AddRange(q2.ToArray());
            }

            var sortedTypes = from t in allTypes orderby t.Name ascending select t; 
          
            IList<TestFixture> allTests = new List<TestFixture>();
            foreach (Type t in sortedTypes.ToList<Type>())
            {
                TestFixture test = new TestFixture();
                test.Name = t.Name;
                test.Tests = methodManager.GetTestsInTestClass(t, testType);

                if (test.Tests.Count > 0)
                {
                    allTests.Add(test);
                }
            }
            return allTests;

        }

    }
}
