using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.IO;
using ExpressUnitModel;

namespace ExpressUnit
{
    public class UnitTestManager
    {
        public UnitTestManager()
        {
           
        }

        public void ExecuteAllUnitTests(DisplayTestResults displayTestResults)
        {
            displayTestResults(RunTests());
        }

        public List<TestResult> RunTests()
        {
            List<TestResult> results = new List<TestResult>();
            Type[] types = GetUnitTestTypes();

            foreach (Type t in types)
            {
                results.AddRange(RunTests(t));
            }

            return results;
        }

        public TestResult RunTest(Type type,MemberInfo m)
        {
            TestResult result = new TestResult();
            try
            {
                result.TestName = m.Name;
                type.InvokeMember(m.Name, BindingFlags.InvokeMethod, null, Activator.CreateInstance(type), null);
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

            return result;
        }

        public List<TestResult> RunTests(Type t)
        {
            List<TestResult> results = new List<TestResult>();
            
            foreach (MemberInfo m in GetUnitTests(t))
            {
                TestResult result = new TestResult();
                result.TestName = m.Name;
                try
                {
                    t.InvokeMember(m.Name, BindingFlags.InvokeMethod, null, Activator.CreateInstance(t), null);
                    result.Passed = true;
                    result.ResultText = string.Format("{0} {1}", m.Name, "(PASSED)");
                }
                catch (UnitTestFailedException assertEx)
                {
                    result.Passed = false;
                    result.ResultText = string.Format("{0} {1} {2}", m.Name, "(FAILED)", assertEx.Message);
                }
                catch (System.Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        UnitTestFailedException innerEx = ex.InnerException as UnitTestFailedException;

                        result.Passed = false;
                        result.ResultText = innerEx.Message;
                        result.ResultText = string.Format("{0} {1} {2}", m.Name, "(FAILED)", innerEx.Message);
                    }
                    else
                    {
                        result.ResultText = string.Format("{0} {1} {2}", m.Name, "(FAILED)", ex.Message);
                    }
                }

                results.Add(result);
            }

            return results;
            
        }

        public MemberInfo[] GetUnitTests(Type t)
        {
            var q = from m in t.GetMembers()
                    where Attribute.IsDefined(m, typeof(UnitTest)) == true && Attribute.IsDefined(m, typeof(Ignore)) == false
                    select m;

            return q.ToArray<MemberInfo>();
        }
       
        public Type[] GetUnitTestTypes()
        {
            List<Type> allTypes = new List<Type>();

            string[] dlls = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");

            foreach(string name in dlls)
            {
                Assembly currentAssembly = Assembly.LoadFile(name);
                Type[] types = currentAssembly.GetTypes(); 

                var q = from t in types
                        where Attribute.IsDefined(t, typeof(TestClass)) == true
                        select t;

                allTypes.AddRange(q.ToArray());
            }
            return allTypes.ToArray<Type>();

        }

    }
}
