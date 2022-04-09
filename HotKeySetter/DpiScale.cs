using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    public class DpiScale
    {
        public static float ScaleValue
        {
            get
            {
                Form fm = new Form();
                using (Graphics g = fm.CreateGraphics())
                {
                    float scale = g.DpiX / 96.0f;
                    return scale;
                }
            }
        }
    }
}
