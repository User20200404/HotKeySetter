using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace HotKeySetter
{
    public class Win32UIApi
    {
        /// <summary>
        /// (WIN32 API)
        /// 设置一个窗口的不透明度。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="crkey">颜色键值。</param>
        /// <param name="balpha">Alpha透明值。</param>
        /// <param name="dwflags">使用的分层模式。</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, int crkey, byte balpha, LayeredMode dwflags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hwnd_child, IntPtr hwnd_parent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hwnd_child);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SetWindowPos(IntPtr hwnd, HWndInsertAfter hWndInsertAfter, int X, int Y, int cX, int cY, WindowPosuFlags uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool GetCursorPos(out Point point);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        public enum HWndInsertAfter
        {
            HWND_BOTTOM = 1,
            HWND_NOTOPMOST = 2,
            HWND_TOP = 0,
            HWND_TOPMOST = -1
        }
        public enum WindowPosuFlags
        {
            SWP_ASYNCWINDOWPOS = 0x4000,
            SWP_DEFERERASE = 0x2000,
            SWP_DRAWFRAME = 0x20,
            SWP_FRAMECHANGED = 0x20,
            SWP_HIDEWINDOW = 0x80,
            SWP_NOACTIVATE = 0x10,
            SWP_NOCOPYBITS = 0x100,
            SWP_NOMOVE = 0x2,
            SWP_NOOWNERZORDER = 0x200,
            SWP_NOREDRAW = 0x8,
            SWP_NOREPOSITION = 0x200,
            SWP_NOSENDCHANGING = 0x400,
            SWP_NOSIZE = 0x1,
            SWP_NOZORDER = 0x4,
            SWP_SHOWWINDOW = 0x40
        }
        /// <summary>
        /// (winlongproject.dll支持)
        /// 向指定窗口添加一个扩展属性。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="ex_style_long">扩展属性的long值。</param>
        /// <param name="ex_style_string">扩展属性在Windows.h中常量的字符串。</param>
        /// <param name="ex_style_index">扩展属性在Windows.h中常量字符串以首字母顺序排序的索引。</param>
        /// <returns>成功返回true，失败返回false。(这个返回值并不一定可靠：即使成功时也有可能返回false。)</returns>
        [DllImport("winlongproject.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl,CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool AddWindowExstyle(IntPtr hwnd, int ex_style_long, string ex_style_string, int ex_style_index);
        /// <summary>
        /// (winlongproject.dll支持)
        /// 从指定窗口移除一个扩展属性。请注意一些扩展属性可能不支持直接移除。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="ex_style_long">扩展属性的long值。</param>
        /// <param name="ex_style_string">扩展属性在Windows.h中常量的字符串。</param>
        /// <param name="ex_style_index">扩展属性在Windows.h中常量字符串以首字母顺序排序的索引。</param>
        /// <returns>成功返回true，失败返回false。某些扩展属性不支持移除，但本方法若设置long值成功，依然返回true。</returns>
        [DllImport("winlongproject.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl,CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool DeleteWindowExstyle(IntPtr hwnd, int ex_style_long, string ex_style_string, int ex_style_index);


        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SendMessageA(IntPtr hwnd,int msg,int wp,int lp);



        /// <summary>
        /// 指定窗口透明使用的分层模式。
        /// </summary>

        public enum LayeredMode
        {
            /// <summary>
            /// 使用Alpha值设置透明度(0-255)
            /// </summary>
            LWA_ALPHA = 2,
            /// <summary>
            /// 使用颜色键值设置要屏蔽的颜色。
            /// </summary>
            LWA_COLORKEY = 1
        }
    }
}
