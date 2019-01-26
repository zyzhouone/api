using System.Web;

using Model;

namespace AppAPI
{
    public class UserLoginInfo
    {
        /// <summary>
        /// 保存登录信息
        /// </summary>
        /// <param name="usr"></param>
        public static void Add(tblusers usr)
        {
            HttpContext.Current.Session.Add("pk_sid_user", usr);
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public static tblusers Get()
        {
            return HttpContext.Current.Session["pk_sid_user"] as tblusers;
        }

        /// <summary>
        /// 清空会话
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}