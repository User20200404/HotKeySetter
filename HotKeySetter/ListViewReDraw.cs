using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace HotKeySetter
{
    class ListViewReDraw
    {
        private DrawListViewColumnHeaderEventArgs e_column;
        private DrawListViewItemEventArgs e_item;
        private DrawListViewSubItemEventArgs e_subitem;
        private CurrentObject currentObject;
        public bool ProvokeException = true;//指示了是否应该引发异常。
   
            public void DrawSubItemGridLine(Color GridLineColor)
            {
                CheckObjectType(CurrentObject.ListViewSubItem);
                Rectangle r = e_subitem.Bounds;
                e_subitem.Graphics.DrawRectangle(new Pen(GridLineColor, 1f), r.X, r.Y, r.Width, r.Height);
            }

            public void DrawSubItemText(string FontFamily, Color TextColor_Normal, Color TextColor_Selected, float FontSize)
            {
                CheckObjectType(CurrentObject.ListViewSubItem);
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Near
                };
                Font font = new Font(FontFamily, FontSize, FontStyle.Regular);
                SolidBrush solidBrush;
                Color item_forecolor = e_subitem.Item.ForeColor;
                if (e_subitem.Item.Selected)
                    solidBrush = new SolidBrush(TextColor_Selected);
                else if (item_forecolor == Color.Blue || item_forecolor == Color.Gray) ///本段代码用于渲染标记的项目
                    solidBrush = new SolidBrush(item_forecolor);
                else solidBrush = new SolidBrush(TextColor_Normal);



                e_subitem.Graphics.DrawString( e_subitem.SubItem.Text, font, solidBrush, e_subitem.Bounds, sf);
            }
        
        public ListViewReDraw(DrawListViewColumnHeaderEventArgs eventargs)
        {
            if (eventargs is null) //null值检查
                throw new ArgumentNullException(nameof(eventargs));
            else
            {
                e_column = eventargs;
                currentObject = CurrentObject.ListViewHeader;
            }
        }
        public ListViewReDraw(DrawListViewItemEventArgs eventargs)
        {
            if (eventargs is null) //null值检查
                throw new ArgumentNullException(nameof(eventargs));
            else
            {
                e_item = eventargs;
                currentObject = CurrentObject.ListViewItem;
            }
        }
        public ListViewReDraw(DrawListViewSubItemEventArgs eventargs)
        {
            if (eventargs is null) //null值检查
                throw new ArgumentNullException(nameof(eventargs));
            else
            {
                e_subitem = eventargs;
                currentObject = CurrentObject.ListViewSubItem;
            }
        }
        /// <summary>
        ///重绘列表头分割条，返回实际绘制的分割条的长度。
        ///必须在绘制背景后绘制该分割条，否则该绘制会被覆盖。
        /// </summary>
        public int DrawColumnHeaderSpliter(Color SpliterColor)
        {
            CheckObjectType(CurrentObject.ListViewHeader);
            if (e_column.ColumnIndex != 0) //第一个不画分割条
                e_column.Graphics.DrawLine(new Pen(SpliterColor), e_column.Bounds.Location, new Point(e_column.Bounds.X, e_column.Bounds.Y + e_column.Bounds.Height)); //绘制分割条
            return e_column.Bounds.Height;
        }
        /// <summary>
        ///重绘列表头背景颜色。
        /// </summary>
        /// <param name="BackgroundColor"></param>
        public void FillColumnHeaderBackground(Color BackgroundColor)
        {
            CheckObjectType(CurrentObject.ListViewHeader);
            e_column.Graphics.FillRectangle(new SolidBrush(BackgroundColor), e_column.Bounds); //填充背景
        }

        /// <summary>
        /// 绘制列表头字体。
        /// 必须在绘制背景后绘制文本，否则该绘制会被覆盖。
        /// </summary>
        /// <param name="FontFamily"></param>
        /// <param name="TextColor"></param>
        /// <param name="FontSize"></param>
        public void DrawColumnHeaderText(string FontFamily, Color TextColor, float FontSize)
        {
            CheckObjectType(CurrentObject.ListViewHeader);
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Near
            };
            Font font = new Font(FontFamily, FontSize, FontStyle.Regular);      //设置字体大小
            e_column.Graphics.DrawString(e_column.Header.Text, font, new SolidBrush(TextColor), e_column.Bounds, sf); //设置字体颜色
        }
        public void DrawItemText(string FontFamily, Color TextColor_Normal, Color TextColor_Selected, float FontSize)
        {
            CheckObjectType(CurrentObject.ListViewItem);
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Near
            };
            Font font = new Font(FontFamily, FontSize, FontStyle.Regular);
            e_item.Graphics.DrawString(e_item.Item.Text, font, new SolidBrush(TextColor_Normal), e_item.Bounds, sf);
        }
        public void FillItemBackground(Color BackgroundColor)
        {
            CheckObjectType(CurrentObject.ListViewItem);
        }
     
        /// <summary>
        /// 当前操作的ListView子对象类型
        /// </summary>
        enum CurrentObject
        {
            ListViewHeader = 0,
            ListViewItem   = 1,
            ListViewSubItem = 2
        }
        /// <summary>
        /// 将当前操作的ListView子对象类型以文本方式展示。
        /// </summary>
        /// <param name="c">当前操作的ListView子对象类型。</param>
        /// <returns>若对象类型有效，返回其文本描述。若对象无效，返回空文本。</returns>
        private string TranslateObjectType(CurrentObject c)
        {
            switch(c)
            {
                case CurrentObject.ListViewHeader:
                    return "ListViewHeader(列表头)";
                case CurrentObject.ListViewItem:
                    return "ListViewItem(列表项)";
                case CurrentObject.ListViewSubItem:
                    return "ListViewSubItem(列表子项)";
            }
            return ""; //没有找到对应项，返回空文本
        }
        /// <summary>
        /// 检查提供的重绘对象类型是否符合重绘类型。
        /// <param name="ExpectedType">期望的操作类型。</param>
        /// </summary>
        private void CheckObjectType(CurrentObject ExpectedType)
        {
            if(ExpectedType != currentObject && ProvokeException)
            {
                throw new Exception("发生重绘错误。\n该操作仅和 " + TranslateObjectType(ExpectedType) + " 类型兼容,而初始化的对象操作类型为 " + TranslateObjectType(currentObject) + "。");
            }
        }
    }
}
