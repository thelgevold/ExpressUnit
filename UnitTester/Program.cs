using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using ExpressUnit;
using System.Windows.Forms;
namespace ExpressUnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
           UnitTestManager unitTestManager = new UnitTestManager();
           unitTestManager.ExecuteAllUnitTests(new DisplayTestResults(PrintResults));

           Console.ReadKey();
        }

        

        private static void PrintResults(IList<TestResult> results)
        {
            var q = from r in results
                    where r.Passed == true
                    select r;

            int passed = 0;
            foreach (TestResult testResult in results)
            {
                if (testResult.Passed)
                {
                    PrintPassMessage(testResult.TestName);
                    passed++;
                }
                else
                {
                    PrintTestFailedMessage(testResult.TestName, testResult.Exception);
                }
            }

            

            Console.WriteLine();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("Result {0}/{1} Passed", passed, results.Count));
        }

        private static void PrintPassMessage(string testName)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("***********************************************************");
            Console.WriteLine(string.Format("Test: {0} Passed", testName));
            Console.WriteLine("***********************************************************");
            Console.WriteLine();
        }

        private static void PrintTestFailedMessage(string testName, Exception ex)
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
    }
}
