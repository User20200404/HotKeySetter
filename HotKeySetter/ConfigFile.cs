using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using Microsoft.Win32;

namespace HotKeySetter
{
    class ConfigFile
    {
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringA",SetLastError = true)]
        private static extern bool WritePrivateProfileString(string AppName, string KeyName, string StringToWrite, string FileName);
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString",SetLastError = true)]
        private static extern uint GetPrivateProfileString(string AppName, string KeyName, string DefaultString, StringBuilder OutPutString,int nSize, string FileName);
        private string app_string, key_string, tar_string;

        const string regKeyFolders = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders";
        const string regValueAppData = @"Local AppData";

        public static string AppDataPath
        {
            get {return Registry.GetValue(regKeyFolders,regValueAppData,null) as string;  }
        }
        public static string FileReadHistoryConfig
        {
            get { return HotKeySetterConfigDirectory + @"\record.ini"; }
        }
        public static string HotKeySetterConfigDirectory
        {
            get { return AppDataPath + @"\HotKeySetter"; }
        }
        public static string HotKeySetterReferenceDir
        {
            get { return AppDataPath + @"\HotKeySetter\Reference"; }
        }
        public ConfigFile(string appname,string keyname,string targetname)
        {
            app_string = appname;
            key_string = keyname;
            tar_string = targetname;
        }
        /// <summary>
        /// 抛出WIN32最后错误的异常。
        /// </summary>
        /// <exception cref="Win32Exception">WIN32异常。</exception>
        private static void ThrowLastWin32Exception()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        /// <summary>
        /// 将配置数据写入文件。
        /// </summary>
        /// <param name="filetowrite">要写入数据的文件。</param>
        /// <returns>成功返回true，失败返回false并抛出WIN32异常。</returns>
        /// <exception cref="Win32Exception">函数失败时，抛出对应错误的WIN32异常。</exception>
        public bool WriteToFile(string filetowrite) 
        {
            if (WritePrivateProfileString(app_string, key_string, tar_string, filetowrite))
            {
                return true;
            }
            else
            {
                ThrowLastWin32Exception();
                return false;
            }
        }
        /// <summary>
        /// 从文件中读取配置数据。
        /// </summary>
        /// <param name="filetoread">要读取数据的文件。</param>
        /// <param name="appname">节名称。</param>
        /// <param name="keyname">项名称。</param>
        /// <returns>成功返回数据字符串，失败返回空字符串并抛出WIN32异常。</returns>
        /// <exception cref="Win32Exception">函数失败时，抛出对应错误的WIN32异常。</exception>
        public static string ReadFromFile(string filetoread,string appname,string keyname)
        {
            StringBuilder ProfileStringBuilder = new StringBuilder(255);
            GetPrivateProfileString(appname, keyname, "", ProfileStringBuilder, 255, filetoread);
            if (Marshal.GetLastWin32Error() != 0)
            {
                ThrowLastWin32Exception();
                return "";
            }
            else return ProfileStringBuilder.ToString();
        }
        /// <summary>
        /// 将配置数据写入文件。
        /// </summary>
        /// <param name="filetowrite">要写入数据的文件。</param>
        /// <param name="appname">节名称。</param>
        /// <param name="keyname">项名称。</param>
        /// <param name="tarname">要写入的字符串数据。</param>
        /// <returns>成功返回true，失败返回false并抛出WIN32异常。</returns>
        /// <exception cref="Win32Exception">对应错误的WIN32异常。</exception>
        public static bool WriteStatic(string filetowrite,string appname,string keyname,string tarname)
        {
            if (!WritePrivateProfileString(appname, keyname, tarname, filetowrite))
            {
                return false;
            }
            else return true;
        }
    }
}
