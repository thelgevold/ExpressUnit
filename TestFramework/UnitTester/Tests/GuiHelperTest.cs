using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressUnit;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using ExpressUnitModel;


namespace ExpressUnitTests
{
    [TestClass]
    public class GuiHelperTest : GuiHelper
    {
        [UnitTest]
        public void RunTestsTest()
        {
            GuiHelper helper = new GuiHelperTest();
            Panel panel = new Panel();
            TreeNode treeNode = new TreeNode();

            helper.RunTests(treeNode, panel);

            Confirm.Equals(Color.Green, panel.Controls[0].ForeColor);
            Confirm.Equals(Color.Red, panel.Controls[1].ForeColor);
        }

        protected override List<TestResult> GetTestResults(TreeNode node)
        {
            List<TestResult> res = new List<TestResult>();

            TestResult r1 = new TestResult();
            r1.Passed = true;
            res.Add(r1);

            r1 = new TestResult();
            r1.Passed = false;
            r1.Exception = new Exception("Failed");
            res.Add(r1);

            return res;
        }

        [UnitTest]
        public void PopulateTestTreeTest()
        {
            TreeView tree = new TreeView();
            tree.Nodes.Add("");
            GuiHelper helper = new GuiHelperTest();
            helper.PopulateTestTree(tree.Nodes[0]);

            Confirm.Equals(3, tree.Nodes[0].Nodes.Count);
            Confirm.Equals(tree.Nodes[0].Nodes[0].Text, "GuiHelperTest");
            Confirm.Equals(tree.Nodes[0].Nodes[0].Tag, typeof(ExpressUnitTests.GuiHelperTest));
        }

        [UnitTest]
        public void PopulateTestNodeTest()
        {
            GuiHelper helper = new GuiHelperTest();
            TreeNode node = new TreeNode();
            node.Tag = typeof(GuiHelperTest);
            helper.PopulateTestNode(node);

            Confirm.Equals(1, node.Nodes.Count);
            Confirm.Equals("PopulateTestNodeTest", node.Nodes[0].Text);
        }

        protected override MemberInfo[] GetTestMethods(Type t)
        {
            MemberInfo member = typeof(GuiHelperTest).GetMember("PopulateTestNodeTest")[0];
           
            MemberInfo[] array = new MemberInfo[1];
            array[0] = member;

            return array;
        }

        protected override Type[] GetTestTypes()
        {
            Type[] type = new Type[3];
            type[0] = typeof(GuiHelperTest);
            type[1] = typeof(GuiHelperTest);
            type[2] = typeof(GuiHelperTest);

            return type;
        }
    }
}
