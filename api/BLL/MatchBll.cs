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
    public class MatchBll : BaseBll
    {
        /// <summary>
        /// 查询赛事信息
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="tel"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public PagedList<tblmatch> GetMatchs(string matchname,string area2, int pageindex)
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.* FROM tbl_match a WHERE 1=1");

                if (!string.IsNullOrEmpty(matchname))
                    sql.AppendFormat(" AND a.match_name like like '%{0}%'", matchname);

                if (!string.IsNullOrEmpty(area2))
                    sql.AppendFormat(" AND a.area2 like '%{0}%'", area2);


                return db.SqlQuery<tblmatch, DateTime?>(sql.ToString(), pageindex, p => p.Date3);
            }
        }

        /// <summary>
        /// 根据matchid查询赛事
        /// </summary>
        /// <param name="playerid"></param>
        /// <returns></returns>
        public tblmatch GetMatchById(string id)
        {
            using (var db = new BFdbContext())
            {
                return db.tblmatch.FirstOrDefault(p => p.Match_id == id);
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
        /// 新增赛事
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int AddMatch(tblmatch ent)
        {
            using (var db = new BFdbContext())
            {
                return db.Insert<tblmatch>(ent);
            }
        }

        /// <summary>
        /// 更新赛事
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int EditMatch(tblmatch ent)
        {
            using (var db = new BFdbContext())
            {
                tblmatch match = db.tblmatch.FirstOrDefault(p => p.Match_id == ent.Match_id);
                match.Match_name = ent.Match_name;
                match.Content = ent.Content;
                match.Area1 = ent.Area1;
                match.Area2 = ent.Area2;
                match.Date1 = ent.Date1;
                match.Date2 = ent.Date2;
                match.Date3 = ent.Date3;
                match.Date4 = ent.Date4;
                match.Pic1 = ent.Pic1;
                match.Pic2 = ent.Pic2;
                match.Status = ent.Status;
                return db.Update<tblmatch>(match);
            }
        }

        /// <summary>
        /// 删除赛事
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMatch(List<string> ids)
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
                            tblmatch ent = db.tblmatch.FirstOrDefault(p => p.Match_id == id);
                            ent.Status = "2";
                            db.TUpdate<tblmatch>(ent);
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

        /// <summary>
        /// 查询线路信息
        /// </summary>
        /// <param name="linename"></param>
        /// <param name="players"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public PagedList<tblline> GetLines(string linename, int pageindex)
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.* FROM tbl_line a WHERE 1=1");

                if (!string.IsNullOrEmpty(linename))
                    sql.AppendFormat(" AND a.linename like like '%{0}%'", linename);

                return db.SqlQuery<tblline, DateTime?>(sql.ToString(), pageindex, p => p.Createtime);
            }
        }


        /// <summary>
        /// 新增线路
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int AddLine(tblline ent)
        {
            using (var db = new BFdbContext())
            {
                return db.Insert<tblline>(ent);
            }
        }

        /// <summary>
        /// 根据lineid查询线路
        /// </summary>
        /// <param name="lineid"></param>
        /// <returns></returns>
        public tblline GetLineById(string id)
        {
            using (var db = new BFdbContext())
            {
                return db.tblline.FirstOrDefault(p => p.Lineid == id);
            }
        }

        /// <summary>
        /// 更新线路
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int EditLine(tblline ent)
        {
            using (var db = new BFdbContext())
            {
                tblline line = db.tblline.FirstOrDefault(p => p.Lineid == ent.Lineid);
                line.Name = ent.Name;
                line.Content = ent.Content;
                line.Players = ent.Players;
                line.Count = ent.Count;
                line.Conditions = ent.Conditions;
                line.Status = ent.Status;
                return db.Update<tblline>(line);
            }
        }

        /// <summary>
        /// 删除线路
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteLine(List<string> ids)
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
                            tblline ent = db.tblline.FirstOrDefault(p => p.Lineid == id);
                            ent.Status = 2;
                            db.TUpdate<tblline>(ent);
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
