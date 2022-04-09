using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    /// <summary>
    /// 提供时间验证功能。
    /// </summary>
    class TimeValidate
    {

        /// <summary>
        /// 初始化TimeValidate类。
        /// </summary>
        public TimeValidate()
        {
            Items = new List<TimeValidateItem>();
        }

        /// <summary>
        /// 获取或设置TimeValidateItem的项目列表。
        /// </summary>
        public List<TimeValidateItem> Items;

        /// <summary>
        /// 检测数据是否已记录。
        /// </summary>
        /// <param name="data">要查询的数据。</param>
        /// <returns>一个bool值，指示了是否存在该数据。</returns>
        public bool Contains(object data)
        {
            for(int i = 0;i<Items.Count;i++)
            {
                if (Items[i].Data == data)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 检测数据是否已记录。
        /// </summary>
        /// <param name="data">要查询的数据。</param>
        /// <param name="index">若查询到数据，该参数被设置为其索引。</param>
        /// <returns>一个bool值，指示了是否存在该数据。</returns>
        public bool Contains(object data,out int index)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Data.Equals(data))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }

        /// <summary>
        /// 查询数据的索引。
        /// </summary>
        /// <param name="data">要查询的数据。</param>
        /// <returns>一个int值，指示了该数据在列表的索引。未找到则返回-1。</returns>
        public int Find(object data)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Data.Equals(data))
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 查询数据。
        /// </summary>
        /// <param name="data">要查询的数据。</param>
        /// <returns>包含该数据的TimeValidateItem实例。</returns>
        public TimeValidateItem FindItem(object data)
        {
            int index = Find(data);
            if (index != -1)
                return Items[index];
            else return null;
        }

        /// <summary>
        /// 删除第一个符合的项目。
        /// </summary>
        /// <param name="data">要删除的项的数据。</param>
        /// <returns>项存在则删除并返回其原先索引，失败返回-1。</returns>
        public int RemoveItem(object data)
        {
            int index = Find(data);
            if (index != -1)
            {
                Items.RemoveAt(index);
            }
            return index;
        }
    }
}
