using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utls
{
    public class Common
    {
        public static int IntTime()
        {
            return int.Parse(DateTime.Now.ToString("yyMMddHHmmss"));
        }

        public static int IntTime(string format)
        {
            return int.Parse(DateTime.Now.ToString(format));
        }

        public static int IntTime(DateTime dt)
        {
            return int.Parse(dt.ToString("yyMMddHHmmss"));
        }

        public static int IntTime(DateTime dt, string format)
        {
            return int.Parse(dt.ToString(format));
        }
    }
}
