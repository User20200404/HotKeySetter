
namespace HotKeySetter
{
    partial class SettingsForm
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
            this.panel_settings_label = new System.Windows.Forms.Panel();
            this.pictureBox_Info = new System.Windows.Forms.PictureBox();
            this.label_settings = new System.Windows.Forms.Label();
            this.panel_index = new System.Windows.Forms.Panel();
            this.label_OpenSettingsDir = new System.Windows.Forms.Label();
            this.button_Other = new System.Windows.Forms.Button();
            this.button_Base = new System.Windows.Forms.Button();
            this.button_Settings_Label = new System.Windows.Forms.Button();
            this.panel_base = new System.Windows.Forms.Panel();
            this.groupBox_ShortCut = new System.Windows.Forms.GroupBox();
            this.checkBox_HOMEShowHide = new System.Windows.Forms.CheckBox();
            this.checkBox_LoadFileHide = new System.Windows.Forms.CheckBox();
            this.groupBox_Notify = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_NotifyMinSpan = new System.Windows.Forms.TextBox();
            this.label_NotifyMinSpan = new System.Windows.Forms.Label();
            this.checkBox_ShowFiredNotify = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_ColorGradient = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_OneWayResizeAnimationOnly = new System.Windows.Forms.CheckBox();
            this.checkBox_ResizeRefresh = new System.Windows.Forms.CheckBox();
            this.checkBox_ResizeAnimation = new System.Windows.Forms.CheckBox();
            this.checkBox_WindowCreatedAnimation = new System.Windows.Forms.CheckBox();
            this.checkBox_PageSwitchAnimation = new System.Windows.Forms.CheckBox();
            this.panel_other = new System.Windows.Forms.Panel();
            this.groupBox_Developer = new System.Windows.Forms.GroupBox();
            this.label_OpenProcessHook = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_OpenProcessHook = new System.Windows.Forms.CheckBox();
            this.checkBox_IgnoreException = new System.Windows.Forms.CheckBox();
            this.pictureBox_WarnIcon = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_RemoveReference = new System.Windows.Forms.Button();
            this.button_AddReference = new System.Windows.Forms.Button();
            this.externDLLReferenceListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.button_Apply = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.panel_SaveAndApply = new System.Windows.Forms.Panel();
            this.panel_All = new System.Windows.Forms.Panel();
            this.panel_settings_label.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Info)).BeginInit();
            this.panel_index.SuspendLayout();
            this.panel_base.SuspendLayout();
            this.groupBox_ShortCut.SuspendLayout();
            this.groupBox_Notify.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_other.SuspendLayout();
            this.groupBox_Developer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_WarnIcon)).BeginInit();
            this.panel_SaveAndApply.SuspendLayout();
            this.panel_All.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_settings_label
            // 
            this.panel_settings_label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_settings_label.AutoScroll = true;
            this.panel_settings_label.BackColor = System.Drawing.Color.AliceBlue;
            this.panel_settings_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_settings_label.Controls.Add(this.pictureBox_Info);
            this.panel_settings_label.Controls.Add(this.label_settings);
            this.panel_settings_label.Location = new System.Drawing.Point(132, 3);
            this.panel_settings_label.Name = "panel_settings_label";
            this.panel_settings_label.Size = new System.Drawing.Size(506, 276);
            this.panel_settings_label.TabIndex = 1;
            // 
            // pictureBox_Info
            // 
            this.pictureBox_Info.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_Info.Location = new System.Drawing.Point(137, 123);
            this.pictureBox_Info.Name = "pictureBox_Info";
            this.pictureBox_Info.Size = new System.Drawing.Size(28, 29);
            this.pictureBox_Info.TabIndex = 2;
            this.pictureBox_Info.TabStop = false;
            // 
            // label_settings
            // 
            this.label_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_settings.Font = new System.Drawing.Font("宋体", 12.5F);
            this.label_settings.Location = new System.Drawing.Point(-2, 1);
            this.label_settings.Name = "label_settings";
            this.label_settings.Size = new System.Drawing.Size(511, 275);
            this.label_settings.TabIndex = 1;
            this.label_settings.Text = "从设置列表中选择项目";
            this.label_settings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_settings.LocationChanged += new System.EventHandler(this.label_settings_LocationChanged);
            // 
            // panel_index
            // 
            this.panel_index.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_index.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel_index.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_index.Controls.Add(this.label_OpenSettingsDir);
            this.panel_index.Controls.Add(this.button_Other);
            this.panel_index.Controls.Add(this.button_Base);
            this.panel_index.Controls.Add(this.button_Settings_Label);
            this.panel_index.Location = new System.Drawing.Point(2, 1);
            this.panel_index.Name = "panel_index";
            this.panel_index.Size = new System.Drawing.Size(124, 331);
            this.panel_index.TabIndex = 2;
            this.panel_index.SizeChanged += new System.EventHandler(this.Panels_Resize_Refresh);
            // 
            // label_OpenSettingsDir
            // 
            this.label_OpenSettingsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_OpenSettingsDir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_OpenSettingsDir.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_OpenSettingsDir.Location = new System.Drawing.Point(-2, 299);
            this.label_OpenSettingsDir.Name = "label_OpenSettingsDir";
            this.label_OpenSettingsDir.Size = new System.Drawing.Size(126, 12);
            this.label_OpenSettingsDir.TabIndex = 3;
            this.label_OpenSettingsDir.Text = "打开设置文件目录";
            this.label_OpenSettingsDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_OpenSettingsDir.Click += new System.EventHandler(this.label_OpenSettingsDir_Click);
            // 
            // button_Other
            // 
            this.button_Other.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Other.Location = new System.Drawing.Point(-94, 79);
            this.button_Other.Name = "button_Other";
            this.button_Other.Size = new System.Drawing.Size(245, 30);
            this.button_Other.TabIndex = 2;
            this.button_Other.Text = "---------------其他设置";
            this.button_Other.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Other.UseVisualStyleBackColor = true;
            this.button_Other.Click += new System.EventHandler(this.IndexButtons_Click);
            // 
            // button_Base
            // 
            this.button_Base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Base.Location = new System.Drawing.Point(-94, 50);
            this.button_Base.Name = "button_Base";
            this.button_Base.Size = new System.Drawing.Size(245, 30);
            this.button_Base.TabIndex = 1;
            this.button_Base.Text = "---------------基本设置";
            this.button_Base.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Base.UseVisualStyleBackColor = true;
            this.button_Base.Click += new System.EventHandler(this.IndexButtons_Click);
            // 
            // button_Settings_Label
            // 
            this.button_Settings_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Settings_Label.Location = new System.Drawing.Point(-95, 0);
            this.button_Settings_Label.Name = "button_Settings_Label";
            this.button_Settings_Label.Size = new System.Drawing.Size(245, 51);
            this.button_Settings_Label.TabIndex = 0;
            this.button_Settings_Label.Text = "---------------设置列表";
            this.button_Settings_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Settings_Label.UseVisualStyleBackColor = true;
            this.button_Settings_Label.Click += new System.EventHandler(this.IndexButtons_Click);
            // 
            // panel_base
            // 
            this.panel_base.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_base.AutoScroll = true;
            this.panel_base.BackColor = System.Drawing.Color.AliceBlue;
            this.panel_base.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_base.Controls.Add(this.groupBox_ShortCut);
            this.panel_base.Controls.Add(this.groupBox_Notify);
            this.panel_base.Controls.Add(this.groupBox1);
            this.panel_base.Location = new System.Drawing.Point(132, 3);
            this.panel_base.Name = "panel_base";
            this.panel_base.Size = new System.Drawing.Size(506, 276);
            this.panel_base.TabIndex = 3;
            this.panel_base.SizeChanged += new System.EventHandler(this.Panels_Resize_Refresh);
            // 
            // groupBox_ShortCut
            // 
            this.groupBox_ShortCut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_ShortCut.Controls.Add(this.checkBox_HOMEShowHide);
            this.groupBox_ShortCut.Controls.Add(this.checkBox_LoadFileHide);
            this.groupBox_ShortCut.Location = new System.Drawing.Point(16, 305);
            this.groupBox_ShortCut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_ShortCut.Name = "groupBox_ShortCut";
            this.groupBox_ShortCut.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_ShortCut.Size = new System.Drawing.Size(467, 70);
            this.groupBox_ShortCut.TabIndex = 2;
            this.groupBox_ShortCut.TabStop = false;
            this.groupBox_ShortCut.Text = "快捷操作";
            // 
            // checkBox_HOMEShowHide
            // 
            this.checkBox_HOMEShowHide.AutoSize = true;
            this.checkBox_HOMEShowHide.Location = new System.Drawing.Point(18, 45);
            this.checkBox_HOMEShowHide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_HOMEShowHide.Name = "checkBox_HOMEShowHide";
            this.checkBox_HOMEShowHide.Size = new System.Drawing.Size(210, 16);
            this.checkBox_HOMEShowHide.TabIndex = 1;
            this.checkBox_HOMEShowHide.Text = "HOME键呼出/隐藏(热键占用时无效)";
            this.checkBox_HOMEShowHide.UseVisualStyleBackColor = true;
            // 
            // checkBox_LoadFileHide
            // 
            this.checkBox_LoadFileHide.AutoSize = true;
            this.checkBox_LoadFileHide.Location = new System.Drawing.Point(18, 20);
            this.checkBox_LoadFileHide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_LoadFileHide.Name = "checkBox_LoadFileHide";
            this.checkBox_LoadFileHide.Size = new System.Drawing.Size(276, 16);
            this.checkBox_LoadFileHide.TabIndex = 0;
            this.checkBox_LoadFileHide.Text = "在启动参数指定文件且读取成功时，隐藏主窗口";
            this.checkBox_LoadFileHide.UseVisualStyleBackColor = true;
            // 
            // groupBox_Notify
            // 
            this.groupBox_Notify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Notify.Controls.Add(this.label3);
            this.groupBox_Notify.Controls.Add(this.textBox_NotifyMinSpan);
            this.groupBox_Notify.Controls.Add(this.label_NotifyMinSpan);
            this.groupBox_Notify.Controls.Add(this.checkBox_ShowFiredNotify);
            this.groupBox_Notify.Location = new System.Drawing.Point(16, 208);
            this.groupBox_Notify.Name = "groupBox_Notify";
            this.groupBox_Notify.Size = new System.Drawing.Size(467, 91);
            this.groupBox_Notify.TabIndex = 1;
            this.groupBox_Notify.TabStop = false;
            this.groupBox_Notify.Text = "通知";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(281, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "在最小间隔时间内，热键触发的通知不会再次显示。";
            // 
            // textBox_NotifyMinSpan
            // 
            this.textBox_NotifyMinSpan.Location = new System.Drawing.Point(121, 39);
            this.textBox_NotifyMinSpan.MaxLength = 6;
            this.textBox_NotifyMinSpan.Name = "textBox_NotifyMinSpan";
            this.textBox_NotifyMinSpan.Size = new System.Drawing.Size(93, 21);
            this.textBox_NotifyMinSpan.TabIndex = 2;
            this.textBox_NotifyMinSpan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_NotifyMinSpan_KeyPress);
            // 
            // label_NotifyMinSpan
            // 
            this.label_NotifyMinSpan.AutoSize = true;
            this.label_NotifyMinSpan.Location = new System.Drawing.Point(45, 44);
            this.label_NotifyMinSpan.Name = "label_NotifyMinSpan";
            this.label_NotifyMinSpan.Size = new System.Drawing.Size(77, 12);
            this.label_NotifyMinSpan.TabIndex = 1;
            this.label_NotifyMinSpan.Text = "最小间隔(ms)";
            // 
            // checkBox_ShowFiredNotify
            // 
            this.checkBox_ShowFiredNotify.AutoSize = true;
            this.checkBox_ShowFiredNotify.Location = new System.Drawing.Point(18, 21);
            this.checkBox_ShowFiredNotify.Name = "checkBox_ShowFiredNotify";
            this.checkBox_ShowFiredNotify.Size = new System.Drawing.Size(144, 16);
            this.checkBox_ShowFiredNotify.TabIndex = 0;
            this.checkBox_ShowFiredNotify.Text = "在触发热键时显示通知";
            this.checkBox_ShowFiredNotify.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox_ColorGradient);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBox_OneWayResizeAnimationOnly);
            this.groupBox1.Controls.Add(this.checkBox_ResizeRefresh);
            this.groupBox1.Controls.Add(this.checkBox_ResizeAnimation);
            this.groupBox1.Controls.Add(this.checkBox_WindowCreatedAnimation);
            this.groupBox1.Controls.Add(this.checkBox_PageSwitchAnimation);
            this.groupBox1.Location = new System.Drawing.Point(16, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 187);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "动画效果";
            // 
            // checkBox_ColorGradient
            // 
            this.checkBox_ColorGradient.AutoSize = true;
            this.checkBox_ColorGradient.Location = new System.Drawing.Point(18, 166);
            this.checkBox_ColorGradient.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_ColorGradient.Name = "checkBox_ColorGradient";
            this.checkBox_ColorGradient.Size = new System.Drawing.Size(264, 16);
            this.checkBox_ColorGradient.TabIndex = 5;
            this.checkBox_ColorGradient.Text = "使用颜色渐变指示热键项延迟执行和最小间隔";
            this.checkBox_ColorGradient.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(317, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "这是旧版遗留的选项，若继续出现黑块问题，请启用此项。";
            // 
            // checkBox_OneWayResizeAnimationOnly
            // 
            this.checkBox_OneWayResizeAnimationOnly.AutoSize = true;
            this.checkBox_OneWayResizeAnimationOnly.Location = new System.Drawing.Point(45, 101);
            this.checkBox_OneWayResizeAnimationOnly.Name = "checkBox_OneWayResizeAnimationOnly";
            this.checkBox_OneWayResizeAnimationOnly.Size = new System.Drawing.Size(324, 16);
            this.checkBox_OneWayResizeAnimationOnly.TabIndex = 4;
            this.checkBox_OneWayResizeAnimationOnly.Text = "仅在单向缩放(水平或垂直)时使用动画(有利于改善表现)";
            this.checkBox_OneWayResizeAnimationOnly.UseVisualStyleBackColor = true;
            // 
            // checkBox_ResizeRefresh
            // 
            this.checkBox_ResizeRefresh.AutoSize = true;
            this.checkBox_ResizeRefresh.Location = new System.Drawing.Point(18, 127);
            this.checkBox_ResizeRefresh.Name = "checkBox_ResizeRefresh";
            this.checkBox_ResizeRefresh.Size = new System.Drawing.Size(360, 16);
            this.checkBox_ResizeRefresh.TabIndex = 3;
            this.checkBox_ResizeRefresh.Text = "(旧)窗口缩放时重绘(解决黑块渲染错误，会增加缩放响应延迟)";
            this.checkBox_ResizeRefresh.UseVisualStyleBackColor = true;
            // 
            // checkBox_ResizeAnimation
            // 
            this.checkBox_ResizeAnimation.AutoSize = true;
            this.checkBox_ResizeAnimation.Location = new System.Drawing.Point(18, 79);
            this.checkBox_ResizeAnimation.Name = "checkBox_ResizeAnimation";
            this.checkBox_ResizeAnimation.Size = new System.Drawing.Size(336, 16);
            this.checkBox_ResizeAnimation.TabIndex = 2;
            this.checkBox_ResizeAnimation.Text = "窗口缩放时动画(不推荐，在GDI+高速绘图会产生撕裂效果)\r\n";
            this.checkBox_ResizeAnimation.UseVisualStyleBackColor = true;
            // 
            // checkBox_WindowCreatedAnimation
            // 
            this.checkBox_WindowCreatedAnimation.AutoSize = true;
            this.checkBox_WindowCreatedAnimation.Location = new System.Drawing.Point(18, 51);
            this.checkBox_WindowCreatedAnimation.Name = "checkBox_WindowCreatedAnimation";
            this.checkBox_WindowCreatedAnimation.Size = new System.Drawing.Size(108, 16);
            this.checkBox_WindowCreatedAnimation.TabIndex = 1;
            this.checkBox_WindowCreatedAnimation.Text = "窗口创建时动画";
            this.checkBox_WindowCreatedAnimation.UseVisualStyleBackColor = true;
            // 
            // checkBox_PageSwitchAnimation
            // 
            this.checkBox_PageSwitchAnimation.AutoSize = true;
            this.checkBox_PageSwitchAnimation.Location = new System.Drawing.Point(18, 23);
            this.checkBox_PageSwitchAnimation.Name = "checkBox_PageSwitchAnimation";
            this.checkBox_PageSwitchAnimation.Size = new System.Drawing.Size(108, 16);
            this.checkBox_PageSwitchAnimation.TabIndex = 0;
            this.checkBox_PageSwitchAnimation.Text = "页面切换时动画";
            this.checkBox_PageSwitchAnimation.UseVisualStyleBackColor = true;
            // 
            // panel_other
            // 
            this.panel_other.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_other.AutoScroll = true;
            this.panel_other.BackColor = System.Drawing.Color.AliceBlue;
            this.panel_other.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_other.Controls.Add(this.groupBox_Developer);
            this.panel_other.Controls.Add(this.pictureBox_WarnIcon);
            this.panel_other.Controls.Add(this.label2);
            this.panel_other.Controls.Add(this.button_RemoveReference);
            this.panel_other.Controls.Add(this.button_AddReference);
            this.panel_other.Controls.Add(this.externDLLReferenceListView);
            this.panel_other.Controls.Add(this.label1);
            this.panel_other.Location = new System.Drawing.Point(132, 3);
            this.panel_other.Name = "panel_other";
            this.panel_other.Size = new System.Drawing.Size(506, 276);
            this.panel_other.TabIndex = 4;
            this.panel_other.SizeChanged += new System.EventHandler(this.Panels_Resize_Refresh);
            // 
            // groupBox_Developer
            // 
            this.groupBox_Developer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Developer.Controls.Add(this.label_OpenProcessHook);
            this.groupBox_Developer.Controls.Add(this.label4);
            this.groupBox_Developer.Controls.Add(this.checkBox_OpenProcessHook);
            this.groupBox_Developer.Controls.Add(this.checkBox_IgnoreException);
            this.groupBox_Developer.Location = new System.Drawing.Point(6, 206);
            this.groupBox_Developer.Name = "groupBox_Developer";
            this.groupBox_Developer.Size = new System.Drawing.Size(491, 66);
            this.groupBox_Developer.TabIndex = 6;
            this.groupBox_Developer.TabStop = false;
            this.groupBox_Developer.Text = "开发者选项";
            // 
            // label_OpenProcessHook
            // 
            this.label_OpenProcessHook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_OpenProcessHook.AutoSize = true;
            this.label_OpenProcessHook.Location = new System.Drawing.Point(207, 44);
            this.label_OpenProcessHook.Name = "label_OpenProcessHook";
            this.label_OpenProcessHook.Size = new System.Drawing.Size(239, 12);
            this.label_OpenProcessHook.TabIndex = 3;
            this.label_OpenProcessHook.Text = "拒绝WIN32程序对HotKeySetter进程的访问。";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(317, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "非致命性异常将不会被抛出，这可能会造成程序的不稳定。";
            // 
            // checkBox_OpenProcessHook
            // 
            this.checkBox_OpenProcessHook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_OpenProcessHook.AutoSize = true;
            this.checkBox_OpenProcessHook.Location = new System.Drawing.Point(10, 44);
            this.checkBox_OpenProcessHook.Name = "checkBox_OpenProcessHook";
            this.checkBox_OpenProcessHook.Size = new System.Drawing.Size(156, 16);
            this.checkBox_OpenProcessHook.TabIndex = 1;
            this.checkBox_OpenProcessHook.Text = "Ring3 OpenProcess Hook";
            this.checkBox_OpenProcessHook.UseVisualStyleBackColor = true;
            this.checkBox_OpenProcessHook.Click += new System.EventHandler(this.checkBox_OpenProcessHook_Click);
            // 
            // checkBox_IgnoreException
            // 
            this.checkBox_IgnoreException.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_IgnoreException.AutoSize = true;
            this.checkBox_IgnoreException.Location = new System.Drawing.Point(10, 20);
            this.checkBox_IgnoreException.Name = "checkBox_IgnoreException";
            this.checkBox_IgnoreException.Size = new System.Drawing.Size(96, 16);
            this.checkBox_IgnoreException.TabIndex = 0;
            this.checkBox_IgnoreException.Text = "忽略所有异常";
            this.checkBox_IgnoreException.UseVisualStyleBackColor = true;
            // 
            // pictureBox_WarnIcon
            // 
            this.pictureBox_WarnIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_WarnIcon.Location = new System.Drawing.Point(20, 171);
            this.pictureBox_WarnIcon.Name = "pictureBox_WarnIcon";
            this.pictureBox_WarnIcon.Size = new System.Drawing.Size(22, 22);
            this.pictureBox_WarnIcon.TabIndex = 5;
            this.pictureBox_WarnIcon.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(44, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "更改引用可能会导致已有配置无法加载\r\nHotKeySetter不保证第三方动态链接库程序的安全性。";
            // 
            // button_RemoveReference
            // 
            this.button_RemoveReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RemoveReference.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_RemoveReference.Location = new System.Drawing.Point(341, 172);
            this.button_RemoveReference.Name = "button_RemoveReference";
            this.button_RemoveReference.Size = new System.Drawing.Size(75, 23);
            this.button_RemoveReference.TabIndex = 3;
            this.button_RemoveReference.Text = "移除";
            this.button_RemoveReference.UseVisualStyleBackColor = true;
            this.button_RemoveReference.Click += new System.EventHandler(this.button_RemoveReference_Click);
            // 
            // button_AddReference
            // 
            this.button_AddReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_AddReference.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_AddReference.Location = new System.Drawing.Point(422, 172);
            this.button_AddReference.Name = "button_AddReference";
            this.button_AddReference.Size = new System.Drawing.Size(75, 23);
            this.button_AddReference.TabIndex = 2;
            this.button_AddReference.Text = "添加";
            this.button_AddReference.UseVisualStyleBackColor = true;
            this.button_AddReference.Click += new System.EventHandler(this.button_AddReference_Click);
            // 
            // externDLLReferenceListView
            // 
            this.externDLLReferenceListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.externDLLReferenceListView.CheckBoxes = true;
            this.externDLLReferenceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.externDLLReferenceListView.FullRowSelect = true;
            this.externDLLReferenceListView.GridLines = true;
            this.externDLLReferenceListView.HideSelection = false;
            this.externDLLReferenceListView.Location = new System.Drawing.Point(7, 37);
            this.externDLLReferenceListView.Name = "externDLLReferenceListView";
            this.externDLLReferenceListView.Size = new System.Drawing.Size(490, 127);
            this.externDLLReferenceListView.TabIndex = 1;
            this.externDLLReferenceListView.UseCompatibleStateImageBehavior = false;
            this.externDLLReferenceListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "引用";
            this.columnHeader1.Width = 179;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "枚举函数入口";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "完整路径";
            this.columnHeader3.Width = 400;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "外部动态链接库引用";
            // 
            // button_Apply
            // 
            this.button_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Apply.Location = new System.Drawing.Point(337, 3);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(85, 29);
            this.button_Apply.TabIndex = 8;
            this.button_Apply.Text = "应用";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cancel.Location = new System.Drawing.Point(10, 3);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(85, 29);
            this.button_Cancel.TabIndex = 10;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Confirm.Location = new System.Drawing.Point(428, 3);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(85, 29);
            this.button_Confirm.TabIndex = 9;
            this.button_Confirm.Text = "确定";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // panel_SaveAndApply
            // 
            this.panel_SaveAndApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_SaveAndApply.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel_SaveAndApply.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SaveAndApply.Controls.Add(this.button_Apply);
            this.panel_SaveAndApply.Controls.Add(this.button_Confirm);
            this.panel_SaveAndApply.Controls.Add(this.button_Cancel);
            this.panel_SaveAndApply.Location = new System.Drawing.Point(120, 279);
            this.panel_SaveAndApply.Name = "panel_SaveAndApply";
            this.panel_SaveAndApply.Size = new System.Drawing.Size(523, 43);
            this.panel_SaveAndApply.TabIndex = 11;
            this.panel_SaveAndApply.SizeChanged += new System.EventHandler(this.Panels_Resize_Refresh);
            // 
            // panel_All
            // 
            this.panel_All.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_All.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_All.Controls.Add(this.panel_index);
            this.panel_All.Controls.Add(this.panel_SaveAndApply);
            this.panel_All.Controls.Add(this.panel_settings_label);
            this.panel_All.Controls.Add(this.panel_other);
            this.panel_All.Controls.Add(this.panel_base);
            this.panel_All.Location = new System.Drawing.Point(0, 0);
            this.panel_All.Name = "panel_All";
            this.panel_All.Size = new System.Drawing.Size(648, 324);
            this.panel_All.TabIndex = 2;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(644, 316);
            this.Controls.Add(this.panel_All);
            this.MinimumSize = new System.Drawing.Size(657, 284);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "HotKeySetter设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.ResizeBegin += new System.EventHandler(this.SettingsForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.SettingsForm_ResizeEnd);
            this.panel_settings_label.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Info)).EndInit();
            this.panel_index.ResumeLayout(false);
            this.panel_base.ResumeLayout(false);
            this.groupBox_ShortCut.ResumeLayout(false);
            this.groupBox_ShortCut.PerformLayout();
            this.groupBox_Notify.ResumeLayout(false);
            this.groupBox_Notify.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_other.ResumeLayout(false);
            this.panel_other.PerformLayout();
            this.groupBox_Developer.ResumeLayout(false);
            this.groupBox_Developer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_WarnIcon)).EndInit();
            this.panel_SaveAndApply.ResumeLayout(false);
            this.panel_All.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_settings_label;
        private System.Windows.Forms.Panel panel_index;
        private System.Windows.Forms.Button button_Other;
        private System.Windows.Forms.Button button_Base;
        private System.Windows.Forms.Button button_Settings_Label;
        private System.Windows.Forms.Panel panel_base;
        private System.Windows.Forms.Label label_settings;
        private System.Windows.Forms.Panel panel_other;
        private System.Windows.Forms.ListView externDLLReferenceListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_RemoveReference;
        private System.Windows.Forms.Button button_AddReference;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.Panel panel_SaveAndApply;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_ResizeAnimation;
        private System.Windows.Forms.CheckBox checkBox_WindowCreatedAnimation;
        private System.Windows.Forms.CheckBox checkBox_PageSwitchAnimation;
        private System.Windows.Forms.CheckBox checkBox_ResizeRefresh;
        private System.Windows.Forms.CheckBox checkBox_OneWayResizeAnimationOnly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox_WarnIcon;
        private System.Windows.Forms.GroupBox groupBox_Notify;
        private System.Windows.Forms.CheckBox checkBox_ShowFiredNotify;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_NotifyMinSpan;
        private System.Windows.Forms.Label label_NotifyMinSpan;
        private System.Windows.Forms.Label label_OpenSettingsDir;
        private System.Windows.Forms.GroupBox groupBox_Developer;
        private System.Windows.Forms.CheckBox checkBox_OpenProcessHook;
        private System.Windows.Forms.CheckBox checkBox_IgnoreException;
        private System.Windows.Forms.Label label_OpenProcessHook;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel_All;
        private System.Windows.Forms.PictureBox pictureBox_Info;
        private System.Windows.Forms.GroupBox groupBox_ShortCut;
        private System.Windows.Forms.CheckBox checkBox_HOMEShowHide;
        private System.Windows.Forms.CheckBox checkBox_LoadFileHide;
        private System.Windows.Forms.CheckBox checkBox_ColorGradient;
    }
}