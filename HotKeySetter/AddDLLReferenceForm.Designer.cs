
namespace HotKeySetter
{
    partial class AddDLLReferenceForm
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
            this.groupBox_Edit = new System.Windows.Forms.GroupBox();
            this.label_CopyTip = new System.Windows.Forms.Label();
            this.button_Validate = new System.Windows.Forms.Button();
            this.textBox_Entrypoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Browse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_DllPath = new System.Windows.Forms.TextBox();
            this.groupBox_DllInfo = new System.Windows.Forms.GroupBox();
            this.listView_DllInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_Add = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.helpStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel_Buttons = new System.Windows.Forms.Panel();
            this.panel_All = new System.Windows.Forms.Panel();
            this.groupBox_Edit.SuspendLayout();
            this.groupBox_DllInfo.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel_Buttons.SuspendLayout();
            this.panel_All.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Edit
            // 
            this.groupBox_Edit.Controls.Add(this.label_CopyTip);
            this.groupBox_Edit.Controls.Add(this.button_Validate);
            this.groupBox_Edit.Controls.Add(this.textBox_Entrypoint);
            this.groupBox_Edit.Controls.Add(this.label2);
            this.groupBox_Edit.Controls.Add(this.button_Browse);
            this.groupBox_Edit.Controls.Add(this.label1);
            this.groupBox_Edit.Controls.Add(this.textBox_DllPath);
            this.groupBox_Edit.Location = new System.Drawing.Point(17, 16);
            this.groupBox_Edit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_Edit.Name = "groupBox_Edit";
            this.groupBox_Edit.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_Edit.Size = new System.Drawing.Size(517, 132);
            this.groupBox_Edit.TabIndex = 0;
            this.groupBox_Edit.TabStop = false;
            this.groupBox_Edit.Text = "编辑";
            // 
            // label_CopyTip
            // 
            this.label_CopyTip.AutoSize = true;
            this.label_CopyTip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_CopyTip.Location = new System.Drawing.Point(29, 110);
            this.label_CopyTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CopyTip.Name = "label_CopyTip";
            this.label_CopyTip.Size = new System.Drawing.Size(454, 15);
            this.label_CopyTip.TabIndex = 6;
            this.label_CopyTip.Text = "被添加的动态链接库会被额外拷贝一份至AppData目录(点此查看)。";
            this.label_CopyTip.Click += new System.EventHandler(this.label_CopyTip_Click);
            // 
            // button_Validate
            // 
            this.button_Validate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Validate.Location = new System.Drawing.Point(445, 69);
            this.button_Validate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Validate.Name = "button_Validate";
            this.button_Validate.Size = new System.Drawing.Size(64, 29);
            this.button_Validate.TabIndex = 5;
            this.button_Validate.Text = "检验";
            this.button_Validate.UseVisualStyleBackColor = true;
            this.button_Validate.Click += new System.EventHandler(this.button_Validate_Click);
            this.button_Validate.Enter += new System.EventHandler(this.DescriptionControls_Activated);
            // 
            // textBox_Entrypoint
            // 
            this.textBox_Entrypoint.Location = new System.Drawing.Point(151, 70);
            this.textBox_Entrypoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_Entrypoint.Name = "textBox_Entrypoint";
            this.textBox_Entrypoint.Size = new System.Drawing.Size(285, 25);
            this.textBox_Entrypoint.TabIndex = 4;
            this.textBox_Entrypoint.Text = "EnumHotKeyEventSupport";
            this.textBox_Entrypoint.Enter += new System.EventHandler(this.DescriptionControls_Activated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "枚举函数入口点";
            this.label2.Enter += new System.EventHandler(this.DescriptionControls_Activated);
            // 
            // button_Browse
            // 
            this.button_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Browse.Location = new System.Drawing.Point(445, 20);
            this.button_Browse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(64, 29);
            this.button_Browse.TabIndex = 2;
            this.button_Browse.Text = "浏览";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            this.button_Browse.Enter += new System.EventHandler(this.PathControls_Activated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "路径";
            this.label1.Enter += new System.EventHandler(this.PathControls_Activated);
            // 
            // textBox_DllPath
            // 
            this.textBox_DllPath.Location = new System.Drawing.Point(77, 20);
            this.textBox_DllPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_DllPath.Name = "textBox_DllPath";
            this.textBox_DllPath.Size = new System.Drawing.Size(359, 25);
            this.textBox_DllPath.TabIndex = 0;
            this.textBox_DllPath.Enter += new System.EventHandler(this.PathControls_Activated);
            // 
            // groupBox_DllInfo
            // 
            this.groupBox_DllInfo.Controls.Add(this.listView_DllInfo);
            this.groupBox_DllInfo.Location = new System.Drawing.Point(17, 156);
            this.groupBox_DllInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_DllInfo.Name = "groupBox_DllInfo";
            this.groupBox_DllInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_DllInfo.Size = new System.Drawing.Size(517, 169);
            this.groupBox_DllInfo.TabIndex = 1;
            this.groupBox_DllInfo.TabStop = false;
            this.groupBox_DllInfo.Text = "动态链接库信息";
            // 
            // listView_DllInfo
            // 
            this.listView_DllInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView_DllInfo.FullRowSelect = true;
            this.listView_DllInfo.GridLines = true;
            this.listView_DllInfo.HideSelection = false;
            this.listView_DllInfo.Location = new System.Drawing.Point(9, 22);
            this.listView_DllInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView_DllInfo.Name = "listView_DllInfo";
            this.listView_DllInfo.Size = new System.Drawing.Size(499, 134);
            this.listView_DllInfo.TabIndex = 0;
            this.listView_DllInfo.UseCompatibleStateImageBehavior = false;
            this.listView_DllInfo.View = System.Windows.Forms.View.Details;
            this.listView_DllInfo.Enter += new System.EventHandler(this.DLLLibInfoControls_Activated);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "索引";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "描述函数";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "执行函数";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "错误提供函数";
            this.columnHeader4.Width = 100;
            // 
            // button_Add
            // 
            this.button_Add.Enabled = false;
            this.button_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Add.Location = new System.Drawing.Point(447, 9);
            this.button_Add.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(100, 29);
            this.button_Add.TabIndex = 2;
            this.button_Add.Text = "添加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cancel.Location = new System.Drawing.Point(29, 9);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(100, 29);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(558, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // helpStatusLabel
            // 
            this.helpStatusLabel.Name = "helpStatusLabel";
            this.helpStatusLabel.Size = new System.Drawing.Size(144, 20);
            this.helpStatusLabel.Text = "选择控件以获取帮助";
            // 
            // panel_Buttons
            // 
            this.panel_Buttons.Controls.Add(this.button_Cancel);
            this.panel_Buttons.Controls.Add(this.button_Add);
            this.panel_Buttons.Location = new System.Drawing.Point(-12, 333);
            this.panel_Buttons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_Buttons.Name = "panel_Buttons";
            this.panel_Buttons.Size = new System.Drawing.Size(611, 39);
            this.panel_Buttons.TabIndex = 5;
            // 
            // panel_All
            // 
            this.panel_All.Controls.Add(this.groupBox_Edit);
            this.panel_All.Controls.Add(this.panel_Buttons);
            this.panel_All.Controls.Add(this.groupBox_DllInfo);
            this.panel_All.Location = new System.Drawing.Point(0, 0);
            this.panel_All.Name = "panel_All";
            this.panel_All.Size = new System.Drawing.Size(558, 379);
            this.panel_All.TabIndex = 6;
            // 
            // AddDLLReferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(558, 408);
            this.Controls.Add(this.panel_All);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDLLReferenceForm";
            this.ShowIcon = false;
            this.Text = "添加外部动态链接库引用";
            this.Load += new System.EventHandler(this.AddDLLReferenceForm_Load);
            this.Shown += new System.EventHandler(this.AddDLLReferenceForm_Shown);
            this.groupBox_Edit.ResumeLayout(false);
            this.groupBox_Edit.PerformLayout();
            this.groupBox_DllInfo.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel_Buttons.ResumeLayout(false);
            this.panel_All.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Edit;
        private System.Windows.Forms.Button button_Validate;
        private System.Windows.Forms.TextBox textBox_Entrypoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_DllPath;
        private System.Windows.Forms.GroupBox groupBox_DllInfo;
        private System.Windows.Forms.ListView listView_DllInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel helpStatusLabel;
        private System.Windows.Forms.Label label_CopyTip;
        private System.Windows.Forms.Panel panel_Buttons;
        private System.Windows.Forms.Panel panel_All;
    }
}