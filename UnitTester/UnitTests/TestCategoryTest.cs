using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnitModel;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class TestCategoryTest
    {
        [UnitTest]
        public void TestCategoryTest_Will_Parse_List_Of_Attributes()
        {
            TestCategory category = new TestCategory("Cat1,Cat2");
            Confirm.IsTrue(category.Categories.Count == 2);

            Confirm.Equal("Cat1", category.Categories[0]);
            Confirm.Equal("Cat2", category.Categories[1]);
        }

        [UnitTest]
        public void TestCategoryTest_Will_Ignore_Invalid_Input()
        {
            TestCategory category = new TestCategory(null);
            Confirm.IsNull(category.Categories);

            category = new TestCategory(string.Empty);
            Confirm.IsNull(category.Categories);
        }
    }
}
