
namespace HotKeySetter
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.groupBox_techdeclare = new System.Windows.Forms.GroupBox();
            this.label_techdeclare = new System.Windows.Forms.Label();
            this.panel_all = new System.Windows.Forms.Panel();
            this.label_developer = new System.Windows.Forms.Label();
            this.groupBox_tech = new System.Windows.Forms.GroupBox();
            this.label_techdec = new System.Windows.Forms.Label();
            this.textBox_lang = new System.Windows.Forms.TextBox();
            this.textBox_adminstatus = new System.Windows.Forms.TextBox();
            this.textBox_currentproc = new System.Windows.Forms.TextBox();
            this.label_adminstatus = new System.Windows.Forms.Label();
            this.label_lang = new System.Windows.Forms.Label();
            this.label_currentproc = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.groupBox_program = new System.Windows.Forms.GroupBox();
            this.textBox_dotnetCLRver = new System.Windows.Forms.TextBox();
            this.label_dotnetCLRver = new System.Windows.Forms.Label();
            this.textBox_filever = new System.Windows.Forms.TextBox();
            this.label_filever = new System.Windows.Forms.Label();
            this.textBox_progver = new System.Windows.Forms.TextBox();
            this.label_progver = new System.Windows.Forms.Label();
            this.label_programname = new System.Windows.Forms.Label();
            this.label_DebugInfo = new System.Windows.Forms.Label();
            this.groupBox_techdeclare.SuspendLayout();
            this.panel_all.SuspendLayout();
            this.groupBox_tech.SuspendLayout();
            this.groupBox_program.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_techdeclare
            // 
            this.groupBox_techdeclare.Controls.Add(this.label_techdeclare);
            this.groupBox_techdeclare.Location = new System.Drawing.Point(273, 10);
            this.groupBox_techdeclare.Name = "groupBox_techdeclare";
            this.groupBox_techdeclare.Size = new System.Drawing.Size(482, 290);
            this.groupBox_techdeclare.TabIndex = 5;
            this.groupBox_techdeclare.TabStop = false;
            this.groupBox_techdeclare.Text = "技术声明";
            // 
            // label_techdeclare
            // 
            this.label_techdeclare.Font = new System.Drawing.Font("宋体", 9F);
            this.label_techdeclare.Location = new System.Drawing.Point(6, 21);
            this.label_techdeclare.Name = "label_techdeclare";
            this.label_techdeclare.Size = new System.Drawing.Size(470, 257);
            this.label_techdeclare.TabIndex = 0;
            this.label_techdeclare.Text = resources.GetString("label_techdeclare.Text");
            // 
            // panel_all
            // 
            this.panel_all.Controls.Add(this.label_DebugInfo);
            this.panel_all.Controls.Add(this.label_developer);
            this.panel_all.Controls.Add(this.groupBox_tech);
            this.panel_all.Controls.Add(this.button_OK);
            this.panel_all.Controls.Add(this.groupBox_program);
            this.panel_all.Controls.Add(this.label_programname);
            this.panel_all.Location = new System.Drawing.Point(1, 2);
            this.panel_all.Name = "panel_all";
            this.panel_all.Size = new System.Drawing.Size(261, 305);
            this.panel_all.TabIndex = 6;
            // 
            // label_developer
            // 
            this.label_developer.AutoSize = true;
            this.label_developer.Location = new System.Drawing.Point(152, 40);
            this.label_developer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_developer.Name = "label_developer";
            this.label_developer.Size = new System.Drawing.Size(95, 12);
            this.label_developer.TabIndex = 9;
            this.label_developer.Text = "- By LG3202 JCY";
            // 
            // groupBox_tech
            // 
            this.groupBox_tech.Controls.Add(this.label_techdec);
            this.groupBox_tech.Controls.Add(this.textBox_lang);
            this.groupBox_tech.Controls.Add(this.textBox_adminstatus);
            this.groupBox_tech.Controls.Add(this.textBox_currentproc);
            this.groupBox_tech.Controls.Add(this.label_adminstatus);
            this.groupBox_tech.Controls.Add(this.label_lang);
            this.groupBox_tech.Controls.Add(this.label_currentproc);
            this.groupBox_tech.Location = new System.Drawing.Point(6, 165);
            this.groupBox_tech.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_tech.Name = "groupBox_tech";
            this.groupBox_tech.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_tech.Size = new System.Drawing.Size(250, 104);
            this.groupBox_tech.TabIndex = 8;
            this.groupBox_tech.TabStop = false;
            this.groupBox_tech.Text = "技术信息";
            // 
            // label_techdec
            // 
            this.label_techdec.AutoSize = true;
            this.label_techdec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_techdec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_techdec.Location = new System.Drawing.Point(195, 22);
            this.label_techdec.Name = "label_techdec";
            this.label_techdec.Size = new System.Drawing.Size(53, 12);
            this.label_techdec.TabIndex = 7;
            this.label_techdec.Text = "技术声明";
            this.label_techdec.Click += new System.EventHandler(this.label_techdec_Click);
            // 
            // textBox_lang
            // 
            this.textBox_lang.Location = new System.Drawing.Point(71, 19);
            this.textBox_lang.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_lang.Name = "textBox_lang";
            this.textBox_lang.ReadOnly = true;
            this.textBox_lang.Size = new System.Drawing.Size(50, 21);
            this.textBox_lang.TabIndex = 4;
            this.textBox_lang.Text = "C#/C++";
            // 
            // textBox_adminstatus
            // 
            this.textBox_adminstatus.Location = new System.Drawing.Point(71, 74);
            this.textBox_adminstatus.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_adminstatus.Name = "textBox_adminstatus";
            this.textBox_adminstatus.ReadOnly = true;
            this.textBox_adminstatus.Size = new System.Drawing.Size(174, 21);
            this.textBox_adminstatus.TabIndex = 5;
            // 
            // textBox_currentproc
            // 
            this.textBox_currentproc.Location = new System.Drawing.Point(71, 46);
            this.textBox_currentproc.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_currentproc.Name = "textBox_currentproc";
            this.textBox_currentproc.ReadOnly = true;
            this.textBox_currentproc.Size = new System.Drawing.Size(174, 21);
            this.textBox_currentproc.TabIndex = 6;
            // 
            // label_adminstatus
            // 
            this.label_adminstatus.AutoSize = true;
            this.label_adminstatus.Location = new System.Drawing.Point(8, 76);
            this.label_adminstatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_adminstatus.Name = "label_adminstatus";
            this.label_adminstatus.Size = new System.Drawing.Size(53, 12);
            this.label_adminstatus.TabIndex = 2;
            this.label_adminstatus.Text = "管 理 员";
            // 
            // label_lang
            // 
            this.label_lang.AutoSize = true;
            this.label_lang.Location = new System.Drawing.Point(8, 23);
            this.label_lang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_lang.Name = "label_lang";
            this.label_lang.Size = new System.Drawing.Size(47, 12);
            this.label_lang.TabIndex = 1;
            this.label_lang.Text = "语   言";
            // 
            // label_currentproc
            // 
            this.label_currentproc.AutoSize = true;
            this.label_currentproc.Location = new System.Drawing.Point(8, 49);
            this.label_currentproc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_currentproc.Name = "label_currentproc";
            this.label_currentproc.Size = new System.Drawing.Size(53, 12);
            this.label_currentproc.TabIndex = 0;
            this.label_currentproc.Text = "当前进程";
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_OK.Location = new System.Drawing.Point(6, 273);
            this.button_OK.Margin = new System.Windows.Forms.Padding(2);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(250, 25);
            this.button_OK.TabIndex = 7;
            this.button_OK.Text = "确 定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // groupBox_program
            // 
            this.groupBox_program.Controls.Add(this.textBox_dotnetCLRver);
            this.groupBox_program.Controls.Add(this.label_dotnetCLRver);
            this.groupBox_program.Controls.Add(this.textBox_filever);
            this.groupBox_program.Controls.Add(this.label_filever);
            this.groupBox_program.Controls.Add(this.textBox_progver);
            this.groupBox_program.Controls.Add(this.label_progver);
            this.groupBox_program.Location = new System.Drawing.Point(6, 53);
            this.groupBox_program.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_program.Name = "groupBox_program";
            this.groupBox_program.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_program.Size = new System.Drawing.Size(250, 108);
            this.groupBox_program.TabIndex = 6;
            this.groupBox_program.TabStop = false;
            this.groupBox_program.Text = "程序集信息";
            // 
            // textBox_dotnetCLRver
            // 
            this.textBox_dotnetCLRver.Location = new System.Drawing.Point(71, 78);
            this.textBox_dotnetCLRver.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_dotnetCLRver.Name = "textBox_dotnetCLRver";
            this.textBox_dotnetCLRver.ReadOnly = true;
            this.textBox_dotnetCLRver.Size = new System.Drawing.Size(169, 21);
            this.textBox_dotnetCLRver.TabIndex = 5;
            // 
            // label_dotnetCLRver
            // 
            this.label_dotnetCLRver.AutoSize = true;
            this.label_dotnetCLRver.Location = new System.Drawing.Point(7, 82);
            this.label_dotnetCLRver.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_dotnetCLRver.Name = "label_dotnetCLRver";
            this.label_dotnetCLRver.Size = new System.Drawing.Size(53, 12);
            this.label_dotnetCLRver.TabIndex = 4;
            this.label_dotnetCLRver.Text = ".NET CLR";
            // 
            // textBox_filever
            // 
            this.textBox_filever.Location = new System.Drawing.Point(71, 48);
            this.textBox_filever.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_filever.Name = "textBox_filever";
            this.textBox_filever.ReadOnly = true;
            this.textBox_filever.Size = new System.Drawing.Size(169, 21);
            this.textBox_filever.TabIndex = 3;
            // 
            // label_filever
            // 
            this.label_filever.AutoSize = true;
            this.label_filever.Location = new System.Drawing.Point(9, 53);
            this.label_filever.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_filever.Name = "label_filever";
            this.label_filever.Size = new System.Drawing.Size(53, 12);
            this.label_filever.TabIndex = 2;
            this.label_filever.Text = "文件版本";
            // 
            // textBox_progver
            // 
            this.textBox_progver.Location = new System.Drawing.Point(71, 18);
            this.textBox_progver.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_progver.Name = "textBox_progver";
            this.textBox_progver.ReadOnly = true;
            this.textBox_progver.Size = new System.Drawing.Size(169, 21);
            this.textBox_progver.TabIndex = 1;
            // 
            // label_progver
            // 
            this.label_progver.AutoSize = true;
            this.label_progver.Location = new System.Drawing.Point(8, 22);
            this.label_progver.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_progver.Name = "label_progver";
            this.label_progver.Size = new System.Drawing.Size(65, 12);
            this.label_progver.TabIndex = 0;
            this.label_progver.Text = "程序集版本";
            // 
            // label_programname
            // 
            this.label_programname.AutoSize = true;
            this.label_programname.Font = new System.Drawing.Font("Segoe Script", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_programname.Location = new System.Drawing.Point(49, 9);
            this.label_programname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_programname.Name = "label_programname";
            this.label_programname.Size = new System.Drawing.Size(155, 33);
            this.label_programname.TabIndex = 5;
            this.label_programname.Text = "HotKeySetter";
            // 
            // label_DebugInfo
            // 
            this.label_DebugInfo.AutoSize = true;
            this.label_DebugInfo.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DebugInfo.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_DebugInfo.Location = new System.Drawing.Point(197, 15);
            this.label_DebugInfo.Name = "label_DebugInfo";
            this.label_DebugInfo.Size = new System.Drawing.Size(52, 21);
            this.label_DebugInfo.TabIndex = 7;
            this.label_DebugInfo.Text = "Release";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(263, 303);
            this.Controls.Add(this.panel_all);
            this.Controls.Add(this.groupBox_techdeclare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.Text = "关于";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.Shown += new System.EventHandler(this.AboutForm_Shown);
            this.groupBox_techdeclare.ResumeLayout(false);
            this.panel_all.ResumeLayout(false);
            this.panel_all.PerformLayout();
            this.groupBox_tech.ResumeLayout(false);
            this.groupBox_tech.PerformLayout();
            this.groupBox_program.ResumeLayout(false);
            this.groupBox_program.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_techdeclare;
        private System.Windows.Forms.Label label_techdeclare;
        private System.Windows.Forms.Panel panel_all;
        private System.Windows.Forms.Label label_developer;
        private System.Windows.Forms.GroupBox groupBox_tech;
        private System.Windows.Forms.Label label_techdec;
        private System.Windows.Forms.TextBox textBox_lang;
        private System.Windows.Forms.TextBox textBox_adminstatus;
        private System.Windows.Forms.TextBox textBox_currentproc;
        private System.Windows.Forms.Label label_adminstatus;
        private System.Windows.Forms.Label label_lang;
        private System.Windows.Forms.Label label_currentproc;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.GroupBox groupBox_program;
        private System.Windows.Forms.TextBox textBox_dotnetCLRver;
        private System.Windows.Forms.Label label_dotnetCLRver;
        private System.Windows.Forms.TextBox textBox_filever;
        private System.Windows.Forms.Label label_filever;
        private System.Windows.Forms.TextBox textBox_progver;
        private System.Windows.Forms.Label label_progver;
        private System.Windows.Forms.Label label_programname;
        private System.Windows.Forms.Label label_DebugInfo;
    }
}