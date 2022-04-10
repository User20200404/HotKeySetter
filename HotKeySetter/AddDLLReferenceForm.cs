using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    public partial class AddDLLReferenceForm : Form
    {
        DynamicEventInfo info;
        TimeValidateItem tvi = new TimeValidateItem("OpenRefDir",DateTime.Now - TimeSpan.FromMilliseconds(500d)); //时间验证，防止连点多开文件夹。
        private Settings programSettings;

        public delegate void EnumResultAddRequiredCallBack(DynamicEventInfo infoData);
        /// <summary>
        /// 在枚举成功且用户点击“添加”按钮时触发。
        /// </summary>
        public event EnumResultAddRequiredCallBack EnumResultAddRequired;
        public AddDLLReferenceForm()
        {
            programSettings = new Settings();
            InitializeComponent();
        }

        private void PathControls_Activated(object sender, EventArgs e)
        {
            helpStatusLabel.Text = "在此输入动态链接库的绝对路径。";
        }

        private void DescriptionControls_Activated(object sender, EventArgs e)
        {
            helpStatusLabel.Text = "描述函数提供了动态链接库中委托事件的信息。HotKeySetter将以枚举的形式首先调用此函数。";
        }

        private void DLLLibInfoControls_Activated(object sender, EventArgs e)
        {
            helpStatusLabel.Text = "这里显示了该动态链接库提供的事件信息。";
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (File.Exists(dialog.FileName))
            {
                textBox_DllPath.Text = dialog.FileName;
                button_Validate_Click(null, null);
            }
        }

        private void button_Validate_Click(object sender, EventArgs e)
        {
            info = new DynamicEventInfo(textBox_DllPath.Text, textBox_Entrypoint.Text);
            listView_DllInfo.Items.Clear();
            for(int i = 0;i<info.MainFuncEntryPoint.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(i.ToString());
                lvi.SubItems.Add(info.DescribeFuncEntryPoint[i]);
                lvi.SubItems.Add(info.MainFuncEntryPoint[i]);
                lvi.SubItems.Add(info.ErrorProviderFuncEntryPoint[i]);
                listView_DllInfo.Items.Add(lvi);
                button_Add.Enabled = true;
            }
            if (info.MainFuncEntryPoint.Count == 0)
            {
                MessageBox.Show("未能获取到事件，请检查" + textBox_DllPath.Text + "是否定义了热键事件。");
                button_Add.Enabled = false;
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (info?.EventEnumStatus == DynamicEventInfo.DynamicEventEnumStatus.SUCCESS)
            {
                string fileName = StringOperation.GetFileNameFromPath(textBox_DllPath.Text);
                string newFilePath = ConfigFile.HotKeySetterReferenceDir + @"\" + fileName;

                DialogResult result = DialogResult.No;
                bool fileExist = false;
                fileExist = File.Exists(newFilePath);

                if(fileExist)
                result = MessageBox.Show("引用文件需要复制到引用目录，因此不允许添加同名引用。\n\n是否覆盖引用文件？", "引用覆盖", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (!fileExist || result == DialogResult.Yes)
                {
                    Directory.CreateDirectory(ConfigFile.HotKeySetterReferenceDir);
                    File.Copy(textBox_DllPath.Text, newFilePath, true); //将引用文件复制到引用目录。
                    EnumResultAddRequired?.Invoke(new DynamicEventInfo(newFilePath, info.EnumFuncEntryPoint));
                    Close();
                }
            }
        }

        private void AddDLLReferenceForm_Load(object sender, EventArgs e)
        {
            Control settingsForm = Tag as Control;
            if(settingsForm != null)
            {
                Left = settingsForm.Left;
                Top = settingsForm.Top;
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label_CopyTip_Click(object sender, EventArgs e)
        {
            if(!tvi.CheckAndUpdate(500))
            Process.Start("explorer.exe", ConfigFile.HotKeySetterReferenceDir);
        }

        private void LoadAnimationCallBack(Control sender)
        {
            panel_All.Show();
            panel_Buttons.Refresh();//刷新防止渲染错误。
        }

        private void PerformLoadAnimation()
        {
             panel_All.Hide();
            new ControlAnimation(panel_Buttons.Height, 450).FadeIn(panel_Buttons, ControlAnimation.FadeMethod.FromDownToUp, 500);
            new ControlAnimation(groupBox_Edit.Height + groupBox_Edit.Top, 450) { FadeBeginEventHandlerToAdd = LoadAnimationCallBack }.FadeIn(groupBox_Edit, ControlAnimation.FadeMethod.FromUpToDown, 0);
            new ControlAnimation(groupBox_DllInfo.Height + groupBox_DllInfo.Top, 450).FadeIn(groupBox_DllInfo, ControlAnimation.FadeMethod.FromUpToDown, 50);
        }
        private void AddDLLReferenceForm_Shown(object sender, EventArgs e)
        {
            if (programSettings.Bases.Animations.WindowCreatedAnimation)
                PerformLoadAnimation();
        }
    }
}
