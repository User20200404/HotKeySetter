using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    /// <summary>
    /// 提供控件的动画效果。
    /// </summary>
    public class ControlAnimation
    {
        public delegate void FadeBeginEventHandler(Control sender);
        public delegate void FadeEndEventHandler(Control sender);
        public event FadeBeginEventHandler FadeBegin;
        public event FadeEndEventHandler FadeEnd;
        private int distance, time;
        private bool deleteLayeredAttribute;
        private Control control;
        /// <summary>
        /// 初始化ControlAnimation类的实例。
        /// </summary>
        /// <param name="distance">动画距离。</param>
        /// <param name="time">动画持续时间。</param>
        public ControlAnimation(int distance, int time)
        {
            this.distance = distance;
            this.time = time;
        }
        /// <summary>
        /// 指示是否在渐变结束后移除控件的分层属性(会使透明效果失效。)
        /// </summary>
        public bool DeleteLayeredAttributeAtEnd
        {
            get { return deleteLayeredAttribute; }
            set
            {
                deleteLayeredAttribute = value;
            }
        }
        public FadeEndEventHandler FadeEndEventHandlerToAdd
        {
            set { FadeEnd += value; }
        }
        public FadeBeginEventHandler FadeBeginEventHandlerToAdd
        {
            set { FadeBegin += value; }
        }




        public static PointF[] BezierAnimationInPoints = new PointF[] { new PointF(0f, 0.8f), new PointF(0f, 1f)};
        public static PointF[] BezierAnimationInPointsOpp = new PointF[] { new PointF(0f, 0f), new PointF(0, 0.2f) };
        public static PointF[] BezierAnimationOutPoints = new PointF[] { new PointF(1f, 0f), new PointF(0.83f, 0.8f)};
        public static PointF[] BezierAnimationOutPointsOpp = new PointF[] { new PointF(0f, 1f), new PointF(0.17f, 0.2f)};
        /// <summary>
        /// 使控件从一个方向非线性淡入。
        /// </summary>
        /// <param name="control">控件对象。</param>
        /// <param name="method">淡入的方法。</param>
        public void FadeIn(Control control, FadeMethod method)
        {
            FadeIn(control, method, 0);
        }
        /// <summary>
        /// 使控件从一个方向非线性淡入。
        /// </summary>
        /// <param name="control">控件对象。</param>
        /// <param name="method">淡入的方法。</param>
        /// <param name="delay">执行动画前的延迟。</param>
        public void FadeIn(Control control, FadeMethod method, uint delay)
        {
            if (control != null)
            {
                //执行渐入
                this.control = control;
                Win32UIApi.AddWindowExstyle(control.Handle, 0, "WS_EX_LAYERED", 0); //添加分层属性。
                TransparentGradient transparentGradient = new TransparentGradient(control) { TransparentVal = 0 };
                Gradient gradient, gradient_transval;
                int end_val;
                switch (method)
                {
                    case FadeMethod.FromLeftToRight:
                        {
                            end_val = control.Left;
                            control.Left -= distance;
                            gradient = new Gradient(control, "Left", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    case FadeMethod.FromRightToLeft:
                        {
                            end_val = control.Left;
                            control.Left += distance;
                            gradient = new Gradient(control, "Left", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    case FadeMethod.FromUpToDown:
                        {
                            end_val = control.Top;
                            control.Top -= distance;
                            gradient = new Gradient(control, "Top", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    case FadeMethod.FromDownToUp:
                        {
                            end_val = control.Top;
                            control.Top += distance;
                            gradient = new Gradient(control, "Top", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    default: throw new Exception("Invalid Value");                 
                }

                PointF[] points = BezierAnimationInPoints;
                gradient_transval = new Gradient(transparentGradient, "TransparentVal", Gradient.ObjectMemberType.Property, control);

                gradient.GradientEnd += FadeInEndCallBack;
                gradient.BeginBezier(end_val, time, points,delay);
                gradient_transval.Begin(255d, time,delay);

                //触发开始事件
                FadeBegin?.Invoke(control);
            }
        }

        /// <summary>
        /// 淡入淡出方法。
        /// </summary>
        public enum FadeMethod
        {
            FromLeftToRight = 0,
            FromRightToLeft = 1,
            FromUpToDown = 2,
            FromDownToUp = 3
        }
        public void FadeOut(Control control, FadeMethod method, uint delay)
        {
            if (control != null)
            {
             
                //执行渐出
                this.control = control;
                Win32UIApi.AddWindowExstyle(control.Handle, 0, "WS_EX_LAYERED", 0); //添加分层属性。
                TransparentGradient transparentGradient = new TransparentGradient(control) { TransparentVal = 255 };
                Gradient gradient, gradient_transval;
                int end_val,start_val;
                switch (method)
                {
                    case FadeMethod.FromLeftToRight:
                        {
                            start_val = control.Left;
                            end_val = control.Left + distance;
                            gradient = new Gradient(control, "Left", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    case FadeMethod.FromRightToLeft:
                        {
                            start_val = control.Left;
                            end_val = control.Left - distance;
                            gradient = new Gradient(control, "Left", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    case FadeMethod.FromUpToDown:
                        {
                            start_val = control.Top;
                            end_val = control.Top + distance;
                            gradient = new Gradient(control, "Top", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    case FadeMethod.FromDownToUp:
                        {
                            start_val = control.Top;
                            end_val = control.Top - distance;
                            gradient = new Gradient(control, "Top", Gradient.ObjectMemberType.Property, control);
                            break;
                        }
                    default: throw new Exception("Invalid Value");
                }
                // PointF[] points = BezierAnimationPoints;
                PointF[] points = BezierAnimationOutPoints;
                gradient_transval = new Gradient(transparentGradient, "TransparentVal", Gradient.ObjectMemberType.Property, control);
                gradient_transval.Begin(0d, time, delay);

                gradient.GradientEnd += FadeOutEndCallBack;
                gradient.BeginBezier(end_val, time, points, delay);

                new Thread(new ThreadStart(() => { WaitAndRestore(gradient, start_val); })).Start();

                //触发事件
                FadeBegin?.Invoke(control);
            }
        }

        public void FadeOut(Control control, FadeMethod method)
        {
            FadeOut(control, method, 0);
        }

        /// <summary>
        /// 用于在FadeOut方法中等待动画线程退出将控件恢复原位。
        /// </summary>
        /// <param name="thread">要等待的动画线程。</param>
        /// <param name="tar">线程结束后要恢复到的值。</param>
        private void WaitAndRestore(Gradient gradient,int tar)
        {
            gradient.CurrentThread.Join();
            gradient.Begin(tar, 0);
        }
        
        /// <summary>
        /// 恢复控件的分层属性为原先状态。
        /// </summary>
        private void RestoreLayeredAttribute()
        {
            if(deleteLayeredAttribute)
            {
                Win32UIApi.DeleteWindowExstyle(control.Handle, 0, "WS_EX_LAYERED", 0);
            }
        }
        private void FadeInEndCallBack()
        {
            //触发结束事件
            FadeEnd?.Invoke(control);
            RestoreLayeredAttribute();
        }

        private void FadeOutEndCallBack()
        {
            FadeEnd?.Invoke(control);
            //RestoreLayeredAttribute();
        }




        /// <summary>
        /// 为透明度渐变提供反射对象。
        /// </summary>
        private class TransparentGradient
        {
            private byte transparentval;
            private Control control;
            public byte TransparentVal
            {
                get
                {
                    return transparentval;
                }
                set
                {
                    transparentval = value;
                    Win32UIApi.SetLayeredWindowAttributes(control.Handle, 0, value, Win32UIApi.LayeredMode.LWA_ALPHA);
                }
            }
            /// <summary>
            /// 初始化TransparentGradient类的实例。
            /// </summary>
            /// <param name="target">透明度操作的目标控件。</param>
            public TransparentGradient(Control target)
            {
                control = target;
            }
        }


    }
}
