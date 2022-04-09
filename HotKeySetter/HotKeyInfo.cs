using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    /// <summary>
    /// 储存热键的信息。
    /// </summary>
    public class HotKeyInfo
    {
        public string Name, EventArgs,EventID;
        public int Count, ID,  DelayTime, MinSpan;
        public uint FirstKeyCode, SecondKeyCode;
        public HotKeyInfo(string name,int count,int id,uint firstKey,uint secondKey,int delay,int minSpan,string args,string eid)
        {
            Name = name;
            Count = count;
            EventArgs = args;
            ID = id;
            EventID = eid;
            FirstKeyCode = firstKey;
            SecondKeyCode = secondKey;
            DelayTime = delay;
            MinSpan = minSpan;
        }
    }
}
