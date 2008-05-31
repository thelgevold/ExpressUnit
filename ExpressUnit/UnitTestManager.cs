﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace ExpressUnit
{
    public class UnitTestManager
    {
        public UnitTestManager()
        {
           
        }

        public void ExecuteAllUnitTests(DisplayTestResults displayTestResults)
        {
            List<TestResult> results = new List<TestResult>();
            Type[] types = GetUnitTestTypes();

            foreach(Type t in types)
            {
                results.AddRange(RunTests(t));    
            }

            displayTestResults(results);
        }

        private List<TestResult> RunTests(Type t)
        {
            List<TestResult> results = new List<TestResult>();
            
            foreach (MemberInfo m in GetUnitTest(t))
            {
                TestResult result = new TestResult();
                result.TestName = m.Name;
                try
                {
                    t.InvokeMember(m.Name, BindingFlags.InvokeMethod, null, Activator.CreateInstance(t), null);
                    result.Passed = true;
                }
                catch (UnitTestFailedException assertEx)
                {
                    result.Passed = false;
                    result.Exception = assertEx;
                }
                catch (System.Exception ex)
                {
                    result.Passed = false;
                    result.Exception = ex;
                }

                results.Add(result);
            }

            return results;
            
        }

        private void PrintPassMessage(string testName)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("***********************************************************");
            Console.WriteLine(string.Format("Test: {0} Passed",testName));
            Console.WriteLine("***********************************************************");
            Console.WriteLine();
        }

        private void PrintTestFailedMessage(string testName,Exception ex)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("***********************************************************");
            Console.WriteLine(string.Format("Test: {0} Failed", testName));
            if (ex.InnerException != null)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            else
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("***********************************************************");
            Console.WriteLine();
        }

        private MemberInfo[] GetUnitTest(Type t)
        {
            var q = from m in t.GetMembers()
                    where Attribute.IsDefined(m, typeof(UnitTest)) == true && Attribute.IsDefined(m, typeof(Ignore)) == false
                    select m;

            return q.ToArray<MemberInfo>();
        }
       
        private Type[] GetUnitTestTypes()
        {
            Assembly a = Assembly.GetEntryAssembly();
            Type[] types = a.GetTypes(); 

            var q = from t in types
                    where Attribute.IsDefined(t, typeof(TestClass)) == true
                    select t;
 
            return q.ToArray();

        }

    }
}
