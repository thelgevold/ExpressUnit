using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using ExpressUnitModel;

namespace ExpressUnit
{
    public class GuiHelper
    {
        private UnitTestManager manager = new UnitTestManager();
        public void PopulateTestTree(TreeNode treeNode)
        {
            Type[] types = GetTestTypes();
            foreach (Type t in types)
            {
                TreeNode node = new TreeNode();
                node.Tag = t;
                node.Text = t.Name;
                node.Nodes.Add("dummy");
                treeNode.Nodes.Add(node);
            }
        }

        public List<TestResult> RunTests(TreeNode node, Panel resultsPanel)
        {
            List<TestResult> results = new List<TestResult>();
            resultsPanel.Controls.Clear();
            int x = 0;
            int y = 0;
            foreach (TestResult res in GetTestResults(node))
            {
                Label lbl = new Label();
                lbl.Width = 600;
                lbl.Location = new Point(x, y);
                lbl.Text = res.ResultText;
               
                if (res.Passed == false)
                {
                    lbl.ForeColor = Color.Red;
                }
                else
                {
                    lbl.ForeColor = Color.Green;
                }

                resultsPanel.Controls.Add(lbl);
                y += lbl.Height + 10;
            }

            return results;
        }

        protected virtual List<TestResult> GetTestResults(TreeNode node)
        {
            List<TestResult> results = new List<TestResult>();
            
            // top node
            if (node.Parent == null)
            {
                results = manager.RunTests();
            }
            // test class
            else if (node.Parent.Parent == null)
            {
                results = manager.RunTests(node.Tag as Type);
            }
            
            // test method
            else
            {
                results.Add(manager.RunTest(node.Parent.Tag as Type,node.Tag as MemberInfo));
            }
            return results;
        }

        public void PopulateTestNode(TreeNode treeNode)
        {
            if (treeNode.Tag == null)
            {
                return;
            }

            treeNode.Nodes.Clear();
            MemberInfo[] members = GetTestMethods(treeNode.Tag as Type);
            foreach (MemberInfo m in members)
            {
                TreeNode node = new TreeNode();
                node.Tag = m;
                node.Text = m.Name;
                treeNode.Nodes.Add(node);
            }
        }

        protected virtual MemberInfo[] GetTestMethods(Type t)
        {
            return manager.GetUnitTests(t);
        }

        protected virtual Type[] GetTestTypes()
        {
            return manager.GetUnitTestTypes();
        }

    }
}
