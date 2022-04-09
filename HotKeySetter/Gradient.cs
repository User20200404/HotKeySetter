using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    /// <summary>
    /// 提供引用对象的渐变设置功能。
    /// </summary>
    class Gradient
    {
        public bool stop_flag = false;
        private Thread currentThread = null;
        private GradientThread currentGradientThread = null;
        public Type MemberType;


        private Assembly assembly = Assembly.Load("HotKeySetter");
        private Type type;
        private string obj_member;
        private object obj_instance;
        private ObjectMemberType mem_type;
        private Control thread_owner;
        public delegate void GradientBeginCallBack();
        public delegate void GradientEndCallBack();

        /// <summary>
        /// 在延迟计时结束，渐变执行开始前触发该事件。
        /// </summary>
        public event GradientBeginCallBack GradientBegin;
        /// <summary>
        /// 在渐变执行完毕后触发该事件。
        /// </summary>
        public event GradientEndCallBack GradientEnd;

        /// <summary>
        /// Gradient执行渐变时的线程。
        /// </summary>
        public Thread CurrentThread
        {
            get { return currentThread; }
        }
        /// <summary>
        /// Gradient执行渐变时的GradentThread类。
        /// </summary>
        public GradientThread CurrentGradientThread
        {
            get { return currentGradientThread; }
        }
        /// <summary>
        /// 设置值来添加一个GradientBegin事件回调。该访问器为做语法方便设置。
        /// </summary>
        public GradientBeginCallBack GradientBeginEventHandlerToAdd
        {
            set
            {
                GradientBegin += value;
            }
        }
        /// <summary>
        /// 设置值来添加一个GradientEnd事件回调。该访问器为做语法方便设置。
        /// </summary>
        public GradientEndCallBack GradientEndEventHandlerToAdd
        {
            set
            {
                GradientEnd += value;
            }
        }

        /// <summary>
        /// 初始化Gradient类的实例。
        /// </summary>
        /// <param name="tar">目标父对象。</param>
        /// <param name="member">子成员的名称(Name)。</param>
        /// <param name="mt">成员的类型。</param>
        /// <param name="t_owner">线程的拥有者。</param>
        public Gradient(object tar, string member, ObjectMemberType mt, Control t_owner)
        {

            obj_instance = tar;
            type = tar.GetType();
            //obj_instance = type.Assembly.CreateInstance(type.ToString());
            obj_member = member;
            mem_type = mt;
            MemberType = GetMemberType();
            thread_owner = t_owner;
        }
        /// <summary>
        /// 获取成员类型。
        /// </summary>
        private Type GetMemberType()
        {
            if (mem_type == ObjectMemberType.Field)
                return type.GetField(obj_member).FieldType;
            else return type.GetProperty(obj_member).PropertyType;
        }
        /// <summary>
        /// 设置对象的值。
        /// </summary>
        /// <param name="obj">值。</param>
        public void SetValue(object obj)
        {
            if (!stop_flag)
            {
                try
                {
                    thread_owner.Invoke(new Action(() =>
                    {
                        if (mem_type == ObjectMemberType.Field)
                            type.GetField(obj_member).SetValue(obj_instance, obj);
                        else type.GetProperty(obj_member).SetValue(obj_instance, obj);
                    }));
                }
                catch (Exception)
                {
                }
            } 

        }
        /// <summary>
        /// 获取对象的值。
        /// </summary>
        /// <returns>对象的值。</returns>
        public object GetValue()
        {
            if (mem_type == ObjectMemberType.Field)
                return type.GetField(obj_member).GetValue(obj_instance);
            else return type.GetProperty(obj_member).GetValue(obj_instance);
        }
        /// <summary>
        /// 立刻停止所有正在进行的渐变操作。
        /// </summary>
        public void Abort()
        {
            stop_flag = true;
        }

        /// <summary>
        /// 在指定时间内将对象属性线性渐变到指定值。请确保在使该对象的引用失效前调用Abort()方法。
        /// </summary>
        /// <param name="end">终止值。</param>
        /// <param name="time">渐变进行的周期(ms)。</param>
        public void Begin(double end,int time)
        {
            Begin(end, time, 0);
        }

        public void Begin(double end, int time,uint delay)
        {
            currentGradientThread = new GradientThread(this, end, time, delay);
            currentGradientThread.ThreadBegin += ThreadBeginCallBack;
            currentGradientThread.ThreadEnd += ThreadEndCallBack;

            currentThread = new Thread(currentGradientThread.ValueMain);
            currentThread.Start();
        }

        public void BeginBezier(float end, int time,PointF[] progressPoints)
        {
            BeginBezier(end, time, progressPoints, 0);
        }

        public void BeginBezier(float end, int time, PointF[] progressPoints,uint delay)
        {
            currentGradientThread = new GradientThread(this, end, time, progressPoints, delay);
            currentGradientThread.ThreadBegin += ThreadBeginCallBack;
            currentGradientThread.ThreadEnd += ThreadEndCallBack;

            currentThread = new Thread(currentGradientThread.BezierCurveMain);
            currentThread.Start();
        }

        /// <summary>
        /// 在指定时间内将对象颜色属性线性渐变到指定值。请确保在使该对象的引用失效前调用Abort()方法。
        /// </summary>
        /// <param name="end">终止值。</param>
        /// <param name="time">渐变进行的周期(ms)。</param>
        public void Begin(Color color, int time) 
        {
            Begin(color, time, 0);
        }


        /// <summary>
        /// 在指定时间内将对象颜色属性线性渐变到指定值。请确保在使该对象的引用失效前调用Abort()方法。
        /// </summary>
        /// <param name="end">终止值。</param>
        /// <param name="time">渐变进行的周期(ms)。</param>
        public void Begin(Color color, int time,uint delay)
        {
            currentGradientThread = new GradientThread(this, color.ToArgb(), time,delay);
            currentGradientThread.ThreadBegin += ThreadBeginCallBack;
            currentGradientThread.ThreadEnd += ThreadEndCallBack;

            currentThread = new Thread(currentGradientThread.ColorMain);
            currentThread.Start();
        }

        private void ThreadBeginCallBack()
        {
            try
            {
                thread_owner?.Invoke(new Action(() => { GradientBegin?.Invoke(); }));
            }
            catch(InvalidOperationException)
            {
                Debug.WriteLine("exception caught.");
            }
        }

       private void ThreadEndCallBack()
        {
            try
            {
                thread_owner?.Invoke(new Action(() => { GradientEnd?.Invoke(); }));
            }
            catch (InvalidOperationException)
            {
                Debug.WriteLine("exception caught.");
            }
        }

        /// <summary>
        /// 指示了一个成员属于Field还是属于Property。
        /// </summary>
        public enum ObjectMemberType
        {
            /// <summary>
            /// Field类型成员，其没有定义get和set访问器。
            /// </summary>
            Field = 0,
            /// <summary>
            /// Property类型成员，其定义了get或set访问器。
            /// </summary>
            Property = 1,
        }
    }
}
