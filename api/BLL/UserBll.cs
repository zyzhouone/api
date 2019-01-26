using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DAL;
using Model;
using Utls;

namespace BLL
{
    public class UserBll : BaseBll
    {
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Telephone"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public PagedList<sysuser> GetUsers(string Username,string tel, int pageindex)
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.* FROM sys_user a WHERE delflag=0 ");

                if (!string.IsNullOrEmpty(Username))
                    sql.AppendFormat(" AND a.Username LIKE '{0}%'", Username);

                if (!string.IsNullOrEmpty(tel))
                    sql.AppendFormat(" AND a.Telphone LIKE '{0}%'", tel);


                return db.SqlQuery<sysuser, DateTime?>(sql.ToString(), pageindex, p => p.Modifydate);
            }
        }

        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="Telephone"></param>
        /// <returns></returns>
        public sysuser GetUser(string id)
        {
            using (var db = new BFdbContext())
            {
                return db.sysuser.FirstOrDefault(p => p.Userid == id);
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int AddUser(sysuser ent)
        {
            using (var db = new BFdbContext())
            {
                if (db.sysuser.Any(u => u.Username == ent.Username))
                    throw new ValidException("Username", "已存在此名称的用户！");
                ent.Delflag = false;
                ent.Createddate = DateTime.Now;
                ent.Modifydate = DateTime.Now;
                return db.Insert<sysuser>(ent);
            }
        }

        /// <summary>
        /// 更新用户新
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int EditUser(sysuser ent)
        {
            using (var db = new BFdbContext())
            {
                if (db.sysuser.Any(u => u.Username == ent.Username && u.Userid != ent.Userid))
                    throw new ValidException("Username", "已存在此名称的用户！");

                sysuser usr = db.sysuser.FirstOrDefault(p => p.Userid == ent.Userid);
                usr.Email = ent.Email;
                usr.Password = ent.Password;
                usr.Delflag = ent.Delflag;
                usr.Username = ent.Username;
                usr.Modifydate = DateTime.Now;
                return db.Update<sysuser>(usr);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteUser(List<string> ids)
        {
            using (var db = new BFdbContext())
            {
                int res = 0;

                using (var tx = db.BeginTransaction())
                {
                    try
                    {
                        foreach (string id in ids)
                        {
                            sysuser ent = db.sysuser.FirstOrDefault(p => p.Userid == id);
                            ent.Delflag = true;
                            db.TUpdate<sysuser>(ent);
                        }
                        db.SaveChanges();
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw ex;
                    }
                }

                return res;
            }
        }
    }
}
