using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace HotKeySetter
{
    public class ErrorMessage
    {
        private static string MakeDebugInfo(Exception ex)
        {
            return "Exception: " + ex.GetType().ToString() + "\n方法："+ex.TargetSite+"\n\n堆栈信息：\n" + ex.StackTrace;
        }
        /// <summary>
        /// 向用户显示WIN32错误消息。
        /// </summary>
        /// <param name="ex">发生的异常。</param>
        /// <param name="adds">附加信息(若本项为null或空则不显示)。</param>
        public static void ShowWin32ErrorMessageBox(Win32Exception ex,string adds)
        {
            if(adds != null && adds!= "")
            {
                MessageBox.Show("发生意外错误：(" + ex.NativeErrorCode.ToString() + ")" + ex.Message + "\n附加信息："+adds+"\n\n以下信息可能对开发者有帮助：\n" + MakeDebugInfo(ex),"捕获到异常",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("发生意外错误：(" + ex.NativeErrorCode.ToString() + ")" + ex.Message + "\n\n以下信息可能对开发者有帮助：\n" + MakeDebugInfo(ex), "捕获到异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 向用户显示热键配置文件过时的消息。
        /// </summary>
        /// <param name="file_path">过时的热键配置文件。</param>
        public static DialogResult ShowConfigOutOfDateMessageBox(string file_path)
        {
            return MessageBox.Show(file_path + "\n\n" + "这是一个来自较旧版本的配置文件，可能缺少部分新的功能配置信息。\nHotKeySetter并不保证程序能正确运行。\n\n继续尝试读取文件请单击\"是\"，取消请单击\"否\"。","过时的热键配置文件",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 向用户显示热键配置文件过时的消息。本消息框不会询问用户是否强制读取。
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowConfigOutOfDateMessageBox(ConfigOutOfDateException ex)
        {
            MessageBox.Show(ex.Message + "\n版本信息不对等，为防止关键信息的缺失，HotKeySetter将不会加载该配置。\n\n若确保配置文件有效，请手动添加或更改以下配置信息：\n[HotKeySetter]\nVersion = " + Application.ProductVersion + "\n\n抛出的异常：" + ex.GetType().ToString(), "版本信息错误", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
        }
        public class ConfigOutOfDateException:Exception
        {
            public ConfigOutOfDateException() {}
            public ConfigOutOfDateException(string message) : base(message) { }
            public ConfigOutOfDateException(string message, Exception inner):base(message,inner) { }
        }
    }
}
