using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utls
{
    public class VerifyCode
    {
        private static string yqm = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        private static string yqm_s = "0123456789";

        /// <summary>
        /// 6位验证码
        /// </summary>
        /// <returns></returns>
        public static string Get6Code()
        {
            Random rd1 = new Random(DateTime.Now.Second + 1);
            Random rd2 = new Random(DateTime.Now.Second + 2);
            Random rd3 = new Random(DateTime.Now.Second + 3);
            Random rd4 = new Random(DateTime.Now.Second + 4);
            Random rd5 = new Random(DateTime.Now.Second + 5);
            Random rd6 = new Random(DateTime.Now.Second + 6);

            return string.Format("{0}{1}{2}{3}{4}{5}", yqm[rd1.Next(33)], yqm[rd2.Next(33)], yqm[rd3.Next(33)],
                        yqm[rd4.Next(33)], yqm[rd5.Next(33)], yqm[rd6.Next(33)]);
        }

        /// <summary>
        /// 8位验证码
        /// </summary>
        /// <returns></returns>
        public static string Get8Code()
        {
            Random rd1 = new Random(DateTime.Now.Second + 1);
            Random rd2 = new Random(DateTime.Now.Second + 2);
            Random rd3 = new Random(DateTime.Now.Second + 3);
            Random rd4 = new Random(DateTime.Now.Second + 4);
            Random rd5 = new Random(DateTime.Now.Second + 5);
            Random rd6 = new Random(DateTime.Now.Second + 6);
            Random rd7 = new Random(DateTime.Now.Second + 7);
            Random rd8 = new Random(DateTime.Now.Second + 8);

            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", yqm[rd1.Next(33)], yqm[rd2.Next(33)], yqm[rd3.Next(33)],
                        yqm[rd4.Next(33)], yqm[rd5.Next(33)], yqm[rd6.Next(33)], yqm[rd7.Next(33)], yqm[rd8.Next(33)]);
        }

        /// <summary>
        /// 6位验证码
        /// </summary>
        /// <returns></returns>
        public static string Get6SzCode()
        {
            Random rd1 = new Random(DateTime.Now.Second + 1);
            Random rd2 = new Random(DateTime.Now.Second + 2);
            Random rd3 = new Random(DateTime.Now.Second + 3);
            Random rd4 = new Random(DateTime.Now.Second + 4);
            Random rd5 = new Random(DateTime.Now.Second + 5);
            Random rd6 = new Random(DateTime.Now.Second + 6);

            return string.Format("{0}{1}{2}{3}{4}{5}", yqm_s[rd1.Next(9)], yqm_s[rd2.Next(9)], yqm_s[rd3.Next(9)],
                        yqm_s[rd4.Next(9)], yqm_s[rd5.Next(9)], yqm_s[rd6.Next(9)]);
        }

        /// <summary>
        /// 8位验证码
        /// </summary>
        /// <returns></returns>
        public static string Get8SzCode()
        {
            Random rd1 = new Random(DateTime.Now.Second + 1);
            Random rd2 = new Random(DateTime.Now.Second + 2);
            Random rd3 = new Random(DateTime.Now.Second + 3);
            Random rd4 = new Random(DateTime.Now.Second + 4);
            Random rd5 = new Random(DateTime.Now.Second + 5);
            Random rd6 = new Random(DateTime.Now.Second + 6);
            Random rd7 = new Random(DateTime.Now.Second + 7);
            Random rd8 = new Random(DateTime.Now.Second + 8);

            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", yqm_s[rd1.Next(9)], yqm_s[rd2.Next(9)], yqm_s[rd3.Next(9)],
                        yqm_s[rd4.Next(9)], yqm_s[rd5.Next(9)], yqm_s[rd6.Next(9)], yqm_s[rd7.Next(9)], yqm_s[rd8.Next(9)]);
        }
    }
}
