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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            InitAboutInfo();
        }
        Settings programSettings;
        /// <summary>
        /// 获取关于信息。
        /// </summary>
        private void InitAboutInfo()
        {
            textBox_progver.Text = Application.ProductVersion;

            var versInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            textBox_filever.Text = versInfo.FileVersion;

            textBox_dotnetCLRver.Text = Environment.Version.ToString();

            Process current_proc = Process.GetCurrentProcess();
            textBox_currentproc.Text = current_proc.ProcessName + "(" + current_proc.Id + ")";

            string status_cn = "";
            if (Privilege.IsProgramRunningAsAdmin())
                status_cn = "是";
            else status_cn = "否";
            textBox_adminstatus.Text = status_cn + "(" + Privilege.GetCurrentTokenName() + ")";

#if DEBUG
            label_DebugInfo.Text = "Debug";
#endif
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            Control mainForm = (Control)Tag; //Tag是主窗口引用
            Left = mainForm.Left;
            Top = mainForm.Top;

            //加载设置
            programSettings = new Settings();
        }
        /// <summary>
        /// 激活加载动画。
        /// </summary>
        private void PerformLoadAnimation()
        {
            panel_all.Hide();
            new ControlAnimation(50, 550) { FadeBeginEventHandlerToAdd = LoadAnimationCallBack }.FadeIn(panel_all, ControlAnimation.FadeMethod.FromUpToDown);
        }

        private void LoadAnimationCallBack(Control sender)
        {
            panel_all.Show();
        }


        private void label_techdec_Click(object sender, EventArgs e)
        {
            label_techdec.Enabled = false;

            Gradient grad_this_width = new Gradient(this, "Width", Gradient.ObjectMemberType.Property, this);
            grad_this_width.BeginBezier(groupBox_techdeclare.Left + groupBox_techdeclare.Width + 20, 550, new PointF[] { new PointF(0.05f, 0.98f), new PointF(0.06f,0.99f),new PointF(0.9f,0.995f)});

            ControlAnimation animation = new ControlAnimation(50, 550);
            animation.FadeIn(groupBox_techdeclare, ControlAnimation.FadeMethod.FromUpToDown,350);

            ControlAnimation label_animation = new ControlAnimation(0, 400);
            label_animation.FadeOut(label_techdec, ControlAnimation.FadeMethod.FromDownToUp, 0);
        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            if(programSettings.Bases.Animations.WindowCreatedAnimation)
            {
                PerformLoadAnimation();
            }
        }
    }
}
