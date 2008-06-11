using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExpressUnit;

namespace ExpressUnitGui
{
    public partial class mainForm : Form
    {
        private GuiHelper helper = new GuiHelper();
        private ContextMenuStrip contextMenu;
        public mainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("RUN");
            contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(ContextMenu_ItemClicked);

            testClasses.Nodes.Add("All Tests");
            helper.PopulateTestTree(testClasses.Nodes[0]);
        }

        protected void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            helper.RunTests(testClasses.SelectedNode, resultPanel);
        }

        protected void TestClasses_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            helper.PopulateTestNode(e.Node);
        }

        private void TestClasses_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(testClasses, e.Location);
            }
        }

    }
}
