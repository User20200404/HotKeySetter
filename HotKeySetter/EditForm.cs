using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    public partial class EditForm : Form
    {
        //这里声明了事件委托。
        public delegate void EditContentSaveRequiredCallBack(bool KeyCodeChanged,HotKeyInfo info,int indexInList);
        /// <summary>
        /// 编辑的热键信息需要保存时触发。
        /// </summary>
        public event EditContentSaveRequiredCallBack EditContentSaveRequired;

        HotKeyInfo info;
        DataTable eventTable;
        Settings programSettings;
        int indexInList;
        public EditForm(HotKeyInfo info,int indexInList,DataTable eventTable)
        {
            this.info = info;
            this.eventTable = eventTable;
            this.indexInList = indexInList;
            InitializeComponent();
        }

        /// <summary>
        /// 将HotKeyInfo显示在编辑控件中。
        /// </summary>
        private void InitHotKeyInfoDisplay()
        {
            textBox_Name.Text = info.Name;
            textBox_Args.Text = info.EventArgs;
            textBox_MinSpan.Text = info.MinSpan.ToString();
            textBox_DelayTime.Text = info.DelayTime.ToString();
            textBox_FirstKey.Text = ((Keys)info.FirstKeyCode).ToString();
            textBox_KeyCode.Text = info.FirstKeyCode.ToString();
            comboBox_SecondKey.Text = HotKey.GetfsModifiersKeyString(info.SecondKeyCode);

            comboBox_Events.DataSource = eventTable;
            comboBox_Events.DisplayMember = "title";
            comboBox_Events.ValueMember = "eid";
            comboBox_Events.SelectedValue = info.EventID;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Control parent = Tag as Control;
            if (parent != null)
            {
                Left = parent.Left;
                Top = parent.Top;
            }
            InitHotKeyInfoDisplay();
            textBox_KeyCode.TextChanged += textBox_KeyCode_TextChanged;

            //初始化设置
            programSettings = new Settings();
        }

        /// <summary>
        /// 激活加载动画。
        /// </summary>
        private void PerformLoadAnimation()
        {
            groupBox_Param.Hide();
            panel_OP.Hide();
            new ControlAnimation(200, 500) {FadeBeginEventHandlerToAdd = LoadAnimationCallBack}.FadeIn(groupBox_Param, ControlAnimation.FadeMethod.FromUpToDown);
            new ControlAnimation(panel_OP.Height, 500).FadeIn(panel_OP, ControlAnimation.FadeMethod.FromDownToUp, 350);
        }

        private void LoadAnimationCallBack(Control sender)
        {
            groupBox_Param.Show();
            panel_OP.Show();
        }

        /// <summary>
        /// 更改键代码时显示警告。
        /// </summary>
        private void CheckAndShowKeyCodeChangedStatus()
        {
            if (IsKeyCodeChanged())
                label_KeyCodeChangeWarning.Show();
            else label_KeyCodeChangeWarning.Hide();
        }

        /// <summary>
        /// 判断键代码是否已更改。
        /// </summary>
        /// <returns>键代码被更改返回true，否则返回false。</returns>
        private bool IsKeyCodeChanged()
        {
            return textBox_KeyCode.Text != info.FirstKeyCode.ToString() || HotKey.GetSecondKeyCodeByString(comboBox_SecondKey.Text) != info.SecondKeyCode;
        }

        private void textBox_FirstKey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            textBox_KeyCode.Text = ((int)e.KeyCode).ToString();
            textBox_FirstKey.Text = e.KeyCode.ToString();
            CheckAndShowKeyCodeChangedStatus();
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

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox_SecondKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndShowKeyCodeChangedStatus();
        }
        private HotKeyInfo GetNewInfo()
        {
            HotKeyInfo newInfo = new HotKeyInfo(
                textBox_Name.Text,
                info.Count,
                info.ID,
                uint.Parse(textBox_KeyCode.Text),
                HotKey.GetSecondKeyCodeByString(comboBox_SecondKey.Text),
                int.Parse(textBox_DelayTime.Text),
                int.Parse(textBox_MinSpan.Text),
                textBox_Args.Text, comboBox_Events.SelectedValue.ToString());
            return newInfo;
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            HotKeyInfo newInfo = GetNewInfo();
            bool keyCodeChangedFlag = IsKeyCodeChanged();
            int keyAvailabilityFlag = HotKey.IsHotKeyAvailable(Handle, newInfo.ID, newInfo.SecondKeyCode, newInfo.FirstKeyCode);
            if (!keyCodeChangedFlag || keyAvailabilityFlag == 0) //即 键代码未改变 或 改变后的热键可以注册。
            {
                TrySaveChanges(keyCodeChangedFlag, newInfo);
                Close();
            }
            else MessageBox.Show("当前更改键代码后的热键不可用。\n\n详细信息：" + new Win32Exception(keyAvailabilityFlag).Message, "重新注册热键失败。", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 激活保存事件，让MainForm保存编辑内容。
        /// </summary>
        private void TrySaveChanges(bool keyCodeChanged,HotKeyInfo newInfo)
        {
            EditContentSaveRequired?.Invoke(keyCodeChanged, newInfo,indexInList);
        }

        private void EditForm_Shown(object sender, EventArgs e)
        {
            if (programSettings.Bases.Animations.WindowCreatedAnimation)
                PerformLoadAnimation();
        }
    }
}
