using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    public class Hex
    {
        /// <summary>
        /// 取十六进制高位。若传入null，本方法返回0。
        /// </summary>
        /// <param name="ptr">要提取的IntPtr类型数据。</param>
        /// <returns>高位的int型数据。</returns>
        public static int GetHighOrder(IntPtr ptr)
        {
            if (ptr != null)
            {
                return ptr.ToInt32() >> 16;
            }
            else return 0;
        }
        /// <summary>
        /// 取十六进制低位。若传入null，本方法返回0。
        /// </summary>
        /// <param name="ptr">要提取的IntPtr类型数据。</param>
        /// <returns>低位的int型数据。</returns>
        public static int GetLowOrder(IntPtr ptr)
        {
            if (ptr != null)
            {
                return ptr.ToInt32() & 0xFFFF;
            }
            else return 0;
        }
    }
}
