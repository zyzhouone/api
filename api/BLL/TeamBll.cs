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
    public class TeamBll : BaseBll
    {
        /// <summary>
        /// 查询队伍信息
        /// </summary>
        /// <param name="teamname"></param>
        /// <param name="company"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public PagedList<tblteamsVew> GetTeams(string teamname, string company, int pageindex)
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT t.*,u.mobile as Moblie,c.name as Companyname,l.name as Linename FROM tbl_teams t left join tbl_company c on c.companyid = t.company left join tbl_line l on l.lineid = t.lineid left join tbl_users u on u.userid = t.userid ");

                if (!string.IsNullOrEmpty(teamname))
                    sql.AppendFormat(" AND t.teamname like '%{0}%'", teamname);

                if (!string.IsNullOrEmpty(company))
                    sql.AppendFormat(" AND c.name like '%{0}%'", company);


                return db.SqlQuery<tblteamsVew, DateTime?>(sql.ToString(), pageindex, p => p.Createtime);
            }
        }

        /// <summary>
        /// 查询公司信息
        /// </summary>
        public List<SelectListItem> GetCompany()
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT  a.companyid  as value,a.name as text FROM tbl_company a order by a.name");

                return db.SqlQuery<SelectListItem>(sql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 查询线路信息
        /// </summary>
        public List<SelectListItem> GetLine()
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.lineid as value,a.name as text FROM tbl_line a order by a.name");

                return db.SqlQuery<SelectListItem>(sql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 根据teamname查询队伍
        /// </summary>
        /// <param name="Teamname"></param>
        /// <returns></returns>
        public tblteams GetTeamByName(string Teamname)
        {
            using (var db = new BFdbContext())
            {
                return db.tblteams.FirstOrDefault(p => p.Teamname == Teamname);
            }
        }

        /// <summary>
        /// 根据id查询队伍
        /// </summary>
        /// <param name="Teamname"></param>
        /// <returns></returns>
        public tblteams GetTeamById(string teamid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblteams.FirstOrDefault(p => p.teamid == teamid);
            }
        }

        /// <summary>
        /// 新增队伍
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int AddTeam(tblteams ent)
        {
            using (var db = new BFdbContext())
            {
                if (db.tblteams.Any(u => u.Teamname == ent.Teamname))
                    throw new ValidException("Mobile", "此队伍名称已经存在！");
                return db.Insert<tblteams>(ent);
            }
        }

        /// <summary>
        /// 更新队伍
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public int EditTeam(tblteams ent)
        {
            using (var db = new BFdbContext())
            {
                tblteams team = db.tblteams.FirstOrDefault(p => p.teamid == ent.teamid);
                team.Userid = ent.Userid;
                team.Company = ent.Company;
                team.Lineid = ent.Lineid;
                team.Status = ent.Status;
                return db.Update<tblteams>(team);
            }
        }

        /// <summary>
        /// 删除队伍
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteTeam(List<string> ids)
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
                            tblteams ent = db.tblteams.FirstOrDefault(p => p.teamid == id);
                            ent.Status = 2;
                            db.TUpdate<tblteams>(ent);
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
