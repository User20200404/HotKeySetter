using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeySetter
{
    public class ProgramIcons
    {
        public static Image UACIcon = Resources.Icon.LoadFromFile("user32.dll", 6);
        public static Image EnabledIcon = Resources.Icon.LoadFromFile("comres.dll", 8);
        public static Image DisabledIcon = Resources.Icon.LoadFromFile("comres.dll", 10);
        public static Image WarnIcon = Resources.Icon.LoadFromFile("SyncCenter.dll", 6);
        public static Image HotKeyFiredIcon = Resources.Icon.LoadFromFile("imageres.dll", 281);
        public static Image InfoIcon = Resources.Icon.LoadFromFile("aclui.dll", 3);
    }
}
