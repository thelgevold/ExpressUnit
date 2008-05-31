using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnit;

namespace UnitTester.Tests
{
    [TestClass]
    public class SampleTests
    {
        [UnitTest]
        public void AddTest()
        {
            Confirm.Equals(5, 5+3);
        }

        [UnitTest]
        public void AddTest2()
        {
            Confirm.Equals(3, 1 + 2);
        }
    }
}
