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
    public class XmlManager
    {
        public static XDocument CreateTestReport(IList<TestResult> testResults)
        {
            XDocument doc = new XDocument();

            var failures = from t in testResults where t.Passed == false select t;

            int failureCount = failures.ToArray<TestResult>().Length;

            XElement testSuite = new XElement("testsuite");

            testSuite.Add(new XAttribute("name", "ExpressUnitTests"));
            testSuite.Add(new XAttribute("tests", testResults.Count));
            testSuite.Add(new XAttribute("failures", failureCount));
            testSuite.Add(new XAttribute("skip", 0));

            doc.Add(testSuite);

            foreach (TestResult res in testResults)
            {
                XElement test = new XElement("testcase");
                test.Add(new XAttribute("name", res.TestName));
                test.Add(new XAttribute("time", res.Duration));

                if (res.Passed == false)
                {
                    XElement error = new XElement("failure");
                    error.Value = res.StackTrace;
                    error.Add(new XAttribute("message",res.ResultText));

                    test.Add(error);
                }
                
                testSuite.Add(test);
            }
            return doc;
        }
    }
}
