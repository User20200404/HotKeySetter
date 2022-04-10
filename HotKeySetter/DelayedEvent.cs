using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
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
        [HandleProcessCorruptedStateExceptions] //应捕获C++ SEH异常
        public extern static int EventMainDelegate(string dllName, string entryPoint, int index, string param);

        [DllImport("HotKeys.dll", EntryPoint = "HotKeyEventErrorDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [HandleProcessCorruptedStateExceptions]
        public extern static ErrorReportMethod HotKeyEventErrorDelegate(string dllName, string entryPoint, int index, int errCode, ref byte errMsg);

        /// <summary>
        /// 初始化DelayedEvent类。
        /// </summary>
        /// <param name="type">事件ID。</param>
        /// <param name="param">事件参数。</param>
        /// <param name="delay">延迟执行的时间(ms)</param>
        public DelayedEvent(DynamicEventInfo eventInfo, string eventID, string args, int delayTime)
        {
            this.delayTime = delayTime;
            this.args = args;
            this.eventInfo = eventInfo;
            this.dllPath = eventInfo.DLLPath;
            this.indexInDll = int.Parse(StringOperation.GetSplittedString(eventID, "|", 4));
            this.eventFuncName = eventInfo.MainFuncEntryPoint[indexInDll];
        }
        /// <summary>
        /// 延迟执行事件的方法。
        /// </summary>
        public void DelayedEventMain()
        {
            Thread.Sleep(delayTime); //延迟时间
            int ret = EventMainDelegate(eventInfo.DLLPath, eventFuncName, indexInDll, args);
            if (Enum.IsDefined(typeof(EventExecuteStatus), ret)) //如果返回值定义在了执行状态中，说明发生了HotKeySetter组件调用错误。
            {
                ErrorReport(ErrorReportMethod.ERR_REPORT_CALL_FAILED, new Win32Exception(Marshal.GetLastWin32Error()).Message);
            }
            else
            {
                byte[] temp_msg = new byte[256];
                ErrorReportMethod method = HotKeyEventErrorDelegate(eventInfo.DLLPath, eventInfo.ErrorProviderFuncEntryPoint[indexInDll], indexInDll, ret, ref temp_msg[0]);
                string errMsg = Encoding.Default.GetString(temp_msg);
                ErrorReport(method, errMsg);
            }
        }

        public void ErrorReport(ErrorReportMethod method, string value)
        {
            switch (method)
            {
                case ErrorReportMethod.ERR_REPORT_DO_NONE:
                    return;
                case ErrorReportMethod.ERR_REPORT_FATAL:
                    MessageBox.Show("事件执行组件报告了一个致命性错误并要求HotKeySetter立即终止，这可能指示着程序堆栈已损坏。\n\n信息：" + value + "\n\nHotKeySetter现在将退出，详细信息请参阅日志文件。\n" + "本错误最初是由" + eventInfo.DLLName + "__" + eventFuncName + "引发的。", "执行事件时发生致命错误(ERR_REPORT_FATAL)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-2); //发生致命错误
                    return;
                case ErrorReportMethod.ERR_REPORT_SHOW_MSGBOX:
                    MessageBox.Show(eventInfo.DLLName + "报告了一个错误：\n\n信息：" + value + "\n\n", "执行事件时发生错误(ERR_REPORT_SHOW_MSGBOX)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                case ErrorReportMethod.ERR_REPORT_CALL_FAILED:
                    MessageBox.Show("无法执行事件组件。\n\n" + "原因：" + value, "调用动态链接库出现错误(ERR_REPORT_CALL_FAILED)",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
            }
        }

        public enum ErrorReportMethod
        {
            //以下为HotKeySetter错误报告方式
            ERR_REPORT_CALL_FAILED = -1,


            //以下为组件错误报告方式
            ERR_REPORT_SHOW_MSGBOX = 1, //HotKeySetter应显示显式消息框，指示错误内容和所在dll。
            ERR_REPORT_WRITE_LOG = 2, //HotKeySetter应写出错误日志。
            ERR_REPORT_DO_NONE = 0, //不作任何操作。
            ERR_REPORT_THORW_EXCEPTION = 3, //HotKeySetter应抛出异常，指示了错误信息和所在dll。
            ERR_REPORT_FATAL = 4 //HotKeySetter出现致命错误，应立即退出。(避免使用本标志) 
        }

        /// <summary>
        /// 事件的执行调用结果
        /// </summary>
        public enum EventExecuteStatus
        {
            EVENT_DLL_LOAD_FAILED = 1024,
            EVENT_FUNC_CALL_FAILED = 1025
        }
    }
}
