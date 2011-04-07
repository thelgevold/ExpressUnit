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
using System.Xml.Linq;
using System.Collections;
using System.IO;

namespace ExpressUnit
{
    public static class XmlManager
    {
        public static XDocument CreateTestReport(IList<TestResult> testResults)
        {
            XDocument doc = new XDocument();

            var failures = testResults.Where(t => t.Passed == false).ToList();
            var resultsByFixture = testResults.GroupBy(t => t.Fixture).ToDictionary(t => t.Key, t => t.ToList());
            var totalExecutionTime = new TimeSpan(testResults.Sum(t => t.Duration.Ticks));

            int failureCount = failures.ToArray<TestResult>().Length;

            XElement testResultsRoot = new XElement("test-results",
                                                    new XAttribute("name", "Tests"),
                                                    new XAttribute("total", testResults.Count),
                                                    new XAttribute("errors", 0), //not supported
                                                    new XAttribute("failures", failureCount),
                                                    new XAttribute("not-run", 0), //not supported
                                                    new XAttribute("ignored", 0));  //not supported




            doc.Add(testResultsRoot);

            XElement testSuite = new XElement("test-suite");
            testResultsRoot.Add(testSuite);

            testSuite.Add(new XAttribute("name", "Tests"));
            testSuite.Add(new XAttribute("executed", "True"));
            testSuite.Add(new XAttribute("success", (failureCount == 0).ToBoolString()));
            testSuite.Add(new XAttribute("time", totalExecutionTime.TotalSeconds));
            testSuite.Add(new XAttribute("asserts", 0)); //not supported

            XElement topResults = new XElement("results");
            testSuite.Add(topResults);

            // NUnit has this second level of test suite nodes, but it appears that CruiseControl does not need this
            // This may be needed in the future.

           /* XElement secondTestSuite = new XElement("test-suite",
                                                    new XAttribute("name", "ExpressUnitTests"),
                                                    new XAttribute("executed", "True"),
                                                    new XAttribute("success", failureCount == 0),
                                                    new XAttribute("time", totalExecutionTime.TotalSeconds),
                                                    new XAttribute("asserts", 0));
            topResults.Add(secondTestSuite);

            XElement secondResults = new XElement("results");
            secondTestSuite.Add(secondResults);*/

            foreach (string fixture in resultsByFixture.Keys)
            {
                List<TestResult> testsInFixture = resultsByFixture[fixture];
                TimeSpan totalTimeForFixture = new TimeSpan(testsInFixture.Sum(t => t.Duration.Ticks));
                bool success = testsInFixture.Any(t => t.Passed == false) == false;

                XElement fixtureTestSuite = new XElement("test-suite",
                                                         new XAttribute("name", fixture),
                                                         new XAttribute("executed", "True"),
                                                         new XAttribute("success", success.ToBoolString()),
                                                         new XAttribute("time", totalTimeForFixture.TotalSeconds),
                                                         new XAttribute("asserts", 0));

                topResults.Add(fixtureTestSuite);

                XElement fixtureResults = new XElement("results");
                fixtureTestSuite.Add(fixtureResults);


                foreach (TestResult res in testsInFixture)
                {
                    XElement test = new XElement("test-case",
                                    new XAttribute("name", res.TestName),
                                    new XAttribute("executed", "True"),
                                    new XAttribute("success", res.Passed.ToBoolString()),
                                    new XAttribute("time", res.Duration.TotalSeconds),
                                    new XAttribute("asserts", 0));

                    if (res.Passed == false)
                    {
                        XElement error = new XElement("failure",
                                                      new XElement("message", new XCData(res.ResultText)),
                                                      new XElement("stack-trace", new XCData(res.StackTrace)));
                        test.Add(error);
                    }

                    fixtureResults.Add(test);
                }
            }
            return doc;
        }

        private static string ToBoolString(this bool value)
        {
            if (value == true)
            {
                return Boolean.TrueString;
            }

            return Boolean.FalseString;
        }
    }
}
