using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnitModel;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class DemoTest
    {
        [TestCategory("Category1")]
        [UnitTest]
        public void Demo1()
        {
            
        }

        [TestCategory("Category2")]
        [UnitTest]
        public void Demo2()
        {

        }

        [TestCategory("Category1,Category2")]
        [UnitTest]
        public void Demo3()
        {

        }
    }
}
