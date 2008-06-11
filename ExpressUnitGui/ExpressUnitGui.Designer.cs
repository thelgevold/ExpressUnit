namespace ExpressUnitGui
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.testClasses = new System.Windows.Forms.TreeView();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // testClasses
            // 
            this.testClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.testClasses.Location = new System.Drawing.Point(1, 0);
            this.testClasses.Name = "testClasses";
            this.testClasses.Size = new System.Drawing.Size(239, 571);
            this.testClasses.TabIndex = 0;
            this.testClasses.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TestClasses_BeforeExpand);
            this.testClasses.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TestClasses_NodeMouseClick);
            // 
            // resultPanel
            // 
            this.resultPanel.AutoScroll = true;
            this.resultPanel.Location = new System.Drawing.Point(246, 12);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(632, 548);
            this.resultPanel.TabIndex = 1;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 572);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.testClasses);
            this.Name = "mainForm";
            this.Text = "ExpressUnit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView testClasses;
        private System.Windows.Forms.Panel resultPanel;
    }
}

