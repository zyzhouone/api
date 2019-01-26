using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DAL;
using Model;
using Utls;
using System.Web.Mvc;

namespace BLL
{
    public class MemberBll : BaseBll
    {
        /// <summary>
        /// 查询会员信息
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="tel"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public PagedList<tblusers> GetMembers(string id, string tel, int pageindex)
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.* FROM tbl_users a WHERE 1=1");

                if (!string.IsNullOrEmpty(id))
                    sql.AppendFormat(" AND a.userid = {0}", id);

                if (!string.IsNullOrEmpty(tel))
                    sql.AppendFormat(" AND a.mobile like '%{0}%'", tel);


                return db.SqlQuery<tblusers, DateTime?>(sql.ToString(), pageindex, p => p.Last_Time);
            }
        }

        /// <summary>
        /// 根据playerid查询会员
        /// </summary>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public tblusers GetMember(string userid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblusers.FirstOrDefault(p => p.userid == userid);
            }
        }

        /// <summary>
        /// 根据tel查询会员
        /// </summary>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public tblusers GetMemberByTel(string tel)
        {
            using (var db = new BFdbContext())
            {
                return db.tblusers.FirstOrDefault(p => p.Mobile == tel);
            }
        }

        /// <summary>
        /// 根据dictid查询字典
        /// </summary>
        /// <param name="dictid"></param>
        /// <returns></returns>
        public List<SelectListItem> GetDict(int dictid)
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.code as value,a.name as text FROM tbl_dict a WHERE 1=1");

                    sql.AppendFormat(" AND a.dictid = {0}", dictid);

                return db.SqlQuery<SelectListItem>(sql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 新增会员
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int AddMember(tblusers ent)
        {
            using (var db = new BFdbContext())
            {
                if (db.tblusers.Any(u => u.Mobile == ent.Mobile))
                    throw new ValidException("Mobile", "已存在此名称的电话！");
                ent.Last_Time = DateTime.Now;
                return db.Insert<tblusers>(ent);
            }
        }

        /// <summary>
        /// 更新会员
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int EditMember(tblusers ent)
        {
            using (var db = new BFdbContext())
            {
                tblusers usr = db.tblusers.FirstOrDefault(p => p.userid == ent.userid);
                usr.Passwd = ent.Passwd;
                usr.sexy = ent.sexy;
                usr.cardtype = ent.cardtype;
                usr.cardno = ent.cardno;
                usr.birthday = ent.birthday;
                usr.Status = ent.Status;
                return db.Update<tblusers>(usr);
            }
        }

        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMember(List<string> ids)
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
                            tblusers ent = db.tblusers.FirstOrDefault(p => p.userid == id);
                            ent.Status = 2;
                            db.TUpdate<tblusers>(ent);
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
