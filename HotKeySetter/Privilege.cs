using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
namespace HotKeySetter
{

    
    public class Privilege
    {
        /// <summary>
        /// 获取一个值，指示当前进程是否拥有管理员令牌。
        /// </summary>
        /// <returns>管理员权限返回true，否则返回false。</returns>
        static public bool IsProgramRunningAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// 获取当前用户令牌的字符串。
        /// </summary>
        /// <returns>当前令牌的字符串。</returns>
        static public string GetCurrentTokenName()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            return id.Name;
        }

        /// <summary>
        /// 执行Windows消息的权限设置。
        /// </summary>
        public class WindowsMessages
        {
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            private extern static bool ChangeWindowMessageFilter(int msg, MessageFilterFlag flag);
            enum MessageFilterFlag
            {
                MSGFLT_ADD = 1,
                MSGFLT_REMOVE =2
            }
            /// <summary>
            /// 允许低特权进程向本进程发送窗口消息。
            /// </summary>
            /// <param name="msg_value">消息数值。</param>
            static public bool Allow(int msg_value)
            {
                return ChangeWindowMessageFilter(msg_value, MessageFilterFlag.MSGFLT_ADD);
            }
            /// <summary>
            /// 允许低特权进程向本进程发送窗口消息。
            /// </summary>
            /// <param name="msg_val_start">消息起始值。</param>
            /// <param name="msg_val_end">消息终止值。</param>
            static public bool Allow(int msg_val_start, int msg_val_end)
            {
                bool flag = true;
                for (int i = msg_val_start; i <= msg_val_end; i++)
                {
                    if (!Allow(i))
                        flag = false;
                }
                return flag;
            }
            /// <summary>
            /// 禁止低特权进程向本进程发送窗口消息。
            /// </summary>
            /// <param name="msg_value">消息数值。</param>
            static public bool Remove(int msg_value)
            {
               return ChangeWindowMessageFilter(msg_value, MessageFilterFlag.MSGFLT_REMOVE);
            }
            /// <summary>
            /// 禁止低特权进程向本进程发送窗口消息。
            /// </summary>
            /// <param name="msg_val_start">消息起始值。</param>
            /// <param name="msg_val_end">消息终止值。</param>
            static public bool Remove(int msg_val_start, int msg_val_end)
            {
                bool flag = true;
                for (int i = msg_val_start; i <= msg_val_end; i++)
                {
                    if (!Remove(i))
                        flag = false;
                }
                return flag;
            }
        }
    }
}
