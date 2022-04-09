using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    /// <summary>
    /// 提供双缓冲控件功能。
    /// </summary>
    public class MyControls
    {
        /// <summary>
        /// 实现ListView的双缓冲化，用于减少界面更新闪烁。
        /// </summary>
        public class DBListView:System.Windows.Forms.ListView
        {
            public DBListView()
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
                SetStyle(ControlStyles.EnableNotifyMessage, true);
            }
            protected override void OnNotifyMessage(Message m)
            {
                if (m.Msg != 0x14)
                    base.OnNotifyMessage(m);
            }
        }
    }
}
