using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace HotKeySetter
{

    class Resources
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpFileName);
        public class Icon
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuload);
            [DllImport("shell32.dll", SetLastError = true)]
            static extern IntPtr ExtractIconA(IntPtr hinst, string exeFileName, uint iconIndex);

            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr DestroyIcon(IntPtr hIcon);

            /// <summary>
            /// 从一个可执行文件的资源中获取图标。
            /// </summary>
            /// <param name="fileName">文件路径。</param>
            /// <param name="iconIndex">图标索引。</param>
            /// <returns>Bitmap类型的图标。</returns>
            public static System.Drawing.Bitmap LoadFromFile(string fileName, int iconIndex)
            {
                IntPtr hIcon = ExtractIconA(IntPtr.Zero, fileName, (uint)iconIndex);
                if (hIcon != IntPtr.Zero)
                {
                    BitmapSource image = Imaging.CreateBitmapSourceFromHIcon(hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    System.Drawing.Bitmap bitmap = BitmapFromSource(image);
                    DestroyIcon(hIcon);
                    return bitmap;
                }
                else return null;
            }
            /// <summary>
            /// 从BitmapSource中获取Bitmap。
            /// </summary>
            /// <param name="bitmapsource">BitmapSource实例。</param>
            /// <returns>提取到的Bitmap图像。</returns>
            private static System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
            {
                System.Drawing.Bitmap bitmap;
                using (MemoryStream outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();

                    enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                    enc.Save(outStream);
                    bitmap = new System.Drawing.Bitmap(outStream);
                }
                return bitmap;
            }
        }
    }
}
