using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

/*开发指导：
     热键注册    
         1.注册时间验证项(以热键ID为分辨符)
         2.更新列表内容
     热键卸载：
         1.注销时间验证项
         2.删除列表内容
*/

namespace HotKeySetter
{
    public partial class MainForm : Form
    {
        string[] commandLine;
        private List<DynamicEventInfo> eventInfos; //用于装载动态链接库。
        private List<HotKeyInfo> hotKeyInfos = new List<HotKeyInfo>(); //用于储存热键列表。
        private DataTable eventTable = new DataTable();                //用于储存事件表。
        private TimeValidate timeValidate = new TimeValidate();           //时间验证对象。
        private ListViewColumnStatus columnStatus;             //记录列是否要显示。
        private TimeValidateItem pageSwitchTimeValidateItem;   //用于记录页面切换时间。
        private TimeValidateItem eventFiredNotifyTimeValidateItem; //用于记录热键事件触发时间。
        private int pageIndex = 0;                             //用于记录页面索引。
        private bool useResizeAnimation;                       //用于指示是否应使用Resize动画。
        private Settings programSettings = new Settings();

        private const int runPageIndex = 0;
        private const int newPageIndex = 1;
        /// <summary>
        /// 指示了热键信息对应的列索引。
        /// </summary>
        private class ListViewColumnStatus
        {
            public int NameIndex, CountIndex, KeyNameIndex, EventTypeIndex, FirstKeyCodeIndex, SecondKeyCodeIndex, DelayIndex, MinSpanIndex, ParamIndex;
            public ListViewColumnStatus(int NameIndex, int CountIndex, int KeyNameIndex, int EventTypeIndex, int FirstKeyCodeIndex, int SecondKeyCodeIndex, int DelayIndex, int MinSpanIndex, int ParamIndex)
            {
                this.NameIndex = NameIndex;
                this.CountIndex = CountIndex;
                this.KeyNameIndex = KeyNameIndex;
                this.EventTypeIndex = EventTypeIndex;
                this.FirstKeyCodeIndex = FirstKeyCodeIndex;
                this.SecondKeyCodeIndex = SecondKeyCodeIndex;
                this.DelayIndex = DelayIndex;
                this.MinSpanIndex = MinSpanIndex;
                this.ParamIndex = ParamIndex;
            }

            /// <summary>
            /// 获取被显示的项目个数。
            /// </summary>
            public int DisplayedCount
            {
                get
                {
                    int ret = 9;
                    if (NameIndex == -1)
                        ret--;
                    if (CountIndex == -1)
                        ret--;
                    if (KeyNameIndex == -1)
                        ret--;
                    if (EventTypeIndex == -1)
                        ret--;
                    if (FirstKeyCodeIndex == -1)
                        ret--;
                    if (SecondKeyCodeIndex == -1)
                        ret--;
                    if (DelayIndex == -1)
                        ret--;
                    if (MinSpanIndex == -1)
                        ret--;
                    if (ParamIndex == -1)
                        ret--;
                    return ret;
                }
            }
        }


        /// <summary>
        /// 查询指定热键是否被禁用。
        /// </summary>
        /// <param name="hotkey_index">热键在列表中的索引值。</param>
        /// <returns>热键未被禁用返回true，否则返回false。</returns>
        private bool IsHotkeyEnabled(int hotkey_index)
        {
            return !hotKeyListView.Items[hotkey_index].Checked;
        }
        private int SelectedCount
        {
            get
            {
                return hotKeyListView.SelectedItems.Count;
            }
        }

        public MainForm(string[] args)
        {
            commandLine = args;
            InitializeComponent();
            //先隐藏窗口
            //  ShowInTaskbar = false;
           // Opacity = 0;

        }

        /// <summary>
        /// 通过EventID寻找事件在eventTable中的索引。
        /// </summary>
        /// <param name="eid">即EventID。</param>
        /// <returns>成功返回索引，失败返回-1。</returns>
        private int FindEvent(string eid)
        {
            string dllName;
            int eventIndexInDll;
            dllName = StringOperation.GetSplittedString(eid, "|", 1);
            eventIndexInDll = int.Parse(StringOperation.GetSplittedString(eid, "|", 4));
            
            for(int i = 0;i<eventTable.Rows.Count;i++)
            {
                DataRow dr = eventTable.Rows[i];
                if (dr["dllName"].ToString() == dllName && int.Parse(dr["index"].ToString()) == eventIndexInDll)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 获取列表中指定项热键的id。
        /// </summary>
        /// <param name="hotkey_index">热键的索引。</param>
        /// <returns>热键的ID。</returns>
        private int GetHotKeyIDByIndex(int hotkey_index)
        {
            return int.Parse(StringOperation.GetSplittedString(hotKeyListView.Items[hotkey_index].SubItems[3].Text, "|", 1));
        }
        private void textBox_FirstKey_KeyDown(object sender, KeyEventArgs e)
        {
            textBox_FirstKey.Text = e.KeyCode.ToString();
            textBox_KeyCode.Text = e.KeyValue.ToString();
        }

        private void textBox_KeyCode_TextChanged(object sender, EventArgs e)
        {
            //若键值有效，自动匹配按键并输出在textBox_FirstKey中
            if (CharSwitch.IsValidVirtualKeyCode(textBox_KeyCode.Text))
            {
                Keys k = (Keys)int.Parse(textBox_KeyCode.Text);
                textBox_FirstKey.Text = k.ToString();
            }
            else textBox_FirstKey.Text = "非法键代码";
        }
        /// <summary>
        /// 获取当前辅助键的设置。
        /// </summary>
        /// <returns></returns>
        private uint GetHotKeySecondKey()
        {
            return HotKey.GetSecondKeyCodeByString(comboBox_SecondKey.Text);
        }
        /// <summary>
        /// 获取首要热键按键的键代码。
        /// </summary>
        /// <returns></returns>
        private uint GetHotKeyFirstKey()
        {
            if (textBox_FirstKey.Text != "(无)" && textBox_FirstKey.Text != "非法键代码" && CharSwitch.IsValidVirtualKeyCode(textBox_KeyCode.Text))
                return (uint)int.Parse(textBox_KeyCode.Text);
            else return 0;
        }
        /// <summary>
        /// 根据现有热键标识符，获取一个可用的最小热键ID。
        /// </summary>
        /// <returns>可用的最小ID值。</returns>
        private int GetMinAvailableKeyID()
        {
            string sid;
            List<int> ids = new List<int>(255);
            int id_max = 0;
            //先排序再寻找最小可用值。
            for (int i = 0; i < hotKeyListView.Items.Count; i++)
            {
                sid = hotKeyListView.Items[i].SubItems[3].Text;
                ids.Add(hotKeyInfos[i].ID);
            }
            ids.Sort(new Comparison<int>((a, b) => { return a - b; }));
            for (int i = 1; i < ids.Count; i++)
            {
                if (ids[i] - ids[i - 1] > 1)
                    id_max = ids[i - 1] + 1;
                else id_max = ids[i] + 1;
            }
            if (ids.Count == 1)
            {
                if (ids[0] == 0)
                    return 1;
            }
            return id_max;
        }
        /// <summary>
        /// 获取当前ComboBox设置的热键ID。
        /// </summary>
        /// <returns></returns>
        private int GetHotKeyID()
        {
            switch (comboBox_HotKeyID.SelectedIndex)
            {
                case 0:
                    return GetMinAvailableKeyID();
                default:
                    if (CharSwitch.IsNumber(comboBox_HotKeyID.Text))
                    {
                        return int.Parse(comboBox_HotKeyID.Text);
                    }
                    else
                    {
                        return GetMinAvailableKeyID();
                    }
            }

        }
        /// <summary>
        /// 获取当前的热键事件操作类型。
        /// </summary>
        /// <returns></returns>
        private string GetHotKeyEventType()
        {
            return comboBox_Events.SelectedValue.ToString();
        }
        public int GetHotKeyEventTypeByIndex(int index)
        {
            return int.Parse(StringOperation.GetSplittedString(hotKeyListView.Items[index].SubItems[3].Text, "|", 4));
        }
        /// <summary>
        /// 获取当前的热键时间延迟时间。
        /// </summary>
        /// <returns></returns>
        private int GetDelayTime()
        {
            int.TryParse(textBox_Delay.Text, out int ret);
            return ret;
        }
        private int GetMinInterval()
        {
            int.TryParse(textBox_MinInterval.Text, out int ret);
            return ret;
        }
        private void InitEvents()
        {
            //加载动态事件。
            //eventInfos.Clear();
            eventInfos = DynamicEventInfo.GetFromConfig(ConfigFile.HotKeySetterConfigDirectory, true, true);

            //解析动态事件
            eventTable.Clear();
            eventTable.Columns.Clear();
            eventTable.Columns.Add("dllName");
            eventTable.Columns.Add("mainEntryPoint");
            eventTable.Columns.Add("title");
            eventTable.Columns.Add("detail");
            eventTable.Columns.Add("index");
            eventTable.Columns.Add("eid");
            for (int i = 0; i < eventInfos.Count; i++)
            {
                DynamicEventInfo currentInfo = eventInfos[i];
                for (int j = 0; j < currentInfo.MainFuncEntryPoint.Count; j++)  //MainFuncEntryPoint.Count代表了其包含的事件总数。
                {
                    //加载动态事件的相关信息。
                    DataRow dr = eventTable.NewRow();
                    dr["dllName"] = currentInfo.DLLName;
                    dr["mainEntryPoint"] = currentInfo.MainFuncEntryPoint[j];
                    dr["title"] = currentInfo.EventDescriptions[j].Title;
                    dr["detail"] = currentInfo.EventDescriptions[j].Detail;
                    dr["index"] = j.ToString();
                    dr["eid"] = StringOperation.ConstructSplittedString("|", currentInfo.DLLName, currentInfo.MainFuncEntryPoint[j], eventTable.Rows.Count.ToString(), j.ToString()); //eid格式 |dllName|mainFuncEntry|indexInEventTable|indexInDLL|
                    eventTable.Rows.Add(dr);
                }
            }

            //设置事件下拉列表的数据源为eventTable
            comboBox_Events.DataSource = eventTable;
            comboBox_Events.DisplayMember = "title";
            comboBox_Events.ValueMember = "eid";
        }

        private void SetMenuBackColor(Color color)
        {
            foreach (ToolStripItem tsmi in MainMenuStrip.Items)
            {
                if (tsmi.GetType() == typeof(ToolStripMenuItem))
                {
                    foreach (ToolStripItem childItem in (tsmi as ToolStripMenuItem).DropDownItems)
                    {
                        childItem.BackColor = color;
                    }
                }
            }
        }

        /// <summary>
        /// 更新Hook注册表注入项。
        /// </summary>
        private void UpdateHookRegistry()
        {
            if (Privilege.IsProgramRunningAsAdmin())
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                key = key.OpenSubKey(@"SoftWare\MicroSoft\Windows NT\CurrentVersion\Windows", true); //解决重定向并写入注册表

                if (programSettings.Others.Developers.OpenProcessHook)
                {
                    ConfigFile.WriteStatic(ConfigFile.HotKeySetterConfigDirectory + @"\Hook.ini", "Ring3Hook", "protectedID", Process.GetCurrentProcess().Id.ToString());
                    key.SetValue("LoadAppInit_DLLs", 1);
                }
                else
                {
                    //在非管理员权限下不进行进程保护。
                    ConfigFile.WriteStatic(ConfigFile.HotKeySetterConfigDirectory + @"\Hook.ini", "Ring3Hook", "protectedID", "-1");
                    key.SetValue("LoadAppInit_DLLs", 0);
                }
                key.SetValue("AppInit_Dlls", AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\DllHookProc.dll", RegistryValueKind.String);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateHookRegistry();
            //设置双缓冲和缓冲区绘制
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //选中自动分配
            comboBox_HotKeyID.SelectedIndex = 0;

            //更改窗口标题
            Text = "HotKeySetter" + Application.ProductVersion;

            //判断并创建配置目录。
            if (!Directory.Exists(ConfigFile.HotKeySetterConfigDirectory))
            {
                Directory.CreateDirectory(ConfigFile.HotKeySetterConfigDirectory);
            }

            //为指定操作加载UAC盾牌图标
            if (!Privilege.IsProgramRunningAsAdmin())
            {
                toolStrip_Help_AdminBoot.Image = ProgramIcons.UACIcon;
                toolStrip_Help_AdminBoot.ImageTransparentColor = Color.Black;
            }
            else Text += "(管理员)";

            //为热键项启用状态添加图标。
            ImageList listViewIcons = new ImageList();
            Bitmap bitmap_disabled = (Bitmap)ProgramIcons.DisabledIcon;
            Bitmap bitmap_enabled = (Bitmap)ProgramIcons.EnabledIcon;
            bitmap_disabled.MakeTransparent(Color.Black);
            bitmap_enabled.MakeTransparent(Color.Black);

            listViewIcons.Images.Add(bitmap_enabled);
            listViewIcons.Images.Add(bitmap_disabled);
            hotKeyListView.StateImageList = listViewIcons;

            //初始化热键事件
            InitEvents();

            //初始化列
            columnStatus = new ListViewColumnStatus(0, 2, 1, 4, 7, 8, 3, 6, 5);

            //为切换页面的动画效果设置最小激活间隔
            pageSwitchTimeValidateItem = new TimeValidateItem("PageAnimation", DateTime.Now - new TimeSpan(0, 0, 0, 0, 700));
            timeValidate.Items.Add(pageSwitchTimeValidateItem);

            //为热键时间激活通知设置最小间隔
            eventFiredNotifyTimeValidateItem = new TimeValidateItem("EventFiredNotify", DateTime.Now - TimeSpan.FromMilliseconds(programSettings.Bases.Notifys.NotifyMinSpan));
            timeValidate.Items.Add(eventFiredNotifyTimeValidateItem);

            //设置菜单颜色
            SetMenuBackColor(Color.AliceBlue);

            //初始化控件分层状态

            //设置右下角图标
            Icon = Icon.FromHandle((ProgramIcons.HotKeyFiredIcon as Bitmap).GetHicon());
            hotKeySetterNotifyIcon.Icon = Icon;
            hotKeySetterNotifyIcon.Text = Text;
            hotKeySetterNotifyIcon.MouseClick += HotKeySetterNotifyIcon_MouseClick;

            //应用Size记录属性。
            ReadAndApplyWindowSizeConfig();

            //下拉列表辅助按键选中"(无)"
            comboBox_SecondKey.SelectedIndex = 0;
        }

        private void HotKeySetterNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Visible = !Visible;
        }

        /// <summary>
        /// 执行窗口加载完毕后的动画效果。
        /// </summary>
        private void PerformLoadAnimation()
        {
            Win32UIApi.AddWindowExstyle(panel_New.Handle, 0, "WS_EX_LAYERED", 0);
            Win32UIApi.SetLayeredWindowAttributes(panel_New.Handle, 0, 0, Win32UIApi.LayeredMode.LWA_ALPHA);
            new ControlAnimation(50, 400).FadeIn(panel_ListOperation, ControlAnimation.FadeMethod.FromDownToUp, 500);
            new ControlAnimation(panel_Run.Width, 600).FadeIn(panel_Run, ControlAnimation.FadeMethod.FromRightToLeft);
        }

        protected override void DefWndProc(ref Message m)
        {
            base.DefWndProc(ref m);
            switch (m.Msg)
            {
                case 786: //收到热键消息

                    if (Hex.GetHighOrder(m.LParam) == 36 && Hex.GetLowOrder(m.LParam) == 0)
                    {
                        Visible = !Visible;
                    }
                    else DispatchHotKeyEvent(m);
                    break;

               case 0x84: //用于判断缩放方向
                    {
                        if ((m.Result.ToInt32() == 13 || m.Result.ToInt32() == 14 || m.Result.ToInt32() == 16 || m.Result.ToInt32() == 17)&&programSettings.Bases.Animations.OneWayResizeAnimationOnly) //对角缩放
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
        /// 通过键代码定位热键索引。
        /// </summary>
        /// <param name="firstKeyCode">主键代码。</param>
        /// <param name="secondKeyCode">次键代码。</param>
        /// <returns>成功返回热键索引，失败返回-1。</returns>
        private int LocateHotKeyIndex(uint firstKeyCode, uint secondKeyCode)
        {
            for (int i = 0; i < hotKeyInfos.Count; i++)
            {
                if (hotKeyInfos[i].FirstKeyCode == firstKeyCode && hotKeyInfos[i].SecondKeyCode == secondKeyCode)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 分配和触发热键事件。
        /// </summary>
        /// <param name="msg">热键消息。</param>
        private void DispatchHotKeyEvent(Message msg)
        {
            uint firstKeyCode = (uint)Hex.GetHighOrder(msg.LParam);
            uint secondKeyCode = (uint)Hex.GetLowOrder(msg.LParam);
            int hotKeyIndex = LocateHotKeyIndex(firstKeyCode, secondKeyCode);
            DoEvent(hotKeyIndex);
        }


        private int FindEventInfoIndex(string eid)
        {
            string dllName = StringOperation.GetSplittedString(eid, "|", 1);
            for(int i=0;i<eventInfos.Count;i++)
            {
                if (eventInfos[i].DLLName == dllName)
                    return i;
            }
            return -1;
        }

        private DynamicEventInfo FindEventInfo(string eid)
        {
            int index = FindEventInfoIndex(eid);
            if (index != -1)
            {
                return eventInfos[index];
            }
            else return null;
        }
        /// <summary>
        /// 执行热键事件。
        /// </summary>
        /// <param name="hotKeyIndex">热键的索引。</param>
        private void DoEvent(int hotKeyIndex)
        {
            HotKeyInfo currentInfo = hotKeyInfos[hotKeyIndex];

            //在这里进行激活事件条件判断 
            TimeValidateItem currentTimeValidateItem = timeValidate.FindItem(currentInfo.ID); //当前时间验证项。
            bool enabled = IsHotkeyEnabled(hotKeyIndex);
            bool minSpanPassed = !currentTimeValidateItem.Check(currentInfo.MinSpan);

            if (enabled && minSpanPassed) //必须符合热键未被禁用且已过去最小时间间隔。
            {
                currentInfo.Count++;

                new Thread(new ThreadStart(new DelayedEvent(FindEventInfo(currentInfo.EventID), currentInfo.EventID, currentInfo.EventArgs, currentInfo.DelayTime).DelayedEventMain)).Start();
                currentTimeValidateItem.UpdateRecordTime(TimeSpan.FromMilliseconds(currentInfo.DelayTime)); //更新验证时间。
                FreshHotKeyItem(hotKeyIndex);

                ShowStatusMessage("热键“" + currentInfo.Name + "”现在被激活。", Color.Blue);

                Gradient colorGradient = null;
      
                //进行项目延迟执行和最小间隔指示
                if (programSettings.Bases.Animations.ColorGradient)
                {
                    if (currentInfo.DelayTime > 0)
                    {
                        colorGradient = ShowItemActivatedColorEffect(hotKeyIndex, currentInfo.DelayTime, Color.DarkSeaGreen, 0); //在延迟执行前进行颜色指示渐变。
                    }
                    if (currentInfo.MinSpan > 0)
                    {
                        ShowItemActivatedColorEffect(hotKeyIndex, currentInfo.MinSpan, Color.OrangeRed, (uint)currentInfo.DelayTime, colorGradient); //在执行后最小间隔内进行颜色指示渐变。
                    }
                }


                ShowEventFiredNotify(currentInfo);
            }
            else
            {
                ShowStatusMessage("热键“" + currentInfo.Name + "”被禁用 或 距上次激活间隔不足" + currentInfo.MinSpan.ToString() + "毫秒。", Color.Orange);
            }
        }

        /// <summary>
        /// 使项目背景颜色从指定颜色渐变到初始颜色。
        /// </summary>
        /// <param name="index">热键项索引。</param>
        /// <param name="time">渐变时间。</param>
        /// <param name="beginColor">渐变开始前设置的颜色。</param>
        /// <param name="delay">延迟执行。</param>
        /// <param name="lastGradient">如果指定最后渐变项，在进行该渐变前将终止最后渐变项的线程。</param>
        private Gradient ShowItemActivatedColorEffect(int index,int time,Color beginColor,uint delay,Gradient lastGradient)
        {
            if(index >=0 && hotKeyListView.Items.Count > index)
            {
                ListViewItem currentItem = hotKeyListView.Items[index];
                Gradient colorGradient = new Gradient(hotKeyListView.Items[index], "BackColor", Gradient.ObjectMemberType.Property, this);
                colorGradient.GradientBegin += new Gradient.GradientBeginCallBack(() => {
                    if (lastGradient != null)
                    {
                        lastGradient.stop_flag = true;
                    }
                    currentItem.BackColor = beginColor; 
                });
                colorGradient.Begin(hotKeyListView.BackColor, time,delay);

                return colorGradient;
            }
            return null;
        }

        /// <summary>
        /// 使项目背景颜色从指定颜色渐变到初始颜色。
        /// </summary>
        /// <param name="index">热键项索引。</param>
        /// <param name="time">渐变时间。</param>
        /// <param name="beginColor">渐变开始前设置的颜色。</param>
        /// <param name="delay">延迟执行。</param>
        private Gradient ShowItemActivatedColorEffect(int index, int time, Color beginColor, uint delay)
        {
            return ShowItemActivatedColorEffect(index, time, beginColor, delay,null);
        }

        /// <summary>
        /// 显示热键触发的通知。若设置未启用该项，通知不会被显示。
        /// </summary>
        /// <param name="currentInfo"></param>
        private void ShowEventFiredNotify(HotKeyInfo currentInfo)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(currentInfo.DelayTime);
                if (programSettings.Bases.Notifys.ShowFiredNotify && !eventFiredNotifyTimeValidateItem.CheckAndUpdate(programSettings.Bases.Notifys.NotifyMinSpan))
                {
                    hotKeySetterNotifyIcon.ShowBalloonTip(1000, "热键“" + currentInfo.Name + "”现在被触发", "参数：" + currentInfo.EventArgs, ToolTipIcon.Info);
                }
            }));
            thread.Start();
        }
        private void toolStrip_HideMainForm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            int filecommand_index = commandLine.ToList().IndexOf("-file");
            if (filecommand_index + 1 < commandLine.Length && filecommand_index >= 0)
            {
                string filename = "";
                filename = commandLine[filecommand_index + 1];
                if (File.Exists(filename))
                {
                    ReadAndAddHotKeyFromConfig(filename);
                    if (programSettings.Bases.ShortCuts.LoadFileHide)
                        Hide();
                }
                else
                {
                    statusBarTextBox.Text = "启动参数指定的“" + filename + "”不存在。";
                }
            }
            else
            {
                ShowInTaskbar = true;
                //窗口创建动画
                if (programSettings.Bases.Animations.WindowCreatedAnimation)
                    PerformLoadAnimation();

            }
            SetHomeShowHideHotKeyEnabledStatus(programSettings.Bases.ShortCuts.HOMEShowHide);

        }
        private void SetHomeShowHideHotKeyEnabledStatus(bool enable)
        {
            try
            {
                HotKey.UnRegisterHotKey(Handle, -1);
            }
            catch (Exception) { Debug.WriteLine("HOME键注销失败。"); }
            try
            {
          
                if (enable)
                {
                    HotKey home = new HotKey(Handle, -1, HotKey.HotKeyfsModifiers.INVALID, 36);
                    home.Register();
                }
            }
            catch (Exception) { Debug.WriteLine("HOME键注册失败。"); }
        }

        private void toolStrip_Help_AdminBoot_Click(object sender, EventArgs e)
        {
            string commandline_source = "";
            for (int i = 0; i < commandLine.Length; i++)
            {
                commandline_source += commandLine[i];
                commandline_source += " ";
            }
            commandline_source += "-IgnoreMutex";
            ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath, commandline_source);
            info.Verb = "RunAs";

            bool flag = true;
            try
            {
                Process proc = Process.Start(info);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提权失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }
            if (flag)
                Application.Exit();
            else
            {
                Activate();
            }
        }



        private void SaveHotKeyConfigs(string filePath)
        {
            for(int i = 0;i<hotKeyInfos.Count;i++)
            {
                string config = "HotKeyRecord" + i.ToString();
                HotKeyInfo currentInfo = hotKeyInfos[i];
                ConfigFile.WriteStatic(filePath, config, "ID", currentInfo.ID.ToString());
                ConfigFile.WriteStatic(filePath, config, "Count", currentInfo.Count.ToString());
                ConfigFile.WriteStatic(filePath, config, "Name", currentInfo.Name);
                ConfigFile.WriteStatic(filePath, config, "FirstKeyCode", currentInfo.FirstKeyCode.ToString());
                ConfigFile.WriteStatic(filePath, config, "SecondKeyCode", currentInfo.SecondKeyCode.ToString());
                ConfigFile.WriteStatic(filePath, config, "EventID", currentInfo.EventID.ToString());
                ConfigFile.WriteStatic(filePath, config, "EventArgs", currentInfo.EventArgs.ToString());
                ConfigFile.WriteStatic(filePath, config, "DelayTime", currentInfo.DelayTime.ToString());
                ConfigFile.WriteStatic(filePath, config, "MinSpan", currentInfo.MinSpan.ToString());
            }
        }

        private List<HotKeyInfo> ReadHotKeyConfigs(string filePath)
        {
            List<HotKeyInfo> ret = new List<HotKeyInfo>();
            for (int i = 0; ; i++)
            {
                string config = "HotKeyRecord" + i.ToString();
                string EventArgs, Name, EventID;
                int ID, Count, DelayTime, MinSpan;
                uint  FirstKeyCode, SecondKeyCode;
                try
                {
                    ID = int.Parse(ConfigFile.ReadFromFile(filePath, config, "ID"));
                    Count = int.Parse(ConfigFile.ReadFromFile(filePath, config, "Count"));
                    FirstKeyCode = uint.Parse(ConfigFile.ReadFromFile(filePath, config, "FirstKeyCode"));
                    SecondKeyCode = uint.Parse(ConfigFile.ReadFromFile(filePath, config, "SecondKeyCode"));
                    DelayTime = int.Parse(ConfigFile.ReadFromFile(filePath, config, "DelayTime"));
                    MinSpan = int.Parse(ConfigFile.ReadFromFile(filePath, config, "MinSpan"));
                    EventArgs = ConfigFile.ReadFromFile(filePath, config, "EventArgs");
                    Name = ConfigFile.ReadFromFile(filePath, config, "Name");
                    EventID = ConfigFile.ReadFromFile(filePath, config, "EventID");
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode != 2)
                        throw ex;
                    else Debug.WriteLine("读取结束，共读取了{0}个项。", i);

                    break;
                }
                ret.Add(new HotKeyInfo(Name, Count, ID, FirstKeyCode, SecondKeyCode, DelayTime, MinSpan, EventArgs, EventID));
            }
            return ret;
        }


        /// <summary>
        /// 添加热键。
        /// </summary>
        /// <param name="info">热键信息。</param>
        /// <returns>一个指示了注册是否成功的数据列表。</returns>
        private HotKey.HotKeyRegisterFlag AddHotKey(HotKeyInfo info)
        {
            HotKey.HotKeyRegisterFlag flag = new HotKey.HotKeyRegisterFlag();
            HotKey hotKey = new HotKey(Handle, info.ID, (HotKey.HotKeyfsModifiers)info.SecondKeyCode, info.FirstKeyCode);
            try
            {
                flag.SuccessFlag = hotKey.Register();
            }
            catch (Win32Exception ex)
            {
                flag.ErrorCode = ex.NativeErrorCode;
            }
            return flag;
        }

        private void UpdateSettings()
        {
            InitEvents();
            programSettings.ReFresh();
            PerformResizeAnimation();
            UpdateHookRegistry();

            //更新HOME呼出设置
            SetHomeShowHideHotKeyEnabledStatus(programSettings.Bases.ShortCuts.HOMEShowHide);
        }
        private void toolStrip_Help_Settings_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm() { Tag = this };
            settings.SettingsUpdateRequired += UpdateSettings;
            settings.ShowDialog();
        }

        private void toolStrip_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStrip_Help_About_Click(object sender, EventArgs e)
        {
            new AboutForm() { Tag = this }.ShowDialog();
        }

        private void button_AddHotKey_Click(object sender, EventArgs e)
        {
            HotKeyInfo info_temp = new HotKeyInfo(textBox_HotKeyName.Text, 0, GetHotKeyID(), GetHotKeyFirstKey(), (uint)GetHotKeySecondKey(), GetDelayTime(), GetMinInterval(), textBox_Para.Text, GetHotKeyEventType());
            if (AddHotKey(info_temp).SuccessFlag)
            {
                hotKeyInfos.Add(info_temp);
            }
            UpdateHotKeyListView();
        }

        /// <summary>
        /// 清除热键列表并从hotKeyInfos更新热键列表的数据。提示：后续开发要尽量确保这个方法是创建Item的唯一方法。
        /// </summary>
        private void UpdateHotKeyListView()
        {
            hotKeyListView.Items.Clear();
            for (int i = 0; i < hotKeyInfos.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //根据要显示的列数一次性添加足够的Subitem。
                for (int j = 0; j < columnStatus.DisplayedCount - 1; j++)
                {
                    lvi.SubItems.Add("");
                }
                lvi.SubItems[columnStatus.NameIndex].Text = hotKeyInfos[i].Name;
                lvi.SubItems[columnStatus.CountIndex].Text = hotKeyInfos[i].Count.ToString();
                lvi.SubItems[columnStatus.MinSpanIndex].Text = hotKeyInfos[i].MinSpan.ToString();

                if (hotKeyInfos[i].SecondKeyCode != 0)
                {
                    lvi.SubItems[columnStatus.KeyNameIndex].Text = HotKey.GetfsModifiersKeyString(hotKeyInfos[i].SecondKeyCode) + " + " + HotKey.GetKeyNameByCode(hotKeyInfos[i].FirstKeyCode);
                }
                else
                {
                    lvi.SubItems[columnStatus.KeyNameIndex].Text = HotKey.GetKeyNameByCode(hotKeyInfos[i].FirstKeyCode);
                }
                lvi.SubItems[columnStatus.DelayIndex].Text = hotKeyInfos[i].DelayTime.ToString();
                lvi.SubItems[columnStatus.FirstKeyCodeIndex].Text = hotKeyInfos[i].FirstKeyCode.ToString();
                lvi.SubItems[columnStatus.SecondKeyCodeIndex].Text = hotKeyInfos[i].SecondKeyCode.ToString();
                lvi.SubItems[columnStatus.EventTypeIndex].Text = eventTable.Rows[FindEvent(hotKeyInfos[i].EventID)]["title"].ToString(); //3即获取index In eventTable
                lvi.SubItems[columnStatus.ParamIndex].Text = hotKeyInfos[i].EventArgs;
                hotKeyListView.Items.Add(lvi);

                if (timeValidate.Find(hotKeyInfos[i].ID) == -1) 
                timeValidate.Items.Add(new TimeValidateItem(hotKeyInfos[i].ID, DateTime.Now - TimeSpan.FromMilliseconds(hotKeyInfos[i].MinSpan))); //注册事件验证项，减去其最小间隔是为了使其首次立即可用。
            }
        }

        /// <summary>
        /// 从hotKeyInfos更新指定项的数据。当无需更改项数，只需更新数据时使用此方法。
        /// </summary>
        /// <param name="index">项索引。</param>
        private void FreshHotKeyItem(int index)
        {
            ListViewItem curItem = hotKeyListView.Items[index];
            curItem.SubItems[columnStatus.NameIndex].Text = hotKeyInfos[index].Name;
            curItem.SubItems[columnStatus.CountIndex].Text = hotKeyInfos[index].Count.ToString();
            curItem.SubItems[columnStatus.MinSpanIndex].Text = hotKeyInfos[index].MinSpan.ToString();

            if (hotKeyInfos[index].SecondKeyCode != 0)
            {
                curItem.SubItems[columnStatus.KeyNameIndex].Text = HotKey.GetfsModifiersKeyString(hotKeyInfos[index].SecondKeyCode) + " + " + HotKey.GetKeyNameByCode(hotKeyInfos[index].FirstKeyCode);
            }
            else
            {
                curItem.SubItems[columnStatus.KeyNameIndex].Text = HotKey.GetKeyNameByCode(hotKeyInfos[index].FirstKeyCode);
            }

            curItem.SubItems[columnStatus.DelayIndex].Text = hotKeyInfos[index].DelayTime.ToString();
            curItem.SubItems[columnStatus.FirstKeyCodeIndex].Text = hotKeyInfos[index].FirstKeyCode.ToString();
            curItem.SubItems[columnStatus.SecondKeyCodeIndex].Text = hotKeyInfos[index].SecondKeyCode.ToString();
            curItem.SubItems[columnStatus.EventTypeIndex].Text = eventTable.Rows[FindEvent(hotKeyInfos[index].EventID)]["title"].ToString(); //3即获取indexIneventTable
            curItem.SubItems[columnStatus.ParamIndex].Text = hotKeyInfos[index].EventArgs;
        }

        /// <summary>
        /// 从hotKeyInfos更新指定项的数据。当无需更改项数，只需更新数据时使用此方法。
        /// </summary>
        /// <param name="sIndex">起始索引。</param>
        /// <param name="eIndex">终止索引。</param>
        private void FreshHotKeyItem(int sIndex, int eIndex)
        {
            if (sIndex <= eIndex && eIndex < hotKeyListView.Items.Count)
            {
                for (int i = sIndex; i <= eIndex; i++)
                {
                    FreshHotKeyItem(i);
                }
            }
        }

        private HotKey.HotKeyRegisterFlags AddHotKeyFromListT(List<HotKeyInfo> infoList)
        {
            HotKey.HotKeyRegisterFlags flags = new HotKey.HotKeyRegisterFlags();
            for (int i = 0; i < infoList.Count; i++)
            {
                HotKeyInfo currentInfo = infoList[i];
                HotKey.HotKeyRegisterFlag flag = new HotKey.HotKeyRegisterFlag();
                try
                {
                    flag.SuccessFlag = new HotKey(this.Handle, currentInfo.ID, currentInfo.SecondKeyCode, currentInfo.FirstKeyCode).Register();
                    hotKeyInfos.Add(currentInfo);
                }
                catch (Win32Exception ex)
                {
                    flag.SuccessFlag = false;
                    flag.ErrorCode = ex.NativeErrorCode;
                }
                flags.AddFlag(flag);
            }
            return flags;
        }

        private void button_DeleteHotKey_Click(object sender, EventArgs e)
        {
            if (SelectedCount > 0)
            {
                DeleteHotKey(hotKeyListView.SelectedItems[0].Index, hotKeyListView.SelectedItems[SelectedCount - 1].Index);
            }
        }

        /// <summary>
        /// 删除并注销指定索引范围的热键。
        /// </summary>
        /// <param name="sIndex">起始索引。</param>
        /// <param name="eIndex">终止索引。</param>
        private void DeleteHotKey(int sIndex, int eIndex)
        {
            if (eIndex >= sIndex && eIndex < hotKeyListView.Items.Count)
            {
                for (int i = eIndex; i >= sIndex; i--)
                {
                    try
                    {
                        HotKey.UnRegisterHotKey(this.Handle, hotKeyInfos[i].ID);
                        timeValidate.RemoveItem(hotKeyInfos[i].ID); //删除时间验证项。
                        hotKeyInfos.RemoveAt(i);
                        UpdateHotKeyListView();
                    }
                    catch (Win32Exception ex)
                    {
                        ErrorMessage.ShowWin32ErrorMessageBox(ex, "这可能是因为热键注册信息已通过其他手段被修改。");
                    }
                }
            }
        }

        private void button_SaveConfig_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "热键配置记录文件|*.hkey|所有文件|*.*"; //筛选器
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveHotKeyConfigs(dialog.FileName);
            }
        }

        private void toolStrip_File_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog() { Filter = "热键配置记录文件|*.hkey|所有文件|*.*" };
            if (dialog.ShowDialog() == DialogResult.OK && File.Exists(dialog.FileName))
            {
                ReadAndAddHotKeyFromConfig(dialog.FileName);
            }
        }

        /// <summary>
        /// 从配置文件读取并添加热键。
        /// </summary>
        /// <param name="filePath">配置文件路径。</param>
        /// <returns>一个指示注册热键错误信息的列表。</returns>
        private HotKey.HotKeyRegisterFlags ReadAndAddHotKeyFromConfig(string filePath)
        {
            List<HotKeyInfo> infos = ReadHotKeyConfigs(filePath);
            DeleteHotKey(0, hotKeyListView.Items.Count - 1);
            HotKey.HotKeyRegisterFlags flags = AddHotKeyFromListT(infos);
            if (flags.SuccessCount > 0 && !FileHistory.TryGetOpenFileHistoryArray().Contains(filePath))
            {
                FileHistory.TrySaveOpenFileHistory(filePath);
            }
            UpdateHotKeyListView();

            //显示热键注册结果。
            ShowStatusMessage("读取了 " + flags.TriedHotKeyCount.ToString() + " 个热键配置：" + flags.SuccessCount.ToString() + " 个注册成功，" + flags.FailedCount.ToString() + " 个注册失败。", Color.Blue);

            return flags;
        }
        private void toolStrip_File_SaveTo_Click(object sender, EventArgs e)
        {
            button_SaveConfig_Click(null, null);
        }

        private void toolStrip_File_DropDownOpening(object sender, EventArgs e)
        {
            toolStrip_HideMainForm.Text = "隐藏本窗口";
            if (programSettings.Bases.ShortCuts.HOMEShowHide)
                toolStrip_HideMainForm.Text += "(HOME键呼出)";

            toolStrip_File_Recents.DropDownItems.Clear();
            string[] historyArray = FileHistory.TryGetOpenFileHistoryArray();
            if (historyArray.Length > 0)
            {
                for (int i = 0; i < historyArray.Length; i++)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(historyArray[i]) { Enabled = File.Exists(historyArray[i])}; //显示失效的项，但禁止点击。
                    item.Click += HistoryItem_Click;//注册点击事件。
                    toolStrip_File_Recents.DropDownItems.Add(item);
                }
                ToolStripMenuItem item_ClearHistory = new ToolStripMenuItem("清除历史记录");
                item_ClearHistory.Click += ClearFileReadHistory; //注册清除记录事件
                toolStrip_File_Recents.DropDownItems.Add(item_ClearHistory);
            }
        }

        /// <summary>
        /// 在底部状态栏显示消息。
        /// </summary>
        /// <param name="msg">消息字符串。</param>
        /// <param name="textColor">文本颜色。</param>
        private void ShowStatusMessage(string msg,Color textColor)
        {
            statusBarTextBox.Text = msg;
            statusBarTextBox.ForeColor = textColor;
        }

        private void ClearFileReadHistory(object sender, EventArgs e)
        {
            FileHistory.DeleteOpenFileHistory();
        }
        private void HistoryItem_Click(object sender, EventArgs e)
        {
            string filePath = (sender as ToolStripMenuItem).Text;
            if (File.Exists(filePath))
            {
                ReadAndAddHotKeyFromConfig(filePath);
            }
        }

        private void button_Disable_Click(object sender, EventArgs e)
        {
            if(SelectedCount >0)
            {
                hotKeyListView.ItemCheck -= hotKeyListView_ItemCheck;
                for (int i = 0;i<SelectedCount;i++)
                {
                    int index = i + hotKeyListView.SelectedItems[0].Index;
                    hotKeyListView.Items[index].Checked = true;             //禁用热键即使其Checked为true。
                }
                hotKeyListView.ItemCheck += hotKeyListView_ItemCheck;
            }
        }

        private void button_Enable_Click(object sender, EventArgs e)
        {
            if (SelectedCount > 0)
            {
                hotKeyListView.ItemCheck -= hotKeyListView_ItemCheck;        //用于解决ListView多选时自动Uncheck的BUG。
                for (int i = 0; i < SelectedCount; i++)
                {
                    int index = i + hotKeyListView.SelectedItems[0].Index;
                    hotKeyListView.Items[index].Checked = false;             //启用热键即使其Checked为false。
                }
                hotKeyListView.ItemCheck += hotKeyListView_ItemCheck;
            }
        }

        private void button_EditParam_Click(object sender, EventArgs e)
        {
            if (SelectedCount > 0)
            {
                int index = hotKeyListView.SelectedIndices[0];
                EditForm editForm = new EditForm(hotKeyInfos[index],index, eventTable) { Tag = this };
                editForm.EditContentSaveRequired += EditForm_EditContentSaveRequired; //注册更改事件。
                editForm.ShowDialog();
            }
        }

        /// <summary>
        /// [Bug May Occurs]编辑窗口请求保存热键参数更改。
        /// </summary>
        /// <param name="KeyCodeChanged"></param>
        /// <param name="info"></param>
        private void EditForm_EditContentSaveRequired(bool KeyCodeChanged, HotKeyInfo info,int index)
        {
            if (KeyCodeChanged)
            { 
                HotKey.UnRegisterHotKey(Handle, hotKeyInfos[hotKeyListView.SelectedIndices[0]].ID);
                new HotKey(Handle, info.ID, info.SecondKeyCode, info.FirstKeyCode).Register();
            }
            hotKeyInfos[hotKeyListView.SelectedIndices[0]] = info;
            FreshHotKeyItem(index);
        }

        private void button_VerifyHotKey_Click(object sender, EventArgs e)
        {
            int errCode = HotKey.IsHotKeyAvailable(Handle, GetHotKeyID(), GetHotKeySecondKey(), GetHotKeyFirstKey());
            if (errCode == 0)
                ShowStatusMessage("热键可用。", Color.Green);
            else ShowStatusMessage("热键不可用：" + new Win32Exception(errCode).Message, Color.Red);
        }

        private void hotKeyListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            e.NewValue = e.CurrentValue;
        }

        private void PerformPageSwitchUnderLineAnimation()
        {
            int time;
            uint delay;
            if(programSettings.Bases.Animations.PageSwitchAnimation)
            {
                time = 300;
                delay = 140;
            }
            else
            {
                time = 0;
                delay = 0;
            }
            if(pageIndex == 1) //从执行切换到新建
            {
                new Gradient(switchButtonUnderLine, "Width", Gradient.ObjectMemberType.Property, this).BeginBezier(Width, time, ControlAnimation.BezierAnimationInPoints);
                new Gradient(switchButtonUnderLine, "Left", Gradient.ObjectMemberType.Property, this).BeginBezier(button_SwitchNew.Left, time, ControlAnimation.BezierAnimationOutPoints, delay);
            }
            if(pageIndex == 0)
            {
                new Gradient(switchButtonUnderLine, "Width", Gradient.ObjectMemberType.Property, this).BeginBezier(button_SwitchRun.Width -4, time, ControlAnimation.BezierAnimationOutPoints, delay);
                new Gradient(switchButtonUnderLine, "Left", Gradient.ObjectMemberType.Property, this).BeginBezier(button_SwitchRun.Left, time, ControlAnimation.BezierAnimationInPoints);
            }
        }
        private void button_SwitchNew_Click(object sender, EventArgs e)
        {
            bool animationEnabled = programSettings.Bases.Animations.PageSwitchAnimation;
            if ((!pageSwitchTimeValidateItem.Check(700) || !animationEnabled) && pageIndex != newPageIndex) //即距离上次点击至少间隔700毫秒。
            {
                //初始化动画数据
                pageIndex = newPageIndex;
                int animationLength;
                uint fadeInDelay;
                if (animationEnabled)
                {
                    fadeInDelay = 300;
                    animationLength = 400;
                }
                else
                {
                    fadeInDelay = 0;
                    animationLength = 0;
                }

                //执行动画
                new ControlAnimation(100, animationLength).FadeOut(panel_Run, ControlAnimation.FadeMethod.FromRightToLeft);
                new ControlAnimation(100, animationLength).FadeIn(panel_New, ControlAnimation.FadeMethod.FromRightToLeft, fadeInDelay);
                PerformPageSwitchUnderLineAnimation();

                pageSwitchTimeValidateItem.UpdateRecordTime();
                panel_New.Refresh();
            }
        }

        private void button_SwitchRun_Click(object sender, EventArgs e)
        {
            bool animationEnabled = programSettings.Bases.Animations.PageSwitchAnimation;
            if ((!pageSwitchTimeValidateItem.Check(700) || !animationEnabled) && pageIndex != runPageIndex)
            { 
                //初始化数据
                pageIndex = runPageIndex;
                int animationLength;
                uint fadeInDelay;
                if (animationEnabled)
                {
                    fadeInDelay = 300;
                    animationLength = 400;
                }
                else
                {
                    fadeInDelay = 0;
                    animationLength = 0;
                }

                new ControlAnimation(100, animationLength).FadeOut(panel_New, ControlAnimation.FadeMethod.FromLeftToRight);
                new ControlAnimation(100, animationLength).FadeIn(panel_Run, ControlAnimation.FadeMethod.FromLeftToRight, fadeInDelay);
                PerformPageSwitchUnderLineAnimation();

                pageSwitchTimeValidateItem.UpdateRecordTime();
                panel_Run.Refresh();
            }
        }
        private void button_CleanCount_Click(object sender, EventArgs e)
        {
            if (SelectedCount > 0)
            {
                int start_index = hotKeyListView.SelectedItems[0].Index;
                int end_index = hotKeyListView.SelectedItems[SelectedCount - 1].Index;
                for (int i = start_index; i <= end_index; i++)
                {
                    hotKeyInfos[i].Count = 0;
                }
                FreshHotKeyItem(start_index, end_index);
            }
        }

        private void panel_SwitchPageButton_Resize(object sender, EventArgs e)
        {
            button_SwitchNew.Width = Width / 2;
            button_SwitchRun.Width = Width / 2;
            Button currentButton;
            if (pageIndex == 0)
                currentButton = button_SwitchRun;
            else currentButton = button_SwitchNew;
            switchButtonUnderLine.Left = currentButton.Left;
            switchButtonUnderLine.Width = currentButton.Width - 4;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            PerformResizeAnimation();
        }

        /// <summary>
        /// 根据是否应使用缩放动画更新Anchor。
        /// </summary>
        private void UpdateAnchors()
        {
            if (useResizeAnimation && programSettings.Bases.Animations.ResizeAnimation)
            {
                panel_All.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
            else
            {
                panel_All.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }
        private void PerformResizeAnimation()
        {
            if (useResizeAnimation && programSettings.Bases.Animations.ResizeAnimation) //若指示应使用动画且启用了动画。这个指示值在DefWndProc中设置。
            {
                new Gradient(panel_All, "Width", Gradient.ObjectMemberType.Property, this).BeginBezier(Width - 16 * DpiScale.ScaleValue, 400, ControlAnimation.BezierAnimationInPoints);
                new Gradient(panel_All, "Height", Gradient.ObjectMemberType.Property, this).BeginBezier(Height - 92 * DpiScale.ScaleValue, 400, ControlAnimation.BezierAnimationInPoints);
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                // Check your window state here
                if ((m.WParam.ToInt32() & 0xFFF0) == 0xF030 || ((m.WParam.ToInt32() & 0xFFF0) == 0xF120 && WindowState == FormWindowState.Maximized)) //窗口正在被最大化或从最大化还原
                {
                    // THe window is being maximized
                    useResizeAnimation = !programSettings.Bases.Animations.OneWayResizeAnimationOnly;
                    UpdateAnchors();
                   new Thread(new ThreadStart(() => { Thread.Sleep(10); PerformResizeAnimation(); })).Start(); ;
                }
            }
            base.WndProc(ref m);
        }

        private void Panels_Resize_Refresh(object sender,EventArgs e)
        {
            if(programSettings.Bases.Animations.ResizeRefresh)
            (sender as Control)?.Refresh();
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            UpdateAnchors();
        }

        /// <summary>
        /// 设置控件的重绘模式。
        /// </summary>
        /// <param name="handle">控件句柄。</param>
        /// <param name="enable">启用。</param>
        private void SetRedrawMode(IntPtr handle, bool enable)
        {
            if (enable)
            {
                Win32UIApi.SendMessageA(handle, 11, 1, 0);
            }
            else
            {
                Win32UIApi.SendMessageA(handle, 11, 0, 0);
            }
        }

        private void menu_HotKeyItem_FireEvent_Click(object sender, EventArgs e)
        {
            for (int i = hotKeyListView.SelectedItems[0].Index;i<=hotKeyListView.SelectedItems[SelectedCount - 1].Index;i++)
            {
                DoEvent(i);
            }
        }

        /// <summary>
        ///  右键项目弹出菜单。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hotKeyListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem lvi = hotKeyListView.GetItemAt(e.X, e.Y);
                if (lvi != null)
                {
                    Point point = PointToClient(hotKeyListView.PointToScreen(new Point(e.X, e.Y)));
                    menu_HotKeyItem.Show(this,point);
                }
            }
        }

        private void menu_HotKeyItem_Opening(object sender, CancelEventArgs e)
        {
            menu_HotKeyItem_FireEvent.Text = "触发 " + SelectedCount.ToString() + "  个事件(&F)";
        }

        private void comboBox_Events_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_EventDes.Text = eventTable.Rows[comboBox_Events.SelectedIndex]["detail"].ToString();
        }


        /// <summary>
        /// 保存MainForm的Size属性。
        /// </summary>
        private void SaveWindowSizeConfig()
        {
            ConfigFile.WriteStatic(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "MainFormWidth", Width.ToString());
            ConfigFile.WriteStatic(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "MainFormHeight", Height.ToString());
        }

        /// <summary>
        /// 读取并应用MainForm的Size属性。
        /// </summary>
        private void ReadAndApplyWindowSizeConfig()
        {
            try
            {
                Width = int.Parse(ConfigFile.ReadFromFile(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "MainFormWidth"));
                Height = int.Parse(ConfigFile.ReadFromFile(ConfigFile.HotKeySetterConfigDirectory + @"\records.ini", "WindowSize", "MainFormHeight"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReadAndApplyWindowSizeConfig ->" + ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowSizeConfig();
        }

        private void menu_HotKeyItem_Enable_Click(object sender, EventArgs e)
        {
            button_Enable_Click(null, null);
        }

        private void menu_HotKeyItem_Disable_Click(object sender, EventArgs e)
        {
            button_Disable_Click(null, null);
        }
    }
}