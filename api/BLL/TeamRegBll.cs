using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DAL;
using Model;
using Utls;

namespace BLL
{
    public class TeamRegBll : BaseBll
    {
        /// <summary>
        /// 团队注册1步
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int Step1(string mobile)
        {
            using (var db = new BFdbContext())
            {
                tblusers usr = new tblusers();
                usr.Mobile = mobile;
                usr.mono = VerifyCode.Get6SzCode();
                usr.Status = 6;

                return db.Insert<tblusers>(usr);
            }
        }

        /// <summary>
        /// 检查验证码是否正确
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="vercode"></param>
        /// <returns></returns>
        public int CheckSms(string mobile, string vercode, string matchid)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.mono == vercode && p.Status == 6);
                if (usr == null)
                    return -1;
                else
                {
                    if (db.tblmatchusers.Any(p => p.Match_Id == matchid && p.Mobile == mobile && p.Leader == 1))
                        return -2;

                    return 0;
                    //return usr.Id.Value;

                }
            }
        }

        /// <summary>
        /// 检查队伍名称是否重复
        /// </summary>
        /// <param name="matchid"></param>
        /// <param name="tname"></param>
        /// <returns></returns>
        public bool CheckTname(string matchid, string tname)
        {
            using (var db = new BFdbContext())
            {
                return db.tblteams.Any(p => p.match_id == matchid && p.Teamname == tname);
            }
        }

        /// <summary>
        /// 注册队伍
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <param name="tname"></param>
        /// <param name="tcom"></param>
        /// <returns></returns>
        public int RegTname(string id, string tid, string tname, string tcom, string pwd)
        {
            using (var db = new BFdbContext())
            {
                if (db.tblteams.Any(p => p.match_id == tid && p.Teamname == tname))
                    return -1;

                int uid = int.Parse(id);

                var usr = db.tblusers.FirstOrDefault(p => p.userid == uid.ToString());
                if (usr != null)
                {
                    usr.Passwd = pwd;
                    db.Update<tblusers>(usr);
                }

                tblteams tm = new tblteams();
                //tm.Company = string.IsNullOrEmpty(tcom) ? "个人" : tcom;
                tm.Createtime = DateTime.Now;
                tm.Eventid = 1;
                tm.Lineid = "";
                tm.match_id = tid;
                tm.Status = 6;
                tm.Teamname = tname;
                tm.Teamno = "";
                tm.Userid = id;

                db.Insert<tblteams>(tm);

                return db.SqlQuery<autoid>("select @@identity as id").FirstOrDefault().Id;
            }
        }

        public List<tblline> GetLines()
        {
            using (var db = new BFdbContext())
            {
                //return db.tblline.Where(p => p.Id < 7).ToList();
                return db.tblline.ToList();
            }
        }

        public tblline GetLineById(int tid)
        {
            using (var db = new BFdbContext())
            {
                return db.SqlQuery<tblline>("select a.* from tbl_line a,tbl_teams b where a.id=b.lineid and b.id=" + tid).FirstOrDefault();
            }
        }

        /// <summary>
        /// 选择路线
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lid"></param>
        /// <returns></returns>
        public int SelectLine(string id, string lid)
        {
            using (var db = new BFdbContext())
            {
                int teamid = int.Parse(id);
                var team = db.tblteams.FirstOrDefault(p => p.teamid == id);
                if (team == null)
                    return -1;

                team.Lineid = lid;//int.Parse(lid);
                return db.Update<tblteams>(team);
            }
        }

        /// <summary>
        /// 录入队员信息
        /// </summary>
        /// <param name="mus"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int InputMb(List<tblmatchusers> mus, int tid)
        {
            using (var db = new BFdbContext())
            {
                //var team = db.tblteams.FirstOrDefault(p => p.Id == tid);
                var team = db.tblteams.FirstOrDefault(p => p.teamid == tid.ToString());
                if (team == null)
                    return -1;

                var user = db.tblusers.FirstOrDefault(p => p.userid == team.Userid);

                int nov = 1;

                foreach (var item in mus)
                {
                    if (item.Cardtype == "1")
                    {
                        if (string.IsNullOrEmpty(item.Cardno))
                            item.Age = 0;
                        else if (item.Cardno.Length == 18)
                        {
                            string dy = item.Cardno.Substring(6, 8);
                            string nw = DateTime.Now.ToString("yyyyMMdd");
                            string m = (int.Parse(nw) - int.Parse(dy)).ToString();

                            if (m.Length > 4)
                                item.Age = int.Parse(m.Substring(0, m.Length - 4));
                            else
                                item.Age = 0;
                        }
                        else if (item.Cardno.Length == 15)
                        {
                            string dy = item.Cardno.Substring(6, 6);
                            if (dy.StartsWith("0") || dy.StartsWith("1") || dy.StartsWith("2"))
                                dy = "20" + dy;
                            else
                                dy = "19" + dy;

                            string nw = DateTime.Now.ToString("yyyyMMdd");
                            string m = (int.Parse(nw) - int.Parse(dy)).ToString();

                            if (m.Length > 4)
                                item.Age = int.Parse(m.Substring(0, m.Length - 4));
                            else
                                item.Age = 0;
                        }
                        else
                            item.Age = 0;

                    }
                    item.Createtime = DateTime.Now.ToString("yyyy=MM-dd HH:mm:ss");
                    item.Match_Id = team.match_id;
                    item.Teamid = tid.ToString();
                    item.Teamname = team.Teamname;

                    int tn = 0;
                    if (int.TryParse(team.Teamno, out tn))
                        item.Teamno = tn;
                    else
                        item.Teamno = 0;

                    item.Pay = 1;
                    item.Pnov = team.Teamno + nov;
                    db.TInsert<tblmatchusers>(item);
                }

                team.Status = 0;
                db.TUpdate<tblteams>(team);

                if (user != null)
                {
                    user.Status = 0;
                    db.TUpdate<tblusers>(user);
                }

                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 团队导入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int ImpTeams(DataTable data)
        {
            using (var db = new BFdbContext())
            {
                tblusers usr = new tblusers();
                //usr.Mobile = mobile;
                usr.mono = VerifyCode.Get6SzCode();
                usr.Status = 6;

                return db.Insert<tblusers>(usr);
            }
        }

        public List<tblmatchdetailentity> GetMatchUsersByUidMid(string userid, string matchid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format(@"SELECT a.*,b.match_name,b.date4 as MatchDate,b.lineno,b.Teamstatus,b.teamno_t,b.Matchstatus from tbl_match_users a,
                            (SELECT a.teamid,a.match_id,b.match_name,b.date4,
                                /* concat(case when a.match_id='6a61b95b-2d5d-4373-abaf-bf4e4c438800' then d.line_no else CONCAT(d.line_no,'-',d.linename) end,if(c.teamno is null,'',if(c.teamno='0','',if(c.teamno='00000','',concat('[', c.teamno,']'))))) lineno, */
                                case when a.match_id='6a61b95b-2d5d-4373-abaf-bf4e4c438800' then d.line_no else CONCAT(d.line_no,'-',d.linename) end lineno,
                                concat(c.`status` ,'') Teamstatus,
                                b.status Matchstatus,
                                if(c.teamno is null,'-',if(c.teamno='0','-',if(c.teamno='00000','-',c.teamno))) as teamno_t 
                                from tbl_match_users a,tbl_match b,tbl_teams c,tbl_lines d
                                where a.match_id=b.match_id and a.teamid=c.teamid and c.linesid=d.lines_id and a.userid='{0}' and a.match_id='{1}') b
                            WHERE a.teamid=b.teamid and a.match_id=b.match_id and a.status<>'9' order by a.leader desc", userid, matchid);

                return db.SqlQuery<tblmatchdetailentity>(sql).ToList();
            }
        }

        public tblmatchentity GetMatchUserByUidMid(string userid, string matchid)
        {
            using (var db = new BFdbContext())
            {
                return db.SqlQuery<tblmatchentity>("select a.*,b.match_name,c.teamno as teamno_t,d.linename as Lineno,b.date4,b.logopic from tbl_match_users a,tbl_match b,tbl_teams c,tbl_lines d where a.match_id=b.match_id and a.teamid=c.teamid and c.linesid=d.lines_id and a.status='1' and a.userid='" + userid + "' and a.match_id='" + matchid + "' and c.eventid = 3").FirstOrDefault();
            }
        }

        public List<tblmatchentity> GetMatchUserByUidMid2(string userid, string matchid)
        {
            using (var db = new BFdbContext())
            {
                return db.SqlQuery<tblmatchentity>(string.Format(@"select a.*,b.match_name,c.teamno as teamno_t,d.linename as Lineno,b.date4,b.logopic
                            from tbl_match_users a,tbl_match b,tbl_teams c,tbl_lines d
                            where a.match_id=b.match_id and a.teamid=c.teamid and c.linesid=d.lines_id and a.status='1' and c.eventid = 3
                            and a.teamid in (select teamid from tbl_match_users where status='1' and userid='{0}' 
                            and match_id='{1}')  order by a.leader desc", userid, matchid)).ToList();
            }
        }
    }
}
