using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
namespace HotKeySetter
{
    /// <summary>
    /// 提供用于记录文件读取的操作。
    /// </summary>
    public class FileHistory
    {
        public static readonly string OpenFileHistoryConfigPath = ConfigFile.HotKeySetterConfigDirectory + @"\records.ini";

        /// <summary>
        /// 尝试读取打开文件的记录。
        /// </summary>
        /// <returns>成功返回记录字符串，失败或不存在返回空字符串。</returns>
        public static string TryGetOpenFileHistoryString()
        {
            try
            {
                return ConfigFile.ReadFromFile(OpenFileHistoryConfigPath, "RecentFiles", "FileName");
            }
            catch (Exception)
            {
                return "";
            }
        } 

        /// <summary>
        /// 删除配置文件记录。
        /// </summary>
        public static void DeleteOpenFileHistory()
        {
            System.IO.File.Delete(OpenFileHistoryConfigPath);
        }

        /// <summary>
        /// 获取文件使用记录的string数组。
        /// </summary>
        /// <returns>string数组。</returns>
        public static string[] TryGetOpenFileHistoryArray()
        {
            string source_string = TryGetOpenFileHistoryString();
            return StringOperation.GetSplittedStringArray(source_string, "|", 0);
        }
        /// <summary>
        /// 查询打开文件记录中是否包括了当前数据。
        /// </summary>
        /// <param name="file">当前的文件数据。</param>
        /// <returns>包括返回true，不包括返回false。</returns>
        public static bool IsFileHistoryIncluding(string file)
        {
            string history = TryGetOpenFileHistoryString();
            return StringOperation.FindSplittedStringPosition(history, "|", file, 1) != -1;
            
        }
        public static bool TrySaveOpenFileHistory(string newFileName)
        {
            string history = TryGetOpenFileHistoryString();
            if (history != "")
            {
                try
                {
                    ConfigFile.WriteStatic(OpenFileHistoryConfigPath,"RecentFiles", "FileName", history + newFileName + "|");
                }
                catch (Win32Exception)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    ConfigFile.WriteStatic(OpenFileHistoryConfigPath, "RecentFiles", "FileName", "|" +newFileName+"|");
                }
                catch (Win32Exception)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
