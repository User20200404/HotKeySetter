using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    /// <summary>
    /// 提供基于动态链接库的事件功能。
    /// </summary>
    public class DynamicEventInfo
    {
        //以下是动态获取热键事件的方法。 说明：byte参数要传入一个byte[]数组的首个byte(即byte[0])，用于接收字符串，然后使用Encoding.Default编码。
        [DllImport("HotKeys.dll", EntryPoint = "HotKeyEventMainDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static int EventMainDelegate(string dllName, string entryPoint, int index, string param);
        [DllImport("HotKeys.dll", EntryPoint = "HotKeyGetDescriptionDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static void GetDescriptionDelegate(string dllName, string entryPoint, int index, ref byte title, ref byte detail);
        [DllImport("HotKeys.dll", EntryPoint = "HotKeyEnumEventDelegate", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static DynamicEventEnumStatus EnumEventDelegate(string dllName, string entryPoint, int index, ref byte desFuncEntry, ref byte mainFuncEntry, ref byte errorFuncEntry);



        private List<string> mainFuncEntryPoint = new List<string>(), desFuncEntryPoint = new List<string>(), errorFuncEntryPoint = new List<string>();
        private List<EventDescription> eventDescriptions = new List<EventDescription>();
        private bool enabled;

        /// <summary>
        /// 指示了该实例是否从配置文件中加载。为确保数据处理正确性，请不要-在实例初始化完毕后再次修改该值。
        /// </summary>
        public bool LoadFromConfig;
        /// <summary>
        /// 指示了该项在配置文件中是否已启用。
        /// </summary>
        public bool Enabled
        {
            get
            {
                if (LoadFromConfig)
                    return enabled;
                else throw new InvalidOperationException("Enabled的值仅在实例从配置文件中读取时有效。");
            }
            set
            {
                if (LoadFromConfig)
                    enabled = value;
                else throw new InvalidOperationException("Enabled的值仅在实例从配置文件中读取时有效。");
            }
        }

        /// <summary>
        /// 动态链接库文件名。
        /// </summary>
        public string DLLName
        {
            get
            {
                return StringOperation.GetFileNameFromPath(DLLPath);
            }
        }



        /// <summary>
        /// 由用户提供的枚举函数的入口点或路径，请注意该值不一定正确。
        /// </summary>
        public string EnumFuncEntryPoint, DLLPath;
        /// <summary>
        /// 指示了枚举事件时，加载枚举函数是否成功。
        /// </summary>
        public DynamicEventEnumStatus EventEnumStatus;
        public List<EventDescription> EventDescriptions { get { return eventDescriptions; } }

        public List<string> MainFuncEntryPoint { get { return mainFuncEntryPoint; } }
        public List<string> DescribeFuncEntryPoint { get { return desFuncEntryPoint; } }
        public List<string> ErrorProviderFuncEntryPoint { get { return errorFuncEntryPoint; } }

        /// <summary>
        /// 初始化DynamicEventInfo类的实例。
        /// </summary>
        /// <param name="dllPath">动态链接库的路径。</param>
        /// <param name="enumFuncEntryPoint">热键事件枚举函数入口点。</param>
        public DynamicEventInfo(string dllPath,string enumFuncEntryPoint)
        {
            EnumFuncEntryPoint = enumFuncEntryPoint;
            DLLPath = dllPath;
            if (File.Exists(dllPath))
            {
                byte[] temp_des = new byte[256];
                byte[] temp_main = new byte[256];
                byte[] temp_error = new byte[256];
                string str_des = "", str_main = "", str_error = "";
                for (int i = 0; i <= 0 || str_main != ""; i++)
                {
                    EventEnumStatus = EnumEventDelegate(dllPath, enumFuncEntryPoint, i, ref temp_des[0], ref temp_main[0], ref temp_error[0]);
                    str_des = Encoding.Default.GetString(temp_des).Replace('\0', ' ').Trim();
                    str_main = Encoding.Default.GetString(temp_main).Replace('\0', ' ').Trim();
                    str_error = Encoding.Default.GetString(temp_error).Replace('\0', ' ').Trim();
                    if (str_main != "")                       //执行函数必须声明，否则不用获取描述函数和错误提供函数。
                    {
                        mainFuncEntryPoint.Add(str_main);

                        if (str_des != "")
                        {
                            desFuncEntryPoint.Add(str_des);
                        }
                        else desFuncEntryPoint.Add("[未定义]");

                        if (str_error != "")
                        {
                            errorFuncEntryPoint.Add(str_error);
                        }
                        else errorFuncEntryPoint.Add("[未定义]");

                        eventDescriptions.Add(new EventDescription(dllPath, str_des, i)); //获取事件描述文本。
                    }
                    Array.Clear(temp_des, 0, 256);
                    Array.Clear(temp_main, 0, 256);
                    Array.Clear(temp_error, 0, 256);
                }
            }
            else EventEnumStatus = DynamicEventEnumStatus.DLL_LOAD_FAILED;
        }


        /// <summary>
        /// 从配置文件获取DynamicEventInfo的实例。通过此方法初始化的DynamicEventInfo实例中将包含一个"Enabled"成员，其指示了加载的项在配置文件中是否启用。
        /// </summary>
        /// <param name="configPath">配置文件的路径。</param>
        /// <param name="loadEnabledOnly">如果传入true，则只会加载启用了的项。</param>
        /// <param name="loadValidOnly">如果传入true，则会去除DynamicEvent初始化未成功的项。</param>
        /// <returns>一个可包含多个DynamicEventInfo实例的列表。</returns>
        public static List<DynamicEventInfo> GetFromConfig(string configPath, bool loadEnabledOnly,bool loadValidOnly)
        {
            List<DynamicEventInfo> infos = new List<DynamicEventInfo>();
            string dllReferenceConfigPath = configPath + @"\Reference.ini";
            for (int i = 0; ; i++)
            {
                string appName = "Reference" + i.ToString();
                string filePath = "";
                string entryPoint = "";
                string enabled = "";
                try
                {
                    filePath = ConfigFile.ReadFromFile(dllReferenceConfigPath, appName, "FilePath");
                    entryPoint = ConfigFile.ReadFromFile(dllReferenceConfigPath, appName, "EntryPoint");
                    enabled = ConfigFile.ReadFromFile(dllReferenceConfigPath, appName, "Enabled");
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode == 2)
                    {
                        System.Diagnostics.Debug.WriteLine("引用读取结束。");
                    }
                    else throw ex;
                    break;
                }
                finally
                {
                    if (!loadEnabledOnly || enabled == "True") //如果 配置文件启用 或 要求加载全部配置
                    {
                        DynamicEventInfo info = new DynamicEventInfo(filePath, entryPoint) { LoadFromConfig = true, enabled = (enabled == "True") };
                        if(!loadValidOnly|| info.EventEnumStatus == DynamicEventEnumStatus.SUCCESS) //如果允许加载非法项目 或 加载完全成功。
                        {
                            infos.Add(info);
                        }
                    }
                }
            }
            return infos;
        }

        /// <summary>
        /// 提供动态链接库事件的文本描述信息。
        /// </summary>
        public class EventDescription
        {
            byte[] temp_title = new byte[256];
            byte[] temp_detail = new byte[1024];
            string str_title, str_detail;
            /// <summary>
            /// 获取事件描述的标题。
            /// </summary>
            public string Title
            {
                get { return str_title; }
            }
            /// <summary>
            /// 获取事件描述的详细文本。
            /// </summary>
            public string Detail
            {
                get
                {
                    return str_detail;
                }
            }
            public EventDescription(string dllPath , string descriptionEntry , int index)
            {
                GetDescriptionDelegate(dllPath, descriptionEntry, index, ref temp_title[0], ref temp_detail[0]);
                str_title = Encoding.Default.GetString(temp_title).Replace('\0', ' ').Trim();
                str_detail = Encoding.Default.GetString(temp_detail).Replace('\0', ' ').Trim();
            }
        }

        /// <summary>
        /// 枚举事件时用于判断操作是否成功的状态值。
        /// </summary>
        public enum DynamicEventEnumStatus
        {
            SUCCESS = 0,
            DLL_LOAD_FAILED = 1,
            FUNC_LOAD_FAILED = 2
        }
    }
}
