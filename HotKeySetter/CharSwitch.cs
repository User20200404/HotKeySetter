using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HotKeySetter
{
    public class CharSwitch
    {
       public static bool IsNumber(string val)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9]*$");
            if (val == "")
                return false;
            return (regex.IsMatch(val));
        }
        public static bool IsValidVirtualKeyCode(string val)
        {
            if (IsNumber(val))
            {
                int convert = int.Parse(val);
                if (convert <= 254 && convert > 0)
                    return true;
            }
            return false;
        }
    }
}
