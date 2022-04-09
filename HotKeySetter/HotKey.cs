using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace HotKeySetter
{
    public class HotKey
    {
        IntPtr hotKeyHwnd;
        int hotKeyID;
        uint vkcode;
        HotKeyfsModifiers mod;
        [DllImport("HotKeys.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private extern static bool SetHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vkcode);
        [DllImport("HotKeys.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private extern static bool UnSetHotKey(IntPtr hwnd, int id);

        //以下是动态获取热键事件的方法。 说明：byte参数要传入一个byte[]数组的首个byte(即byte[0])，用于接收字符串，然后使用Encoding.Default编码。
        [DllImport("HotKeys.dll", EntryPoint = "HotKeyEventMainDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static int EventMainDelegate(string dllName, string entryPoint, int index, string param);
        [DllImport("HotKeys.dll", EntryPoint = "HotKeyGetDescriptionDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static int GetDescriptionDelegate(string dllName, string entryPoint, int index, ref byte title, ref byte detail);
        [DllImport("HotKeys.dll", EntryPoint = "HotKeyEnumEventDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static int EnumEventDelegate(string dllName, string entryPoint, int index, ref byte desFuncEntry, ref byte mainFuncEntry, ref byte errorFuncEntry);
        public enum HotKeyfsModifiers
        {
            INVALID = 0,
            MOD_ALT = 1,
            MOD_CONTROL = 2,
            MOD_NOREPEAT = 0x4000,
            MOD_SHIFT = 0x0004,
            MOD_WIN = 0x0008
        }


        
        /// <summary>
        /// 热键事件的类型
        /// </summary>
        public enum HotKeyEventType
        {
            NONE = 0,
            RECORDONLY = 1,
            RUNEXTERN = 2
        }

        /// <summary>
        /// 包含SuccessFlag和ErrorCode两个值，分别指示注册热键是否成功和其错误代码。
        /// </summary>
        public class HotKeyRegisterFlag
        {
            public bool SuccessFlag; //指示注册热键是否成功。
            public int ErrorCode;  //指示热建注册的错误代码，成功为0。
            public HotKeyRegisterFlag()
            {
                SuccessFlag = false;
                ErrorCode = 0;
            }
        }
        /// <summary>
        /// 包含SuccessFlags和ErrorCodes两个列表，分别指示注册热键是否成功和其错误代码。
        /// </summary>
        public class HotKeyRegisterFlags 
        {
            public List<bool> SuccessFlags; //指示注册热键是否成功。
            public List<int> ErrorCodes;//指示热建注册的错误代码，成功为0。
            public int SuccessCount
            {
                get
                {
                    int count = 0;
                    for (int i = 0; i < SuccessFlags.Count; i++)
                        if (SuccessFlags[i])
                            count++;
                    return count;
                }
            }
            public int TriedHotKeyCount
            {
                get
                {
                    return SuccessFlags.Count;
                }
            }

            public int FailedCount
            {
                get
                {
                    return TriedHotKeyCount - SuccessCount;
                }
            }

            public HotKeyRegisterFlags()
            {
                SuccessFlags = new List<bool>();
                ErrorCodes = new List<int>();
            }
            /// <summary>
            /// 添加一项指示值。
            /// </summary>
            /// <param name="flag"></param>
            public void AddFlag(HotKeyRegisterFlag flag)
            {
                if(flag != null)
                {
                    SuccessFlags.Add(flag.SuccessFlag);
                    ErrorCodes.Add(flag.ErrorCode);
                }
            }
        }
        /// <summary>
        /// 注销指定的热键。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="id">要注销的热键的id。</param>
        /// <exception cref="Win32Exception">抛出的WIN32异常。</exception>
        /// <returns>成功返回true，失败返回false并抛出WIN32异常。</returns>
        public static bool UnRegisterHotKey(IntPtr hwnd,int id)
        {
            if (UnSetHotKey(hwnd, id))
                return true;
            else throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /*
        /// <summary>
        /// 获取事件对应的文本描述。
        /// </summary>
        /// <param name="type">热键事件类型。</param>
        /// <returns>热键事件的文本描述。</returns>
        public static string GetEventTypeString(HotKeyEventType type)
        {
            switch (type)
            {
                case HotKeyEventType.RECORDONLY:
                    return "仅记录";
                case HotKeyEventType.RUNEXTERN:
                    return "运行外部程序";
                default:
                    return "无";
            }
        }
        
        /// <summary>
        /// 获取事件对应的文本描述。
        /// </summary>
        /// <param name="type">热键事件类型。</param>
        /// <returns>热键事件的文本描述。</returns>
        public static string GetEventTypeString(int type)
        {
           return  GetEventTypeString((HotKeyEventType)type);
        }*/

        /// <summary>
        /// 初始化HotKey的对象
        /// </summary>
        /// <param name="hwnd">接受热键消息的窗口句柄</param>
        /// <param name="id">热键ID</param>
        /// <param name="hotKeyfsModifiers">热键辅助键</param>
        /// <param name="vk">虚拟键代码</param>
        public HotKey(IntPtr hwnd, int id, HotKeyfsModifiers hotKeyfsModifiers, uint vk)
        {
            hotKeyHwnd = hwnd;
            hotKeyID = id;
            mod = hotKeyfsModifiers;
            vkcode = vk;
        }
        public HotKey(IntPtr hwnd, int id, uint hotKeyfsModifiers, uint vk)
        {
            hotKeyHwnd = hwnd;
            hotKeyID = id;
            mod = (HotKeyfsModifiers)hotKeyfsModifiers;
            vkcode = vk;
        }
        /// <summary>
        /// 获取次要按键的文本名称。
        /// </summary>
        /// <param name="fs">次要按键的键代码。</param>
        /// <returns>次要按键的文本名称。</returns>
        public static string GetfsModifiersKeyString(uint fs)
        {
            switch (fs)
            {
                case 1:
                    return "ALT";
                case 2:
                    return "CTRL";
                case 4:
                    return "SHIFT";
                case 8:
                    return "WIN";
                default: 
                    return "(无)";
            }
        }
        /// <summary>
        /// 通过辅助按键的文本获取其键代码。
        /// </summary>
        /// <param name="fsText">文本。</param>
        /// <returns>成功返回键代码，不存在返回0。</returns>
        public static uint GetSecondKeyCodeByString(string fsText)
        {
            switch (fsText)
            {
                case "ALT":
                    return (uint)HotKeyfsModifiers.MOD_ALT;
                case "CTRL":
                    return (uint)HotKeyfsModifiers.MOD_CONTROL;
                case "SHIFT":
                    return (uint)HotKeyfsModifiers.MOD_SHIFT;
                case "WIN":
                    return (uint)HotKeyfsModifiers.MOD_WIN;
                default:
                    return 0;
            }
        }
        public static string GetfsModifiersKeyString(HotKeyfsModifiers fs)
        {
            return GetfsModifiersKeyString((uint)fs);
        }
        /// <summary>
        /// 使用键代码获取按键的名称字符串(带修饰符)。
        /// </summary>
        /// <param name="keycode">键代码</param>
        /// <returns>键代码对应的字符串。</returns>
        public static string GetKeyNameByCode(uint keycode)
        {
            if (CharSwitch.IsValidVirtualKeyCode(keycode.ToString()))
            {
                return ((Keys)keycode).ToString();
            }
            else return null;
        }

        /// <summary>
        /// 执行调用外部程序事件。
        /// </summary>
        /// <param name="param"></param>
        /// <returns>成功执行返回true，否则返回false。</returns>
        public static bool DoRunExternEvent(string param)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.Arguments = "/c \"" + param + "\"";
           info.WindowStyle = ProcessWindowStyle.Hidden;
          
            try
            {
                Process.Start(info);
            }
            catch (Exception e)
            {
                MessageBox.Show("无法运行外部程序。\n\n原因：" + e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 注册热键。
        /// </summary>
        /// <returns>热键注册是否成功。</returns>
        /// <exception cref="Win32Exception"></exception>
        public bool Register()
        {
            if (SetHotKey(hotKeyHwnd, hotKeyID, (uint)mod, vkcode))
                return true;
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 检测某个热键是否可用。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="id">热键id。</param>
        /// <param name="modifier">辅助键。</param>
        /// <param name="vkcode">虚拟键代码。</param>
        /// <returns>若不可用，返回win32错误代码；若可用，返回0。</returns>
        public static int IsHotKeyAvailable(IntPtr hwnd,int id,int modifier,uint vkcode)
        {
            HotKey hotKey = new HotKey(hwnd, id, (HotKeyfsModifiers)modifier, vkcode);
            try
            {
                hotKey.Register();
            }
            catch(Win32Exception ex)
            {
                return ex.NativeErrorCode;
            }
            hotKey.UnRegister();
            return 0;
        }
        /// <summary>
        /// 检测某个热键是否可用。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="id">热键id。</param>
        /// <param name="modifier">辅助按键。</param>
        /// <param name="vkcode">虚拟键代码。</param>
        /// <returns>若不可用，返回win32错误代码；若可用，返回0。</returns>
        public static int IsHotKeyAvailable(IntPtr hwnd, int id, HotKeyfsModifiers modifier, uint vkcode)
        {
            HotKey hotKey = new HotKey(hwnd, id, modifier, vkcode);
            try
            {
                hotKey.Register();
            }
            catch (Win32Exception ex)
            {
                return ex.NativeErrorCode;
            }
            hotKey.UnRegister();
            return 0;
        }
        /// <summary>
        /// 检测某个热键是否可用。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="id">热键id。</param>
        /// <param name="modifier">辅助按键。</param>
        /// <param name="vkcode">虚拟键代码。</param>
        /// <returns>若不可用，返回win32错误代码；若可用，返回0。</returns>
        public static int IsHotKeyAvailable(IntPtr hwnd, int id, uint modifier, uint vkcode)
        {
            return IsHotKeyAvailable(hwnd, id, (HotKeyfsModifiers)modifier, vkcode);
        }

        public bool UnRegister()
        {
            if (UnSetHotKey(hotKeyHwnd, hotKeyID))
                return true;
            else throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}
