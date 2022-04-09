
namespace HotKeySetter
{
    partial class EditForm
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
            this.groupBox_Param = new System.Windows.Forms.GroupBox();
            this.comboBox_Events = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label_KeyCodeChangeWarning = new System.Windows.Forms.Label();
            this.textBox_KeyCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_SecondKey = new System.Windows.Forms.ComboBox();
            this.textBox_Args = new System.Windows.Forms.TextBox();
            this.textBox_MinSpan = new System.Windows.Forms.TextBox();
            this.textBox_DelayTime = new System.Windows.Forms.TextBox();
            this.textBox_FirstKey = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.panel_OP = new System.Windows.Forms.Panel();
            this.groupBox_Param.SuspendLayout();
            this.panel_OP.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Param
            // 
            this.groupBox_Param.Controls.Add(this.comboBox_Events);
            this.groupBox_Param.Controls.Add(this.label7);
            this.groupBox_Param.Controls.Add(this.label_KeyCodeChangeWarning);
            this.groupBox_Param.Controls.Add(this.textBox_KeyCode);
            this.groupBox_Param.Controls.Add(this.label6);
            this.groupBox_Param.Controls.Add(this.comboBox_SecondKey);
            this.groupBox_Param.Controls.Add(this.textBox_Args);
            this.groupBox_Param.Controls.Add(this.textBox_MinSpan);
            this.groupBox_Param.Controls.Add(this.textBox_DelayTime);
            this.groupBox_Param.Controls.Add(this.textBox_FirstKey);
            this.groupBox_Param.Controls.Add(this.textBox_Name);
            this.groupBox_Param.Controls.Add(this.label9);
            this.groupBox_Param.Controls.Add(this.label5);
            this.groupBox_Param.Controls.Add(this.label4);
            this.groupBox_Param.Controls.Add(this.label3);
            this.groupBox_Param.Controls.Add(this.label2);
            this.groupBox_Param.Controls.Add(this.label1);
            this.groupBox_Param.Location = new System.Drawing.Point(4, 2);
            this.groupBox_Param.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_Param.Name = "groupBox_Param";
            this.groupBox_Param.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox_Param.Size = new System.Drawing.Size(459, 362);
            this.groupBox_Param.TabIndex = 0;
            this.groupBox_Param.TabStop = false;
            this.groupBox_Param.Text = "参数表";
            // 
            // comboBox_Events
            // 
            this.comboBox_Events.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Events.FormattingEnabled = true;
            this.comboBox_Events.Items.AddRange(new object[] {
            "(无)",
            "ALT",
            "CTRL",
            "SHIFT",
            "WIN"});
            this.comboBox_Events.Location = new System.Drawing.Point(119, 54);
            this.comboBox_Events.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_Events.Name = "comboBox_Events";
            this.comboBox_Events.Size = new System.Drawing.Size(325, 23);
            this.comboBox_Events.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "事      件";
            // 
            // label_KeyCodeChangeWarning
            // 
            this.label_KeyCodeChangeWarning.AutoSize = true;
            this.label_KeyCodeChangeWarning.ForeColor = System.Drawing.Color.Crimson;
            this.label_KeyCodeChangeWarning.Location = new System.Drawing.Point(17, 332);
            this.label_KeyCodeChangeWarning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_KeyCodeChangeWarning.Name = "label_KeyCodeChangeWarning";
            this.label_KeyCodeChangeWarning.Size = new System.Drawing.Size(352, 15);
            this.label_KeyCodeChangeWarning.TabIndex = 24;
            this.label_KeyCodeChangeWarning.Text = "更改键代码会使热键重新注册，这个过程可能失败。";
            this.label_KeyCodeChangeWarning.Visible = false;
            // 
            // textBox_KeyCode
            // 
            this.textBox_KeyCode.Location = new System.Drawing.Point(353, 248);
            this.textBox_KeyCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_KeyCode.Name = "textBox_KeyCode";
            this.textBox_KeyCode.Size = new System.Drawing.Size(75, 25);
            this.textBox_KeyCode.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(331, 252);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "=";
            // 
            // comboBox_SecondKey
            // 
            this.comboBox_SecondKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SecondKey.FormattingEnabled = true;
            this.comboBox_SecondKey.Items.AddRange(new object[] {
            "(无)",
            "ALT",
            "CTRL",
            "SHIFT",
            "WIN"});
            this.comboBox_SecondKey.Location = new System.Drawing.Point(119, 199);
            this.comboBox_SecondKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_SecondKey.Name = "comboBox_SecondKey";
            this.comboBox_SecondKey.Size = new System.Drawing.Size(325, 23);
            this.comboBox_SecondKey.TabIndex = 21;
            this.comboBox_SecondKey.SelectedIndexChanged += new System.EventHandler(this.comboBox_SecondKey_SelectedIndexChanged);
            // 
            // textBox_Args
            // 
            this.textBox_Args.Location = new System.Drawing.Point(119, 86);
            this.textBox_Args.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_Args.Multiline = true;
            this.textBox_Args.Name = "textBox_Args";
            this.textBox_Args.Size = new System.Drawing.Size(325, 104);
            this.textBox_Args.TabIndex = 20;
            // 
            // textBox_MinSpan
            // 
            this.textBox_MinSpan.Location = new System.Drawing.Point(343, 292);
            this.textBox_MinSpan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_MinSpan.Name = "textBox_MinSpan";
            this.textBox_MinSpan.Size = new System.Drawing.Size(101, 25);
            this.textBox_MinSpan.TabIndex = 19;
            // 
            // textBox_DelayTime
            // 
            this.textBox_DelayTime.Location = new System.Drawing.Point(119, 292);
            this.textBox_DelayTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_DelayTime.Name = "textBox_DelayTime";
            this.textBox_DelayTime.Size = new System.Drawing.Size(88, 25);
            this.textBox_DelayTime.TabIndex = 18;
            // 
            // textBox_FirstKey
            // 
            this.textBox_FirstKey.Location = new System.Drawing.Point(119, 248);
            this.textBox_FirstKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_FirstKey.Name = "textBox_FirstKey";
            this.textBox_FirstKey.ReadOnly = true;
            this.textBox_FirstKey.Size = new System.Drawing.Size(203, 25);
            this.textBox_FirstKey.TabIndex = 17;
            this.textBox_FirstKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_FirstKey_KeyDown);
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(119, 19);
            this.textBox_Name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(325, 25);
            this.textBox_Name.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 202);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "辅助按键";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "参      数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 298);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "延迟执行(ms)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 298);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "最小间隔(ms)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 252);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "主要按键";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "热  键  名";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(348, 4);
            this.button_OK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(100, 31);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(15, 4);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(100, 31);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // panel_OP
            // 
            this.panel_OP.Controls.Add(this.button_Cancel);
            this.panel_OP.Controls.Add(this.button_OK);
            this.panel_OP.Location = new System.Drawing.Point(4, 368);
            this.panel_OP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_OP.Name = "panel_OP";
            this.panel_OP.Size = new System.Drawing.Size(459, 38);
            this.panel_OP.TabIndex = 3;
            // 
            // EditForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(465, 405);
            this.Controls.Add(this.panel_OP);
            this.Controls.Add(this.groupBox_Param);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowIcon = false;
            this.Text = "编辑参数";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.Shown += new System.EventHandler(this.EditForm_Shown);
            this.groupBox_Param.ResumeLayout(false);
            this.groupBox_Param.PerformLayout();
            this.panel_OP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Param;
        private System.Windows.Forms.TextBox textBox_Args;
        private System.Windows.Forms.TextBox textBox_MinSpan;
        private System.Windows.Forms.TextBox textBox_DelayTime;
        private System.Windows.Forms.TextBox textBox_FirstKey;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.ComboBox comboBox_SecondKey;
        private System.Windows.Forms.TextBox textBox_KeyCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_KeyCodeChangeWarning;
        private System.Windows.Forms.ComboBox comboBox_Events;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel_OP;
    }
}