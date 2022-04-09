using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    /// <summary>
    /// 应与SettingsForm的项目同步。
    /// </summary>
    public class Settings
    {
        public Other Others;
        public Base Bases;
        public Settings()
        {
            Others = new Other();
            Bases = new Base();
            ReFresh();
        }

        /// <summary>
        /// 提供设置的加载和保存接口。
        /// </summary>
        public interface ISettingsItem
        {
            void LoadFromFile(string dir);
            void SaveToFile(string dir);
        }

        /// <summary>
        /// 刷新设置信息。
        /// </summary>
        public void ReFresh()
        {
            try
            {
                LoadFromFile(ConfigFile.HotKeySetterConfigDirectory);
            }
            catch(Win32Exception ex)
            {
                if (ex.NativeErrorCode != 2 && ex.NativeErrorCode != 3)
                    throw ex;
            }
          }

        private void LoadFromFile(string dir)
        {
            Others.LoadFromFile(dir);
            Bases.LoadFromFile(dir);
        }

        private void SaveToFile(string dir)
        {
            Others.SaveToFile(dir);
            Bases.SaveToFile(dir);
        }
        public void Save()
        {
            SaveToFile(ConfigFile.HotKeySetterConfigDirectory);
        }

        public class Other:ISettingsItem
        {
            public List<DynamicEventInfo> AllEventInfos;
            public Developer Developers;
            public Other()
            {
                AllEventInfos = new List<DynamicEventInfo>();
                Developers = new Developer();
            }
            public void SaveToFile(string dir)
            {
                //保存AllEventInfos
                string configPath = dir + @"\Reference.ini";
                File.Delete(configPath);
                for (int i = 0; i < AllEventInfos.Count; i++)
                {
                    string appName = "Reference" + i.ToString();
                    ConfigFile.WriteStatic(configPath, appName, "FilePath", AllEventInfos[i].DLLPath);
                    ConfigFile.WriteStatic(configPath, appName, "EntryPoint", AllEventInfos[i].EnumFuncEntryPoint);
                    ConfigFile.WriteStatic(configPath, appName, "Enabled", AllEventInfos[i].Enabled.ToString());
                }

                Developers.SaveToFile(dir);
            }
            public void LoadFromFile(string dir)
            {
                AllEventInfos = DynamicEventInfo.GetFromConfig(dir, false, true);
                Developers.LoadFromFile(dir);
            }

            public class Developer:ISettingsItem
            {
                public bool OpenProcessHook;
                public bool IgnoreException;
                public Developer()
                {    
                }
                public void LoadFromFile(string dir)
                {
                    string configPath = dir + @"\Developer.ini";
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "OpenProcessHook", "Enabled"),out OpenProcessHook);
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "IgnoreException", "Enabled"),out IgnoreException);
                }
                public void SaveToFile(string dir)
                {
                    string configPath = dir + @"\Developer.ini";
                    ConfigFile.WriteStatic(configPath, "OpenProcessHook", "Enabled", OpenProcessHook.ToString());
                    ConfigFile.WriteStatic(configPath, "IgnoreException", "Enabled", IgnoreException.ToString());
                }

            }
        }

        public class Base:ISettingsItem
        {
            public Animation Animations;
            public Notify Notifys;
            public ShortCut ShortCuts;
            public Base()
            {
                Animations = new Animation();
                Notifys = new Notify();
                ShortCuts = new ShortCut();
            }
            public void LoadFromFile(string dir)
            {
                Animations.LoadFromFile(dir);
                Notifys.LoadFromFile(dir);
                ShortCuts.LoadFromFile(dir);
            }
            public void SaveToFile(string dir)
            {
                Animations.SaveToFile(dir);
                Notifys.SaveToFile(dir);
                ShortCuts.SaveToFile(dir);
            }
            public class Animation: ISettingsItem
            {
                public bool WindowCreatedAnimation;
                public bool PageSwitchAnimation;
                public bool ResizeAnimation;
                public bool ResizeRefresh;
                public bool OneWayResizeAnimationOnly;
                public bool ColorGradient;
                public Animation()
                {

                }

                public void SaveToFile(string dir)
                {
                    string configPath = dir + @"\Animation.ini";
                    ConfigFile.WriteStatic(configPath, "WindowCreatedAnimation", "Enabled", WindowCreatedAnimation.ToString());
                    ConfigFile.WriteStatic(configPath, "PageSwitchAnimation", "Enabled", PageSwitchAnimation.ToString());
                    ConfigFile.WriteStatic(configPath, "ResizeAnimation", "Enabled", ResizeAnimation.ToString());
                    ConfigFile.WriteStatic(configPath, "OneWayResizeAnimationOnly", "Enabled", OneWayResizeAnimationOnly.ToString());
                    ConfigFile.WriteStatic(configPath, "ResizeRefresh", "Enabled", ResizeRefresh.ToString());
                    ConfigFile.WriteStatic(configPath, "ColorGradient", "Enabled", ColorGradient.ToString());
                }
                public void LoadFromFile(string dir)
                {
                    string configPath = dir + @"\Animation.ini";
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "WindowCreatedAnimation", "Enabled"), out WindowCreatedAnimation);
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "PageSwitchAnimation", "Enabled"), out PageSwitchAnimation);
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "ResizeAnimation", "Enabled"), out ResizeAnimation);
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "OneWayResizeAnimationOnly", "Enabled"), out OneWayResizeAnimationOnly);
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "ResizeRefresh", "Enabled"), out ResizeRefresh);
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "ColorGradient", "Enabled"), out ColorGradient);
                }
            }
            public class Notify : ISettingsItem
            {
                public bool ShowFiredNotify;
                public int NotifyMinSpan;
                public Notify()
                {
                }

                public void SaveToFile(string dir)
                {
                    string configPath = dir + @"\Notify.ini";
                    ConfigFile.WriteStatic(configPath, "ShowFiredNotify", "Enabled", ShowFiredNotify.ToString());
                    ConfigFile.WriteStatic(configPath, "NotifyMinSpan", "Value", NotifyMinSpan.ToString());
                }
                public void LoadFromFile(string dir)
                {
                    string configPath = dir + @"\Notify.ini";
                    bool.TryParse(ConfigFile.ReadFromFile(configPath, "ShowFiredNotify", "Enabled"), out ShowFiredNotify);
                    int.TryParse(ConfigFile.ReadFromFile(configPath, "NotifyMinSpan", "Value"), out NotifyMinSpan);
                }
            }
            public class ShortCut:ISettingsItem
            {
                /// <summary>
                /// 指示从启动参数加载文件时，是否隐藏主窗口。
                /// </summary>
                public bool LoadFileHide;
                public bool HOMEShowHide;
                public ShortCut()
                {

                }
                public void LoadFromFile(string dir)
                {
                    string configPath = dir + @"\ShortCut.ini";
                    LoadFileHide = bool.Parse(ConfigFile.ReadFromFile(configPath, "LoadFileHide", "Enabled"));
                    HOMEShowHide = bool.Parse(ConfigFile.ReadFromFile(configPath, "HOMEShowHide", "Enabled"));

                }
                public void SaveToFile(string dir)
                {
                    string configPath = dir + @"\ShortCut.ini";
                    ConfigFile.WriteStatic(configPath, "LoadFileHide", "Enabled", LoadFileHide.ToString());
                    ConfigFile.WriteStatic(configPath, "HOMEShowHide", "Enabled", HOMEShowHide.ToString());
                }
            }
        }


    }
}
