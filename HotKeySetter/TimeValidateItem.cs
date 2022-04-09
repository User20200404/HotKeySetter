using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    /// <summary>
    /// 时间验证功能的项目。
    /// </summary>
    class TimeValidateItem
    {
        /// <summary>
        /// 记录的数据。
        /// </summary>
        public object Data;
        /// <summary>
        /// 该数据的验证时间。
        /// </summary>
        public DateTime RecordTime;

        /// <summary>
        /// 初始化TimeValidateItem类的实例。其Data为null，验证时间为当前时间。
        /// </summary>
        public TimeValidateItem()
        {
            Data = null;
            RecordTime = DateTime.Now;
        }

        /// <summary>
        /// 初始化TimeValidateItem类的实例。
        /// </summary>
        /// <param name="Data">要记录的数据。</param>
        /// <param name="RecordTime">数据的验证时间。</param>
        public TimeValidateItem(object In_Data,DateTime In_RecordTime)
        {
            Data = In_Data;
            RecordTime = In_RecordTime;
        }

        /// <summary>
        /// 初始化TimeValidateItem类的实例。其验证时间为当前时间。
        /// </summary>
        /// <param name="Data">要记录的数据。</param>
        public TimeValidateItem(object In_Data)
        {
            Data = In_Data;
            RecordTime = DateTime.Now;
        }
        /// <summary>
        /// 验证数据是否已过时。
        /// </summary>
        /// <param name="DeadLine">以毫秒为单位的时间限期。</param>
        /// <returns>返回一个bool值，指示了数据验证时间到现在时间的间隔是否在限期内。</returns>
        public bool Check(int deadLine)
        {
            TimeSpan span =  DateTime.Now - RecordTime;
            return span.TotalMilliseconds < deadLine;
        }

        /// <summary>
        /// 验证数据是否已过时。若未过时则立刻更新验证时间到当前时间。
        /// </summary>
        /// <param name="deadLine">以毫秒为单位的时间限期。</param>
        /// <returns>返回一个bool值，指示了数据验证时间到现在时间的间隔是否在限期内。</returns>
        public bool CheckAndUpdateIfTrue(int deadLine)
        {
            bool flag = Check(deadLine);
            if (flag)
                UpdateRecordTime();
            return flag;
        }

        /// <summary>
        /// 验证数据是否已过时。若已过时则立刻更新验证时间到当前时间。
        /// </summary>
        /// <param name="deadLine">以毫秒为单位的时间限期。</param>
        /// <returns>返回一个bool值，指示了数据验证时间到现在时间的间隔是否在限期内。</returns>
        public bool CheckAndUpdateIfFalse(int deadLine)
        {
            bool flag = Check(deadLine);
            if (!flag)
                UpdateRecordTime();
            return flag;
        }

        /// <summary>
        /// 验证数据是否已过时 并 立刻更新验证时间到当前时间。
        /// </summary>
        /// <param name="deadLine">以毫秒为单位的时间限期。</param>
        /// <returns>返回一个bool值，指示了数据验证时间到现在时间的间隔是否在限期内。</returns>
        public bool CheckAndUpdate(int deadLine)
        {
            bool flag = Check(deadLine);
            UpdateRecordTime();
            return flag;
        }
        /// <summary>
        /// 更新验证时间为当前时间。
        /// </summary>
        public void UpdateRecordTime()
        {
            UpdateRecordTime(TimeSpan.Zero);
        }

        /// <summary>
        /// 更新验证时间为 当前时间加上相对时间 的时间。
        /// </summary>
        /// <param name="timeToAdd">要推后的时间。</param>
        public void UpdateRecordTime(TimeSpan timeToAdd)
        {
            RecordTime = DateTime.Now + timeToAdd;
        }
    }
}
