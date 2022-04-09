using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace HotKeySetter
{
    static class Program
    {

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string classname, string windowname);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hwnd, int status);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ActivateWindow(IntPtr hwnd);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CheckDll();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Mutex mutex = new Mutex(true, "HotKeySetter", out bool createNew);
            if (createNew || args.Contains<string>("-IgnoreMutex"))
            {
                Application.Run(new MainForm(args));
            }
            else
            {
                IntPtr hwnd = FindWindow(null, "HotKeySetter" + Application.ProductVersion);
                if (hwnd != IntPtr.Zero)
                {
                    ShowWindow(hwnd, 5); //5 = 原先状态激活窗口
                    ActivateWindow(hwnd);
                }
            }
        }

        /// <summary>
        /// 在这里处理未捕获异常的代码。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Settings.Other.Developer exceptionSettings = new Settings.Other.Developer();
            exceptionSettings.LoadFromFile(ConfigFile.HotKeySetterConfigDirectory);
            //如果没有设置忽略所有异常
            if (!exceptionSettings.IgnoreException)
            {
               DialogResult result =  MessageBox.Show("在" + e.Exception.Source + "发生未经处理的异常。\n\n"+"详细信息："+e.Exception.Message+"\n\n点击“是”以忽略错误并尝试继续，点击“否”以退出HotKeySetter。","程序发生错误",MessageBoxButtons.YesNo,MessageBoxIcon.Error);
               if(result == DialogResult.No)
                {
                    Environment.Exit(-1);
                }
            }
        }

        private static void CheckDll()
        {
            string programDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //是否存在
            bool winlongproject = File.Exists(programDir + @"\winlongproject.dll"),
                HotKeys = File.Exists(programDir + @"\HotKeys.dll"),
                DllHookProc = File.Exists(programDir + @"\DllHookProc.dll");

            //是否能够加载
            IntPtr winlongproject_handle = Win32KernelApi.LoadLibraryA("winlongproject.dll");
            IntPtr HotKeys_handle = Win32KernelApi.LoadLibraryA("HotKeys.dll");
            bool winlongproject_load = winlongproject_handle != IntPtr.Zero;
            bool HotKeys_load = HotKeys_handle != IntPtr.Zero;

            if (!winlongproject || !HotKeys || !DllHookProc)
            {
                string msg = "";
                if (!winlongproject)
                    msg += "winlongproject.dll\n";
                if (!HotKeys)
                    msg += "HotKeys.dll\n";
                if (!DllHookProc)
                    msg += "DllHookProc";
                DialogResult result = MessageBox.Show("HotKeySetter缺少以下必要库文件：\n"+ msg+"\n忽略错误并继续，请点击“确定”；退出，请点击“取消”。" , "缺少必要DLL", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel)
                {
                    Environment.Exit(2);
                }
            }
            else
            {
                if (!winlongproject_load||!HotKeys_load)
                {
                    string msg = "";
                    if (!winlongproject_load)
                        msg += "winlongproject.dll\n";
                    if (!HotKeys_load)
                        msg += "HotKeys.dll\n";
                    DialogResult result = MessageBox.Show("HotKeySetter检测到库文件存在，但未能成功加载，这可能是系统因为缺少运行库，请尝试使用HotKeySetterStatic版本。\n错误的文件：\n"+ msg+"\n忽略错误并继续，请点击“确定”；退出，请点击“取消”。" + msg, "无法加载必要DLL", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Cancel)
                    {
                        Environment.Exit(2);
                    }
                }
            }
            //释放dll资源
            Win32KernelApi.FreeLibrary(winlongproject_handle);
            Win32KernelApi.FreeLibrary(HotKeys_handle);
        }
        private static void InitConfigDir()
        {
            Directory.CreateDirectory(ConfigFile.HotKeySetterConfigDirectory);
            Directory.CreateDirectory(ConfigFile.HotKeySetterConfigDirectory + @"\Reference.ini");
        }

    }
}
