using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotKeySetter
{   
    /// <summary>
    /// Gradient类所使用的线程类。
    /// </summary>
    class GradientThread
    {
        private uint delay_begin;
        private DateTime begin_time;
        private DateTime end_time;
        private double end_val;
        private int span_time;
        private PointF[] curve_points = new PointF[0];
        private Gradient gradient;
        private Action<object> SetAction;
        private Func<object> GetAction;
        private Func<Type> GetTypeOfMember;
        /// <summary>
        /// 在渐变线程执行完毕且即将结束前触发。
        /// </summary>
        public delegate void GradientThreadEndCallBack();
        /// <summary>
        /// 在渐变线程开始执行且在获取初始值之前触发。
        /// </summary>
        public delegate void GradientThreadBeginCallBack();
     
        public event GradientThreadBeginCallBack ThreadBegin;

        public event GradientThreadEndCallBack ThreadEnd;


        /// <summary>
        /// 初始化GradientThread类的实例。
        /// </summary>
        /// <param name="grad">Gradient类的实例。</param>
        /// <param name="end">目标的终止值。</param>
        /// <param name="time">渐变所花的时间。</param>
        public GradientThread(Gradient grad,double end,int time)
        {
            InitClass(grad, end, time, 0);
        }

        /// <summary>
        /// 初始化GradientThread类的实例。
        /// </summary>
        /// <param name="grad">Gradient类的实例。</param>
        /// <param name="end">目标的终止值。</param>
        /// <param name="time">渐变所花的时间(ms)。</param>
        /// <param name="delay">渐变执行前等待的延迟(ms)。</param>
        public GradientThread(Gradient grad, double end, int time,uint delay)
        {
            InitClass(grad, end, time, delay);
        }

        private void InitClass (Gradient grad, double end, int time, uint delay)
        {
            if (grad != null)
            {
                gradient = grad;
                end_val = end;
                span_time = time;
                delay_begin = delay;
                SetAction = new Action<object>((obj) => { gradient.SetValue(obj); });
                GetAction = new Func<object>(() => { return gradient.GetValue(); });
                GetTypeOfMember = new Func<Type>(() => { return gradient.MemberType; });
            }
            else throw new NullReferenceException("参数“grad”的引用不能为空。");
        }

        /// <summary>
        /// 初始化GradientThread的实例。
        /// </summary>
        /// <param name="grad">Gradient类。</param>
        /// <param name="end">数据终止值。</param>
        /// <param name="time">从渐变开始到结束所用的时间。</param>
        /// <param name="points">一个有序二维点组。格式：(<渐变时间进度>,<渐变值>) 时间进度取值在0-1间。</param>
        public GradientThread(Gradient grad, double end, int time,PointF[] points)
        {
            if (grad != null)
            {
                gradient = grad;
                end_val = end;
                span_time = time;
                curve_points = points;
                SetAction = new Action<object>((obj) => { gradient.SetValue(obj); });
                GetAction = new Func<object>(() => { return gradient.GetValue(); });
                GetTypeOfMember = new Func<Type>(() => { return gradient.MemberType; });
            }
            else throw new NullReferenceException("参数“grad”的引用不能为空。");
        }
        public GradientThread(Gradient grad, double end, int time, PointF[] points,uint delay)
        {
            if (grad != null)
            {
                gradient = grad;
                end_val = end;
                span_time = time;
                curve_points = points;
                delay_begin = delay;
                SetAction = new Action<object>((obj) => { gradient.SetValue(obj); });
                GetAction = new Func<object>(() => { return gradient.GetValue(); });
                GetTypeOfMember = new Func<Type>(() => { return gradient.MemberType; });
            }
            else throw new NullReferenceException("参数“grad”的引用不能为空。");
        }

        private void SetValue(object val)
        {
            SetAction(UnDoConvert(val));
        }
        private object GetValue()
        {
            return DoConvert(GetAction());
        }
        /// <summary>
        /// 渐变数值功能入口。
        /// </summary>
        public void ValueMain()
        {
            Thread.Sleep((int)delay_begin);

            ActivateBeginEvent();

            begin_time = DateTime.Now;
            end_time = begin_time.AddMilliseconds(span_time);
            double begin_val = Convert.ToDouble(GetValue());
            float k = (float)(end_val - begin_val) / (float)span_time; //求变化斜率
            for (float time_pass = (float)(DateTime.Now - begin_time).TotalMilliseconds; begin_time.AddMilliseconds(time_pass) < end_time; System.Threading.Thread.Sleep(1))
            {
                if (!gradient.stop_flag)
                {
                    float val_set = (float)begin_val + (float)(k * time_pass);
                    if ((end_val - val_set) * k >= 0)
                    {
                        SetValue(val_set);
                    }
                    time_pass = (int)(DateTime.Now - begin_time).TotalMilliseconds;

                }
                else return;
            }
            SetValue(end_val);

            ActivateEndEvent();
        }
        /// <summary>
        /// 渐变颜色功能入口。
        /// </summary>
        public void ColorMain()
        {
            Thread.Sleep((int)delay_begin);
            ActivateBeginEvent();

            begin_time = DateTime.Now;
            end_time = begin_time.AddMilliseconds(span_time);
            Color begin_val = (Color)GetValue();
            Color end_val_color = Color.FromArgb((int)end_val);
            int br = begin_val.R;
            int bg = begin_val.G;
            int bb = begin_val.B;
            int er = end_val_color.R;
            int eg = end_val_color.G;
            int eb = end_val_color.B;
            float kr = (float)(er - br) / (float)span_time;
            float kg = (float)(eg - bg) / (float)span_time;
            float kb = (float)(eb - bb) / (float)span_time;

            int val_r, val_g, val_b;

            for (int time_pass = (int)(DateTime.Now - begin_time).TotalMilliseconds; begin_time.AddMilliseconds(time_pass) < end_time; System.Threading.Thread.Sleep(1))
            {
                if (!gradient.stop_flag)
                {
                    val_r = (int)(br + kr * time_pass);
                    val_g = (int)(bg + kg * time_pass);
                    val_b = (int)(bb + kb * time_pass);
                    SetValue(Color.FromArgb(val_r, val_g, val_b));
                    time_pass = (int)(DateTime.Now - begin_time).TotalMilliseconds;
                }
                else return;
            }
            SetValue(Color.FromArgb((int)end_val));
            ActivateEndEvent();
        }

        /// <summary>
        /// 贝塞尔曲线渐变功能入口。
        /// </summary>
        public void BezierCurveMain()
        {
            Thread.Sleep((int)delay_begin);
            ActivateBeginEvent();
            if (curve_points.Length > 0)
            {
                //绘制阶段
                begin_time = DateTime.Now;
                end_time = begin_time.AddMilliseconds(span_time);
                double begin_val = Convert.ToDouble(GetValue());
                double diff = end_val - begin_val;

                PointF[] curve_input_points = new PointF[curve_points.Length + 2];
                curve_input_points[0] = new PointF(0f, (float)begin_val);
                for(int i = 0;i<curve_points.Length; i++)
                {
                    curve_input_points[i + 1] = new PointF(curve_points[i].X, curve_points[i].Y * (float)diff + (float)begin_val);
                }
                curve_input_points[curve_input_points.Length - 1] = new PointF(1f, (float)end_val);
                /*for(int i = 0;i<curve_points.Length;i++)
                {
                    curve_input_points[i+1] = new PointF((float)(begin_time.AddMilliseconds(span_time*curve_points[i].X) - begin_time).TotalMilliseconds,curve_points[i].Y); //计算时间轴对应的实际值。
                }
                */
                PointF[] curve_output_points = BezierCurve.Draw(curve_input_points, curve_input_points.Length, 0.005f);

                //执行阶段
                for (float time_pass = (float)(DateTime.Now - begin_time).TotalMilliseconds; begin_time.AddMilliseconds(time_pass) < end_time; System.Threading.Thread.Sleep(1))
                {
                    float t_ratio = time_pass / (float)span_time;
                    
                    for(int i = 0;i<curve_output_points.Length;i++)
                    {
                        if(curve_output_points[i].X<t_ratio &&t_ratio<curve_output_points[i+1].X)
                        {
                            SetValue(curve_output_points[i].Y);
                        }
                    }

                    time_pass = (float)(DateTime.Now - begin_time).TotalMilliseconds;
                }
                SetValue(end_val);
                ActivateEndEvent();
            }
            else throw new ArgumentNullException("必须在初始化实例时指定贝塞尔曲线所用的有效点组，才能调用此方法。");
        }

        /// <summary>
        /// 根据对象类型进行转换。
        /// </summary>
        /// <param name="source">要转换为数值的值。</param>
        /// <returns>转换后的数值数据。</returns>
        private object DoConvert(object source)
        {
            Type current_type = GetTypeOfMember();
            return Convert.ChangeType(source, current_type);
        }

        private object UnDoConvert(object source)
        {
            return Convert.ChangeType(source, GetTypeOfMember());
        }

        /// <summary>
        /// 广播线程开始执行事件。
        /// </summary>
        private void ActivateBeginEvent()
        {
            ThreadBegin?.Invoke();
        }
        /// <summary>
        /// 广播线程执行完毕事件。
        /// </summary>
        private void ActivateEndEvent()
        {
            ThreadEnd?.Invoke();
        }
    }
}
