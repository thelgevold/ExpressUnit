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
using System.Xml.Linq;
using System.Collections;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class XmlManagerTest
    {
        [UnitTest]
        public void CreateTestReport_Will_Create_Test_Report_With_One_Failing_And_One_Passing_Test()
        {
            IList<TestResult> testResults = new List<TestResult>();

            TestResult res1 = new TestResult();
            res1.Passed = true;
            res1.TestName = "Passing Test";
            res1.Duration = DateTime.Now - DateTime.Now.AddSeconds(-1);

            TestResult res2 = new TestResult();
            res2.Passed = false;
            res2.TestName = "Failing Test";
            res2.ResultText = "Expected different result";
            res2.Duration = DateTime.Now - DateTime.Now.AddSeconds(-1);

            testResults.Add(res1);
            testResults.Add(res2);

            XDocument doc = XmlManager.CreateTestReport(testResults);

            // Two elements representing two tests
            var q = from e in doc.Elements("testsuite").Elements<XElement>("testcase") select e;
            Confirm.Equal(2, q.ToList<XElement>().Count);

            Confirm.Equal("Passing Test", q.ToList<XElement>()[0].Attribute("name").Value);
            Confirm.Equal("Failing Test", q.ToList<XElement>()[1].Attribute("name").Value);


            // One error element contained within the failing test element
            q = from e in q.ToList<XElement>()[1].Elements("failure") select e;
            Confirm.Equal(1, q.ToList<XElement>().Count);

            Confirm.Equal("2", doc.Element("testsuite").Attribute("tests").Value);
           // Confirm.Equals("1", doc.Element("testsuite").Attribute("errors").Value);
            Confirm.Equal("1", doc.Element("testsuite").Attribute("failures").Value);
            Confirm.Equal("0", doc.Element("testsuite").Attribute("skip").Value);

        }
    }
}
