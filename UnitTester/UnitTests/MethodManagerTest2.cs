using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnit;
using ExpressUnitModel;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class MethodManagerTest2
    {
        [TestCategory("Category1")]
        [UnitTest]
        public void MethodManagerTest2_Will_Find_Test_Of_Single_Category()
        {
            MethodManager manager = new MethodManager();

            IList<TestMethod> tests = manager.GetTestsInTestClass(typeof(MethodManagerTest2), TestType.UnitTest, "Category1");
            Confirm.IsTrue(tests.Count == 1);

            Confirm.Equal("MethodManagerTest2_Will_Find_Test_Of_Single_Category", tests[0].Name);
        }

        [TestCategory("Category2,Category3")]
        [UnitTest]
        public void MethodManagerTest2_Will_Find_Test_Of_More_Than_One_Category()
        {
            MethodManager manager = new MethodManager();

            IList<TestMethod> tests = manager.GetTestsInTestClass(typeof(MethodManagerTest2), TestType.UnitTest, "Category3");
            Confirm.IsTrue(tests.Count == 1);

            Confirm.Equal("MethodManagerTest2_Will_Find_Test_Of_More_Than_One_Category", tests[0].Name);

            tests = manager.GetTestsInTestClass(typeof(MethodManagerTest2), TestType.UnitTest, "Category2");
            Confirm.IsTrue(tests.Count == 1);

            Confirm.Equal("MethodManagerTest2_Will_Find_Test_Of_More_Than_One_Category", tests[0].Name);
        }
    }
}
