using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotKeySetter
{
    public class DelayedEvent
    {
        private int delayTime,indexInDll;
        private string args;
        private string dllPath;
        private string eventFuncName;
        private DynamicEventInfo eventInfo;

        [DllImport("HotKeys.dll", EntryPoint = "HotKeyEventMainDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static int EventMainDelegate(string dllName, string entryPoint, int index, string param);

        [DllImport("HotKeys.dll", EntryPoint = "HotKeyEventErrorDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static ErrorReportMethod HotKeyEventErrorDelegate(string dllName, string entryPoint, int index, int errCode, ref byte errMsg);

        /// <summary>
        /// 初始化DelayedEvent类。
        /// </summary>
        /// <param name="type">事件ID。</param>
        /// <param name="param">事件参数。</param>
        /// <param name="delay">延迟执行的时间(ms)</param>
        public DelayedEvent(DynamicEventInfo eventInfo,string eventID,string args,int delayTime)
        {
            this.delayTime = delayTime;
            this.args = args;
            this.eventInfo = eventInfo;
            this.dllPath = StringOperation.GetSplittedString(eventID,"|",1);
            this.eventFuncName = StringOperation.GetSplittedString(eventID, "|", 2);
            this.indexInDll = int.Parse(StringOperation.GetSplittedString(eventID, "|", 4));
        }
        /// <summary>
        /// 延迟执行事件的方法。
        /// </summary>
        public void DelayedEventMain()
        {
            Thread.Sleep(delayTime); //延迟时间
            int ret = EventMainDelegate(dllPath, eventFuncName, indexInDll, args);
            byte[] temp_msg = new byte[256];
            ErrorReportMethod method = HotKeyEventErrorDelegate(dllPath, eventInfo.ErrorProviderFuncEntryPoint[indexInDll], indexInDll, ret, ref temp_msg[0]);
            string errMsg = Encoding.Default.GetString(temp_msg);
            ErrorReport(method, errMsg);
        }

        public void ErrorReport(ErrorReportMethod method,string value)
        {
            switch(method)
            {
                case ErrorReportMethod.ERR_REPORT_DO_NONE:
                    return;
                case ErrorReportMethod.ERR_REPORT_FATAL:
                    MessageBox.Show("在" + eventInfo.DLLName + "发生了一个致命错误：\n\n信息：" + value + "\n\nHotKeySetter现在将立即退出，详细信息请参阅日志文件。", "执行事件时发生致命错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-2); //发生致命错误
                    return;
                case ErrorReportMethod.ERR_REPORT_SHOW_MSGBOX:
                    MessageBox.Show(eventInfo.DLLName + "报告了一个错误：\n\n信息："+ value+"\n\n","执行事件时发生错误",MessageBoxButtons.OK,MessageBoxIcon.Warning); 
                    return;
            }
        }

        public enum ErrorReportMethod
        {
            ERR_REPORT_SHOW_MSGBOX = 1, //HotKeySetter应显示显式消息框，指示错误内容和所在dll。
            ERR_REPORT_WRITE_LOG = 2, //HotKeySetter应写出错误日志。
            ERR_REPORT_DO_NONE = 0, //不作任何操作。
            ERR_REPORT_THORW_EXCEPTION = 3, //HotKeySetter应抛出异常，指示了错误信息和所在dll。
            ERR_REPORT_FATAL = 4 //HotKeySetter出现致命错误，应立即退出。(避免使用本标志) 
        }
    }
}
