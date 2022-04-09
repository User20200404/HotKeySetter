using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    public partial class SettingsForm : Form
    {
        private Panel displayedPanel;
        private Button displayedButton;
        private TimeValidate lastPanelAnimation = new TimeValidate();
        private const bool timeValidateTool = false;
        private readonly string dllReferenceConfigPath = ConfigFile.HotKeySetterConfigDirectory + @"\Reference.ini";
        private bool useResizeAnimation;  //是否启用缩放动画。
        private Settings settings;
        private TimeValidateItem activateResizeRefresh = new TimeValidateItem("activateResizeRefresh",DateTime.Now);

        public delegate void SettingsUpdateRequiredEventHandler();
        public event SettingsUpdateRequiredEventHandler SettingsUpdateRequired;




        public SettingsForm()
        {

            //初始化Settings类
            settings = new Settings();
            InitializeComponent();
        }

        /// <summary>
        /// 使需要用到透明度的控件分层。
        /// </summary>
        private void InitLayeredAttributes()
        {
            Win32UIApi.AddWindowExstyle(panel_index.Handle, 0, "WS_EX_LAYERED", 0);
            Win32UIApi.AddWindowExstyle(panel_base.Handle, 0, "WS_EX_LAYERED", 0);
            Win32UIApi.AddWindowExstyle(panel_other.Handle, 0, "WS_EX_LAYERED", 0);

            Win32UIApi.SetLayeredWindowAttributes(panel_index.Handle, 0, 0, Win32UIApi.LayeredMode.LWA_ALPHA);
            Win32UIApi.SetLayeredWindowAttributes(panel_base.Handle, 0, 0, Win32UIApi.LayeredMode.LWA_ALPHA);
            Win32UIApi.SetLayeredWindowAttributes(panel_other.Handle, 0, 0, Win32UIApi.LayeredMode.LWA_ALPHA);
            lastPanelAnimation.Items.Add(new TimeValidateItem(timeValidateTool));
        }

        private void LoadAnimationCallBack(Control sender)
        {
            panel_All.Show();
            panel_index.Refresh();
        }
        /// <summary>
        /// 激活加载动画。
        /// </summary>
        private void PerformLoadAnimation()
        {
           
            new ControlAnimation(50, 350).FadeIn(panel_settings_label, ControlAnimation.FadeMethod.FromUpToDown, 250);
            new ControlAnimation(panel_index.Width, 600) { FadeBeginEventHandlerToAdd = LoadAnimationCallBack }.FadeIn(panel_index, ControlAnimation.FadeMethod.FromLeftToRight, 0);
            new ControlAnimation(panel_SaveAndApply.Height, 550).FadeIn(panel_SaveAndApply, ControlAnimation.FadeMethod.FromDownToUp, 50);
            new Gradient(button_Settings_Label, "Left", Gradient.ObjectMemberType.Property, this).BeginBezier(displayedButton.Left + 50, 400, ControlAnimation.BezierAnimationInPoints, 150);
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Control mainForm = Tag as Control;
            Left = mainForm.Left;
            Top = mainForm.Top;


            button_Base.Tag = panel_base;
            button_Settings_Label.Tag = panel_settings_label;
            button_Other.Tag = panel_other;

            displayedPanel = panel_settings_label;
            displayedButton = button_Settings_Label;


          
            ReadDLLReferenceConfig();
            ReadAnimationConfig();
            ReadNotifyConfig();
            ReadDeveloperConfig();
            ReadShortCutConfig();
            InitLayeredAttributes();
            // InitVisibleStatus();


            //警告图标
            pictureBox_WarnIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap bitmap_WarnIcon = (Bitmap)ProgramIcons.WarnIcon;
            bitmap_WarnIcon.MakeTransparent(Color.Black);
            pictureBox_WarnIcon.Image = bitmap_WarnIcon;
            //信息图标
            pictureBox_Info.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap bitmap_InfoIcon = (Bitmap)ProgramIcons.InfoIcon;
            bitmap_InfoIcon.MakeTransparent(Color.Black);
            pictureBox_Info.Image = bitmap_InfoIcon;

            //应用Size属性。
            ReadAndApplyWindowSizeConfig();

        }
        private void InitVisibleStatus()
        {
            panel_base.Visible = false;
            panel_other.Visible = false;
            panel_settings_label.Visible = true;
        }

        private void ReadDeveloperConfig()
        {
            checkBox_IgnoreException.Checked = settings.Others.Developers.IgnoreException;
            checkBox_OpenProcessHook.Checked = settings.Others.Developers.OpenProcessHook;

            if(!Privilege.IsProgramRunningAsAdmin())
            {
                checkBox_OpenProcessHook.Enabled = false;
                label_OpenProcessHook.Text = "以管理员身份重启HotKeySetter以更改该设置。";
                label_OpenProcessHook.ForeColor = Color.Orange;
            }
        }
        private void ReadShortCutConfig()
        {
            checkBox_HOMEShowHide.Checked = settings.Bases.ShortCuts.HOMEShowHide;
            checkBox_LoadFileHide.Checked = settings.Bases.ShortCuts.LoadFileHide;
        }
        private void ReadNotifyConfig()
        {
            checkBox_ShowFiredNotify.Checked = settings.Bases.Notifys.ShowFiredNotify;
            textBox_NotifyMinSpan.Text = settings.Bases.Notifys.NotifyMinSpan.ToString();
        }
        private void IndexButtons_Click(object sender, EventArgs e)
        {
            bool animationEnabled = settings.Bases.Animations.PageSwitchAnimation;

            Button currentButton = sender as Button;
            if (currentButton != null && (!lastPanelAnimation.Items[0].Check(610) || !animationEnabled))
            {
                //Panel切换页面动画部分
                //初始化Panel切换页面动画数值
                int fadeInLength;
                int fadeOutLength;
                uint animationDelay;

                if (animationEnabled)
                {
                    fadeOutLength = 150;
                    fadeInLength = 600;
                    animationDelay = 50;
                }
                else
                {
                    fadeOutLength = 0;
                    fadeInLength = 0;
                    animationDelay = 0;
                }

                //如果点击的按钮和对应的页面不对应，就应该切换页面。
                if (displayedPanel != currentButton.Tag)
                {
                    displayedPanel.SendToBack();
                    new ControlAnimation(0, fadeOutLength).FadeOut(displayedPanel, ControlAnimation.FadeMethod.FromDownToUp, 0);
                    displayedPanel = currentButton.Tag as Panel;
                    displayedPanel.BringToFront();
                    new ControlAnimation(100, fadeInLength).FadeIn(displayedPanel, ControlAnimation.FadeMethod.FromUpToDown, animationDelay);
                    lastPanelAnimation.Items[0].UpdateRecordTime();

                    //设置前后Z序
             

                }


                //Button动画部分
                if (displayedButton != currentButton)
                {
                    new Gradient(currentButton, "Left", Gradient.ObjectMemberType.Property, this).BeginBezier(currentButton.Left + 50, fadeInLength, ControlAnimation.BezierAnimationInPoints);
                    new Gradient(displayedButton, "Left", Gradient.ObjectMemberType.Property, this).BeginBezier(displayedButton.Left - 50, fadeInLength, ControlAnimation.BezierAnimationInPoints);
                    displayedButton = currentButton;
                }

                //切换页面也进行重绘防止页面渲染错误
                RefreshAllPanels();

            }
        }

        private void RefreshAllPanels()
        {
            panel_base.Refresh();
            panel_index.Refresh();
            panel_other.Refresh();
            panel_SaveAndApply.Refresh();
            panel_settings_label.Refresh();
        }

        private void button_AddReference_Click(object sender, EventArgs e)
        {
            AddDLLReferenceForm addDLLForm = new AddDLLReferenceForm() { Tag = this };
            addDLLForm.EnumResultAddRequired += AddDLLForm_EnumResultAddRequired;
            addDLLForm.ShowDialog();
        }

        private void AddDLLForm_EnumResultAddRequired(DynamicEventInfo infoData)
        {
            bool exist = false;
            for (int i = 0; i < externDLLReferenceListView.Items.Count; i++)
            {
                if (externDLLReferenceListView.Items[i].Text == infoData.DLLName)
                    exist = true;
            }

            if (!exist)
            {
                ListViewItem lvi = new ListViewItem(infoData.DLLName) { Checked = true };
                lvi.SubItems.Add(infoData.EnumFuncEntryPoint);
                lvi.SubItems.Add(infoData.DLLPath);
                externDLLReferenceListView.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 暂时保存动态链接库引用到settings。
        /// </summary>
        private void SaveDLLReferenceConfig()
        {
            settings.Others.AllEventInfos.Clear();
            for (int i = 0; i < externDLLReferenceListView.Items.Count; i++)
            {
                string currentItemDllPath = externDLLReferenceListView.Items[i].SubItems[2].Text;
                string currentItemEntryPoint = externDLLReferenceListView.Items[i].SubItems[1].Text;
                bool currentItemChecked = externDLLReferenceListView.Items[i].Checked;
                settings.Others.AllEventInfos.Add(new DynamicEventInfo(currentItemDllPath, currentItemEntryPoint) { LoadFromConfig = true, Enabled = currentItemChecked });
            }
        }
        /// <summary>
        /// 暂时保存动画设置到settings。
        /// </summary>
        private void SaveAnimationConfig()
        {
            settings.Bases.Animations.PageSwitchAnimation = checkBox_PageSwitchAnimation.Checked;
            settings.Bases.Animations.ResizeAnimation = checkBox_ResizeAnimation.Checked;
            settings.Bases.Animations.WindowCreatedAnimation = checkBox_WindowCreatedAnimation.Checked;
            settings.Bases.Animations.ResizeRefresh = checkBox_ResizeRefresh.Checked;
            settings.Bases.Animations.OneWayResizeAnimationOnly = checkBox_OneWayResizeAnimationOnly.Checked;
            settings.Bases.Animations.ColorGradient = checkBox_ColorGradient.Checked;
        }
        private void ReadDLLReferenceConfig()
        {
            #region OldVer
            /*
            for (int i = 0; ; i++)
            {
                string appName = "Reference" + i.ToString();
                string filePath = "";
                string entryPoint = "";
                string enabled = "";
                try
                {
                    filePath = ConfigFile.ReadFromFile(dllReferenceConfigPath, appName, "FilePath");
                    entryPoint = ConfigFile.ReadFromFile(dllReferenceConfigPath, appName, "EntryPoint");
                    enabled = ConfigFile.ReadFromFile(dllReferenceConfigPath, appName, "Enabled");
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode == 2)
                    {
                        Debug.WriteLine("引用读取结束。");
                    }
                    else throw ex;
                    break;
                }
                finally
                {
                    if (filePath != "" && entryPoint != "")
                    {
                        ListViewItem lvi = new ListViewItem(StringOperation.GetFileNameFromPath(filePath));
                        lvi.SubItems.Add(entryPoint);
                        lvi.SubItems.Add(filePath);
                        lvi.Checked = (enabled == "True");
                        externDLLReferenceListView.Items.Add(lvi);
                    }
                }
            }
            */
            #endregion

            //List<DynamicEventInfo> info =  DynamicEventInfo.GetFromConfig(ConfigFile.HotKeySetterConfigDirectory, false, true);
            List<DynamicEventInfo> info = settings.Others.AllEventInfos;
            for (int i = 0; i < info.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(info[i].DLLName);
                lvi.SubItems.Add(info[i].EnumFuncEntryPoint);
                lvi.SubItems.Add(info[i].DLLPath);
                lvi.Checked = info[i].Enabled;
                externDLLReferenceListView.Items.Add(lvi);
            }
        }
        private void ReadAnimationConfig()
        {
            checkBox_PageSwitchAnimation.Checked = settings.Bases.Animations.PageSwitchAnimation;
            checkBox_ResizeAnimation.Checked = settings.Bases.Animations.ResizeAnimation;
            checkBox_WindowCreatedAnimation.Checked = settings.Bases.Animations.WindowCreatedAnimation;
            checkBox_ResizeRefresh.Checked = settings.Bases.Animations.ResizeRefresh;
            checkBox_OneWayResizeAnimationOnly.Checked = settings.Bases.Animations.OneWayResizeAnimationOnly;
            checkBox_ColorGradient.Checked = settings.Bases.Animations.ColorGradient;
        }
        private void DeleteReference(int sIndex, int eIndex)
        {
            if (eIndex >= sIndex && eIndex < externDLLReferenceListView.Items.Count)
            {
                for (int i = eIndex; i >= sIndex; i--)
                {
                    externDLLReferenceListView.Items[i].Remove();
                }
            }
        }
        private void button_Confirm_Click(object sender, EventArgs e)
        {
            button_Apply_Click(null, null);
            Close();
        }

        /// <summary>
        /// 暂时保存通知设置到settings。
        /// </summary>
        private void SaveNotifyConfig()
        {
            settings.Bases.Notifys.ShowFiredNotify = checkBox_ShowFiredNotify.Checked;
            settings.Bases.Notifys.NotifyMinSpan = int.Parse(textBox_NotifyMinSpan.Text);
        }

        private void SaveShortCutConfig()
        {
            settings.Bases.ShortCuts.LoadFileHide = checkBox_LoadFileHide.Checked;
            settings.Bases.ShortCuts.HOMEShowHide = checkBox_HOMEShowHide.Checked;
        }

        private void SaveDeveloperConfig()
        {
            settings.Others.Developers.IgnoreException = checkBox_IgnoreException.Checked;
            settings.Others.Developers.OpenProcessHook = checkBox_OpenProcessHook.Checked;

        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            SaveDLLReferenceConfig();
            SaveAnimationConfig();
            SaveNotifyConfig();
            SaveDeveloperConfig();
            SaveShortCutConfig();
            settings.Save();
            SettingsUpdateRequired?.Invoke(); //广播设置更改事件。
        }
        private void button_RemoveReference_Click(object sender, EventArgs e)
        {
            if (externDLLReferenceListView.SelectedIndices.Count > 0)
                DeleteReference(externDLLReferenceListView.SelectedItems[0].Index, externDLLReferenceListView.SelectedItems[externDLLReferenceListView.SelectedItems.Count - 1].Index);
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            InitLayeredAttributes();
            if (settings.Bases.Animations.WindowCreatedAnimation)
                PerformLoadAnimation();
            else button_Settings_Label.Left += 50;
        }

        private void textBox_NotifyMinSpan_KeyPress(object sender, KeyPressEventArgs e)
        {
            //屏蔽非数字输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void label_OpenSettingsDir_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", ConfigFile.HotKeySetterConfigDirectory);
        }

        private void checkBox_OpenProcessHook_Click(object sender, EventArgs e)
        {
            if (checkBox_OpenProcessHook.Checked)
            {
                DialogResult result = MessageBox.Show("要启用Ring3 OpenProcess Hook，HotKeySetter将修改注册表以向所有进程注入Hook代码段，这会触发一些杀毒软件和反作弊程序的反馈。\n\n你仍然要继续吗？", "安全性警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    checkBox_OpenProcessHook.Checked = true;
                }
                else checkBox_OpenProcessHook.Checked = false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                // Check your window state here
                if ((m.WParam.ToInt32() & 0xFFF0) == 0xF030 || (m.WParam.ToInt32() & 0xFFF0) == 0xF120) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    // THe window is being maximized
                    useResizeAnimation = !settings.Bases.Animations.OneWayResizeAnimationOnly;
                    UpdateAnchors();
                    new Thread(new ThreadStart(() => { Thread.Sleep(10); PerformResizeAnimation(); })).Start(); ;
                }
            }
            base.WndProc(ref m);
        }

        protected override void DefWndProc(ref Message m)
        {
            base.DefWndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //用于判断缩放方向
                    {
                        if ((m.Result.ToInt32() == 13 || m.Result.ToInt32() == 14 || m.Result.ToInt32() == 16 || m.Result.ToInt32() == 17) && settings.Bases.Animations.OneWayResizeAnimationOnly) //对角缩放
                        {
                            useResizeAnimation = false;
                        }
                        else useResizeAnimation = true;
                        break;
                    }
            }
            return;
        }

        /// <summary>
        /// 根据是否应使用缩放动画更新Anchor。
        /// </summary>
        private void UpdateAnchors()
        {
            if (useResizeAnimation && settings.Bases.Animations.ResizeAnimation)
            {
                panel_All.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
            else
            {
                panel_All.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void SettingsForm_ResizeBegin(object sender, EventArgs e)
        {
            UpdateAnchors();
        }

        private void SettingsForm_ResizeEnd(object sender, EventArgs e)
        {
            PerformResizeAnimation();
        }

        private void PerformResizeAnimation()
        {
            if (useResizeAnimation && settings.Bases.Animations.ResizeAnimation) //若指示应使用动画且启用了动画。这个指示值在DefWndProc中设置。
            {
                new Gradient(panel_All, "Width", Gradient.ObjectMemberType.Property, this).BeginBezier(Width - 10 * DpiScale.ScaleValue, 400, ControlAnimation.BezierAnimationInPoints);
                new Gradient(panel_All, "Height", Gradient.ObjectMemberType.Property, this).BeginBezier(Height - 31 * DpiScale.ScaleValue, 400, ControlAnimation.BezierAnimationInPoints);
            }
        }

        private void Panels_Resize_Refresh(object sender, EventArgs e)
        {
            if (settings.Bases.Animations.ResizeRefresh && (!activateResizeRefresh.Check(600)|| !settings.Bases.Animations.ResizeRefresh))
                (sender as Control)?.Refresh();
        }

        private void label_settings_LocationChanged(object sender, EventArgs e)
        {
            pictureBox_Info.Location = label_settings.Location;
        }


        /// <summary>
        /// 保存SettingsForm的Size属性。
        /// </summary>
        private void SaveWindowSizeConfig()
        {
            ConfigFile.WriteStatic(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "SettingsFormWidth", Width.ToString());
            ConfigFile.WriteStatic(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "SettingsFormHeight", Height.ToString());
        }

        /// <summary>
        /// 读取并应用SettingsForm的Size属性。
        /// </summary>
        private void ReadAndApplyWindowSizeConfig()
        {
            try
            {
                Width = int.Parse(ConfigFile.ReadFromFile(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "SettingsFormWidth"));
                Height = int.Parse(ConfigFile.ReadFromFile(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "SettingsFormHeight"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReadAndApplyWindowSizeConfig ->" + ex.Message);
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowSizeConfig();
        }
    }
}
