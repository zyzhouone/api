using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utls
{
    public class IDGenerator
    {
        private static Random rd = new Random();

        /// <summary>
        /// F开头订单号
        /// </summary>
        /// <returns></returns>
        public static string GetIdF()
        {
            int n = rd.Next(99);
            return string.Format("F{0}{1}", DateTime.Now.Ticks, n.ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// G开头订单号
        /// </summary>
        /// <returns></returns>
        public static string GetIdG()
        {
            int n = rd.Next(99);
            return string.Format("G{0}{1}", DateTime.Now.Ticks, n.ToString().PadLeft(2, '0'));
        }
    }
}
