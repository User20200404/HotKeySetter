using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace HotKeySetter
{
    class Win32KernelApi
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public extern static IntPtr LoadLibraryA(string libPath);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public extern static IntPtr GetProcAddress(IntPtr libHandle, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public extern static IntPtr FreeLibrary(IntPtr libHandle);
    }
}
