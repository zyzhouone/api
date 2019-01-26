using System;
using System.Linq;
using System.Collections.Generic;

using DAL;
using Model;

namespace BLL
{
    public class AuthBll : BaseBll
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public sysuser Login(string name, string pwd)
        {
            using (BFdbContext db = new BFdbContext())
            {
                IEnumerable<sysuser> users = db.FindAll<sysuser>(p => p.Logincode == name && p.Password == pwd && p.Delflag == false);
                if (users.Count() < 1)
                    return null;
                else
                {
                    sysuser usr=users.First();
                    usr.Lastlogindate = DateTime.Now;
                    usr.Logincount = usr.Logincount.GetValueOrDefault(0) + 1;
                    db.Update<sysuser>(usr);

                    return usr;
                }
            }
        }
    }
}
