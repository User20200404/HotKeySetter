using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    /// <summary>
    /// 提供字符串数据分割读取操作。
    /// </summary>
    public class StringOperation
    {
        /// <summary>
        /// 获取指定字符在字符串中第n次出现的位置(index)。
        /// </summary>
        /// <param name="str">供搜索的字符串。</param>
        /// <param name="str_need">要搜索的字符</param>
        /// <param name="order">第n次出现。该值必须大于0。</param>
        /// <returns>若找到字符，返回其出现的位置；若未找到字符或参数无效，则返回-1。</returns>
        public static int GetCharIndex(string str,string str_need,int order)
        {
            int index = -1;
            if (str != null && str_need != null && order > 0)
            {
                for (int i = 0; i < order; i++)
                {
                    index = str.IndexOf(str_need, index + 1);
                    if (index == -1)
                        break;
                }
            }
            return index;
        }
        /// <summary>
        /// 获取由指定字符分割的字符串中的第n组字符串。
        /// </summary>
        /// <param name="str">供搜索的字符串。</param>
        /// <param name="split_mark">分割标志字符。</param>
        /// <param name="order">第n组字符串。该值必须大于0。</param>
        /// <returns>若成功提取，返回该字符串数据；若方法失败，则返回null。</returns>
        public static string GetSplittedString(string str, string split_mark, int order)
        {
            int index_start;
            int index_end;
            if (str != null && split_mark != null && order > 0)
            {
                index_start = GetCharIndex(str, split_mark, order);
                index_end = GetCharIndex(str, split_mark, order +1);
                if (index_start == -1 || index_end == -1) //任一分隔符位置获取失败
                    return null;
                else return str.Substring(index_start + 1, index_end - index_start - 1);
            }
            return null;
        }
        /// <summary>
        /// 构造一个由指定分割标识符分割的字符串。
        /// </summary>
        /// <param name="split_mark">分割标识符。</param>
        /// <param name="values">值。</param>
        /// <returns>构造的字符串。如由“|”分割的{val1,val2,val3}返回数据为|val1|val2|val3|</returns>
        public static string ConstructSplittedString(string split_mark, params string[] values)
        {
            string ret = split_mark;
            for (int i = 0; i < values.Length; i++)
            {
                ret += values[i] + split_mark;
            }
            return ret;
        }
        /// <summary>
        /// 构造一个由指定分割标识符分割的字符串。
        /// </summary>
        /// <param name="split_mark">分割标识符。</param>
        /// <param name="values">值。</param>
        /// <returns>构造的字符串。如由“|”分割的{val1,val2,val3}返回数据为|val1|val2|val3|</returns>
        public static string ConstructSplittedString(string split_mark, params object[] values)
        {
            List<string> val = new List<string>();
            for (int i = 0; i < values.Length; i++)
            {
                val.Add(values[i].ToString());
            }
            return ConstructSplittedString(split_mark, val.ToArray());
        }
        /// <summary>
        /// 获取由指定字符分割的字符串组中符合条件的字符串位置(从1开始)。
        /// </summary>
        /// <param name="str">供搜索的字符串。</param>
        /// <param name="split_mark">分割标志字符。</param>
        /// <param name="str_to_find">要查找的字符串。</param>
        /// <param name="order">从第n组字符串开始搜索。</param>
        /// <returns>若找到字符串，返回该字符串在字符串组中的位置(从1开始)；若失败，返回-1。</returns>
        public static int FindSplittedStringPosition(string str,string split_mark,string str_to_find,int start_order)
        {
            string data = GetSplittedString(str, split_mark, start_order);
            for (int i = start_order; data != null; data = GetSplittedString(str, split_mark, ++i))
            {
                if (data == str_to_find)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 获取由指定字符分割的字符串的等效字符串数组。
        /// </summary>
        /// <param name="str">由分隔符组成的字符串。</param>
        /// <param name="split_mark">分隔符。</param>
        /// <param name="max_count">字符数组的最大长度，若该值小于1则返回由所有分割字符串组成的数组。</param>
        /// <returns>成功返回字符串数组，失败返回null。</returns>
        public static string[] GetSplittedStringArray(string str, string split_mark, int max_count)
        {
            List<string> ret = new List<string>();
            string val = GetSplittedString(str, split_mark, 1);
            for (int i = 0; (i < max_count || max_count < 1) && val != null; i++)
            {
                ret.Add(val);
                val = GetSplittedString(str, split_mark, i + 2);
            }
            return ret.ToArray();
        }

        public static string GetFileNameFromPath(string file_path)
        {
            int last_slash = -1;
            for (int i = 0; i < file_path.Length; i++)
            {
                if (file_path[i].CompareTo('\\') == 0)
                    last_slash = i;
            }
            if (last_slash == -1)
                return file_path;
            else return file_path.Substring(last_slash + 1);
        }
        public static string GetFileNameExtensionFromPath(string file_path)
        {
            int last_slash = -1;
            for (int i = 0; i < file_path.Length; i++)
            {
                if (file_path[i].CompareTo('.') == 0)
                    last_slash = i;
            }
            if (last_slash == -1)
                return file_path;
            else return file_path.Substring(last_slash + 1);
        }
    }
}
