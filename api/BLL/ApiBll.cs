using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

using DAL;
using Model;
using Utls;

namespace BLL
{
    /// <summary>
    /// API接口信息
    /// </summary>
    public class ApiBll : BaseBll
    {
        /// <summary>
        /// 获取队伍信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblteams GetTeamById(string id)
        {
            using (var db = new BFdbContext())
            {
                return db.tblteams.FirstOrDefault(p => p.teamid == id);
            }
        }

        public List<tblmatch> GetMatcheByDt(string dt)
        {
            using (var db = new BFdbContext())
            {
                //string sql = "SELECT match_id,match_name,content,area1,area2,date1,date2,date3,date4,pic1,pic2,logopic,ispic,status,'' as notice,sort FROM tbl_match where ispic<>'2' order by sort desc,date4 desc";
                //return db.SqlQuery<tblmatch>(sql).ToList();
                string sql = @"SELECT match_id,match_name,content,area1,area2,date1,date2,date3,date4,pic1,pic2,logopic,ispic,status,'' as notice,sort FROM tbl_match where ispic<>'2'";
                if (dt == "1")
                {
                    sql += " and status<>'5'";
                    sql += " order by sort desc,date4";
                }
                else
                {

                    sql += " and status='5'";
                    sql += " order by sort desc,date4 desc";
                }

                return db.SqlQuery<tblmatch>(sql).ToList();
            }
        }
        public List<tblmatch> GetMatcheStart(string userid)
        {
            using (var db = new BFdbContext())
            {
                //string sql = "SELECT match_id,match_name,content,area1,area2,date1,date2,date3,date4,pic1,pic2,logopic,ispic,status,'' as notice,sort FROM tbl_match where ispic<>'2' order by sort desc,date4 desc";
                //return db.SqlQuery<tblmatch>(sql).ToList();
                string sql = @"SELECT match_id,match_name,content,area1,area2,date1,date2,date3,date4,pic1,pic2,logopic,ispic,status,'' as notice,sort FROM tbl_match where ispic<>'2'";
                sql += " and status in ('1','3') ";
                sql += " order by sort desc,date4";
                
                return db.SqlQuery<tblmatch>(sql).ToList();
            }
        }
        public tblusers Login(string name, string pwd,string typ)
        {
            using (BFdbContext db = new BFdbContext())
            {
                //IEnumerable<tblusers> users = db.FindAll<tblusers>(p => p.Mobile == name && p.Passwd == pwd && p.Status== '0');
                tblusers usr = db.tblusers.FirstOrDefault(p => p.Mobile == name && p.Passwd == pwd && p.Status == 0);
                if (usr == null)
                    return null;
                else
                {
                    //tblusers usr = users.First();
                    usr.Last_Time = DateTime.Now;
                    //usr.Logincount = usr.Logincount.GetValueOrDefault(0) + 1;
                    usr.DeviceToken = typ;
                    db.Update<tblusers>(usr);

                    return usr;
                }
            }
        }
        public int reg_step1(string mobile)
        {
            using (var db = new BFdbContext())
            {
                tblusers usr = new tblusers();
                usr.Mobile = mobile;
                usr.mono = VerifyCode.Get6SzCode();
                //测试注册所有验证码用默认值
                //usr.mono = "123456";
                usr.Status = 6;

                return db.Insert<tblusers>(usr);
            }
        }

        public List<tblinfomation> GetInformation(string userid)
        {
            using (var db = new BFdbContext())
            {
                string sql = "select * from tbl_infomation where userid='" + userid + "' order by createtime desc";
                return db.SqlQuery<tblinfomation>(sql).ToList();
            }
        }

        public tblinfomation GetInformationDetail(string infoid)
        {
            using (var db = new BFdbContext())
            {
                var info = db.tblinfomation.FirstOrDefault(p => p.Infoid == infoid);
                if (info != null && info.Status == "0")
                {
                    info.Status = "1";
                    db.Update<tblinfomation>(info);
                }
                return info;
            }
        }

        public datacount GetInformationCount(string userid)
        {
            using (var db = new BFdbContext())
            {
                //2个月内未读的信息
                string sql = "SELECT count(1) cnt FROM tbl_infomation where status='0' and date_add(createtime,interval 60 day)>now() and userid='" + userid + "'";
                return db.SqlQuery<datacount>(sql).FirstOrDefault();
            }
        }

        public int setreadmsg(string userid)
        {
            using (var db = new BFdbContext())
            {
                string sql = "update tbl_infomation set status='1' where status='0' and userid='" + userid + "'";
                return db.ExecuteSqlCommand(sql);
            }
        }

        public List<tbllines> GetLines(string lines_id)
        {
            using (var db = new BFdbContext())
            {
                //string sql = "select match_id from tbl_match where match_id in (select match_id from tbl_match_users where userid='" + userid + "' and status='0'） order by dt1";
                //var mc = db.SqlQuery<tblmatch>(sql).ToList();
                //string mc_id = mc.match_id;

                //string sql = "select * from tbl_lines where lines_id='044c06b7-60e3-11e6-a2c5-6c92bf312dd1'";
                string sql = "select * from tbl_lines where lines_id='" + lines_id + "' order by line_no";

                return db.SqlQuery<tbllines>(sql).ToList();
            }
        }

        public int GetPaycountByTeamid(string teamid)
        {
            using (var db = new BFdbContext())
            {
                //string sql = "select match_id from tbl_match where match_id in (select match_id from tbl_match_users where userid='" + userid + "' and status='0'） order by dt1";
                //var mc = db.SqlQuery<tblmatch>(sql).ToList();
                //string mc_id = mc.match_id;

                //string sql = "select * from tbl_lines where lines_id='044c06b7-60e3-11e6-a2c5-6c92bf312dd1'";
                string sql = "select a.* from tbl_lines a,tbl_teams b where a.lines_id=b.linesid and b.teamid='" + teamid + "'";

                var lines = db.SqlQuery<tbllinesview>(sql).FirstOrDefault();
                if (lines == null)
                    return 0;

                return lines.Paycount.Value;
            }
        }

        public List<tblpoints> GetPoints(string linesid)
        {
            using (var db = new BFdbContext())
            {
                string sql = "select * from tbl_points where lineguid='" + linesid + "' order by sort";
                return db.SqlQuery<tblpoints>(sql).ToList();
            }
        }

        public List<tblmatch> GetPicList(DateTime dt)
        {
            using (var db = new BFdbContext())
            {
                string sql = "select * from tbl_match where ispic='1'";
                return db.SqlQuery<tblmatch>(sql).ToList();

            }
        }

        public List<usermatch> GetUserMatch(string userid, string issuc)
        {
            using (var db = new BFdbContext())
            {
                string sql = "";
                sql += "select a.match_id,a.match_name,b.teamid,b.teamname,b.eventid,d.lines_id,d.linename,a.status ";
                sql += "from tbl_match as a,tbl_teams as b,tbl_match_users as c,tbl_lines as d ";
                sql += "where a.match_id=b.match_id ";
                sql += "and b.linesid=d.lines_id ";
                sql += "and b.teamid=c.teamid ";
                sql += "and c.status=1 ";
                sql += "and c.userid='" + userid + "'";
                if (issuc == "1")
                    sql += " and b.eventid=3";

                return db.SqlQuery<usermatch>(sql).ToList();

            }
        }

        public List<tblmatchpic> GetPicDetail(string match_id)
        {
            using (var db = new BFdbContext())
            {
                string sql = "select * from tbl_match_pics where match_id='" + match_id + "' order by id";
                return db.SqlQuery<tblmatchpic>(sql).ToList();

            }
        }

        //判断任务书,返回0为没有任务书,返回1为下载任务书,返回2为任务书已下载,返回3删除任务书,返回4上传任务书
        public List<usertask> TaskCheck(string userid)
        {
            using (var db = new BFdbContext())
            {
                //寻找当前比赛 match.status=4 寻找队伍状态 team.status=0
                string sql = "";
                sql += "select a.match_id,a.match_name,b.teamid,b.teamname,d.lines_id,d.linename,c.matchuserid,c.isdown,c.nickname,c.leader,b.teamno,a.tasklogo,a.logopic,date_format(a.date4,'%Y-%m-%d') date4 ";
                sql += "from tbl_match as a,tbl_teams as b,tbl_match_users as c,tbl_lines as d ";
                sql += "where a.match_id=b.match_id ";
                sql += "and b.linesid=d.lines_id ";
                sql += "and b.teamid=c.teamid ";
                sql += "and a.status='4' ";
                sql += "and b.status=0 ";
                sql += "and c.userid='" + userid + "'";

                //var usr = new usertask();
                //usr = db.SqlQuery<usertask>(sql).FirstOrDefault();

                //if (usr == null)
                //    return("0");

                //return (usr.isdown);

                return db.SqlQuery<usertask>(sql).ToList();

            }
        }

        //设置成已下载完成
        public string UpdateTask(string matchuserid)
        {
            using (var db = new BFdbContext())
            {
                string sql = "update tbl_match_users set isdown='2' where matchuserid='";
                sql += matchuserid + "'";

                int res = db.ExecuteSqlCommand(sql);

                return res.ToString();

            }
        }

        public string UploadRecord(string matchuserid, string pointid, DateTime pointtime)
        {
            using (var db = new BFdbContext())
            {

                //查询team表是否有数据

                tblteams tem = new tblteams();
                string sql = "select * from tbl_teams where teamid in (select teamid from tbl_match_users where matchuserid='" + matchuserid + "')";
                string tmid = "";
                tem = db.SqlQuery<tblteams>(sql).FirstOrDefault();
                if (tem == null)
                {
                    return ("-1");
                }
                else
                {
                    //判断team表point sort是否 > 提交的point sort
                    int res1;
                    tmid = tem.teamid;
                    if (string.IsNullOrEmpty(tem.Record))
                        tem.Record = "0";

                    if (tem.Record.Equals("0"))
                    {
                        tem.Record = pointid;
                    }
                    else
                    {
                        tblpoints pt = new tblpoints();
                        sql = "select * from tbl_points where pointid='" + tem.Record + "'";
                        pt = db.SqlQuery<tblpoints>(sql).FirstOrDefault();
                        if (pt == null)
                        {
                            tem.Record = pointid;
                        }
                        else
                        {
                            var st1 = pt.Sort;
                            sql = "select * from tbl_points where pointid='" + pointid + "'";
                            pt = db.SqlQuery<tblpoints>(sql).FirstOrDefault();
                            if (pt == null)
                            {
                                return ("-2");
                            }
                            else
                            {

                                var st2 = pt.Sort;
                                string typ = pt.Pointtype.ToString();
                                if (st2 >= st1)
                                {
                                    tem.Record = pointid;
                                    if (typ.Equals("2"))
                                    {
                                        tem.Eventid = 3;
                                    }
                                }
                            }

                        }

                    }

                    res1 = db.Update<tblteams>(tem);

                }

                //matchrecord新增记录
                tblmatchrecord usr = new tblmatchrecord();
                usr.recordid = Guid.NewGuid().ToString();
                usr.teamid = tmid;
                usr.pointid = pointid;
                usr.typ = "1";
                usr.upt_by = matchuserid;
                usr.matchuserid = matchuserid;
                usr.pointtime = pointtime;
                usr.createtime = DateTime.Now;
                usr.status = "0";

                int res = db.Insert<tblmatchrecord>(usr);
                return res.ToString();

            }
        }

        public string GetTeamRecord(string matchuserid)
        {
            using (var db = new BFdbContext())
            {

                string sql = "";
                sql += "select * from tbl_teams where teamid in (select teamid from tbl_match_users where matchuserid='" + matchuserid + "')";

                var usr = new tblteams();
                usr = db.SqlQuery<tblteams>(sql).FirstOrDefault();

                if (usr == null)
                    return ("0");

                return (usr.Record);

            }
        }

        public string UpdateTestRecord(string pointid)
        {
            using (var db = new BFdbContext())
            {
                string sql = "update tbl_teams set record='";
                sql += pointid + "' where teamid='1961c21c-52f3-4559-9c9f-a1a33b6df2e6'";

                int res = db.ExecuteSqlCommand(sql);

                return res.ToString();

            }
        }

        public string SetResult(string teamid, string match_id, string teamno, string userno, DateTime? starttime, DateTime? settime, string timeline)
        {
            using (var db = new BFdbContext())
            {
                tblresult urs = new tblresult();
                string sql = "select * from tbl_result where teamid='" + teamid + "'";

                //int res;
                urs = db.SqlQuery<tblresult>(sql).FirstOrDefault();

                tblresultdetail trd = new tblresultdetail();
                trd.detailid = Guid.NewGuid().ToString();
                trd.createtime = DateTime.Now;
                trd.match_id = match_id;
                trd.settime = settime;
                trd.starttime = starttime;
                trd.teamid = teamid;
                trd.teamno = teamno;
                trd.timeline = timeline;
                trd.userno = userno;
                db.TInsert<tblresultdetail>(trd);

                if (urs == null)
                {
                    tblresult urs1 = new tblresult();
                    //urs1.teamid = Guid.NewGuid().ToString();
                    urs1.teamid = teamid;
                    urs1.match_id = match_id;
                    urs1.userno = userno;
                    urs1.teamno = teamno;
                    urs1.starttime = starttime;
                    urs1.settime = settime;
                    urs1.timeline = timeline;
                    urs1.createtime = DateTime.Now;
                    urs1.status = "0";

                    db.TInsert<tblresult>(urs1);
                    return db.SaveChanges().ToString();
                }
                else
                {
                    urs.userno = userno;
                    if (userno.ToLower().StartsWith("daoda"))
                    {
                        urs.settime = settime;
                        //urs.timeline = timeline;
                    }
                    else if (userno.ToLower().StartsWith("chufa"))
                    {
                        urs.starttime = starttime;
                        //urs.timeline = "0";
                    }

                    if (urs.starttime.HasValue && urs.settime.HasValue)
                        urs.timeline = Math.Round((urs.settime.Value - urs.starttime.Value).TotalHours, 4).ToString();

                    urs.createtime = DateTime.Now;
                    urs.status = "0";

                    db.TUpdate<tblresult>(urs);
                    return db.SaveChanges().ToString();
                }
            }
        }

        public List<tblresult> GetResult(string userno,string match_id)
        {
            using (var db = new BFdbContext())
            {
                string sql = "select * from tbl_result where match_id='" + match_id + "' and userno <> '" + userno + "' order by teamno";
                return db.SqlQuery<tblresult>(sql).ToList();

            }
        }

        public List<tblmatch> GetScanMatch()
        {
            using (var db = new BFdbContext())
            {
                string sql = "select * from tbl_match where status='4' ";
                return db.SqlQuery<tblmatch>(sql).ToList();
            }
        }

        public List<teamresult> GetScanTeams(string match_id)
        {
            using (var db = new BFdbContext())
            {
                string sql = "select a.teamid,a.teamname,a.teamno,c.line_no,b.match_name,b.match_id from tbl_teams as a,tbl_match as b,tbl_lines as c";
                sql += " where a.match_id=b.match_id and a.linesid=c.lines_id and b.match_id='" + match_id + "' and teamno like '_____'";
                return db.SqlQuery<teamresult>(sql).ToList();
            }
        }

        public List<tblline> Getline(string match_id)
        {
            using (var db = new BFdbContext())
            {
                return db.tblline.Where(p => p.Match_id == match_id && p.Status != 9).ToList();
            }
        }

        public List<tbllines> GetlinesByMatchid(string match_id)
        {
            using (var db = new BFdbContext())
            {
                return db.tbllines.Where(p => p.Matchid == match_id).OrderBy(p => p.Lineno).ToList();
            }
        }

        public List<tbllines> GetlinesByLineid(string lineid)
        {
            using (var db = new BFdbContext())
            {
                return db.tbllines.Where(p => p.Lineid == lineid).OrderBy(p => p.Lineno).ToList();
            }
        }

        /// <summary>
        /// 团队注册1步
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int Step1(string mobile)
        {
            using (var db = new BFdbContext())
            {
                tblusers usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.Status == 0);
                int res = 0;
                if (usr == null)
                {
                    usr = new tblusers();
                    usr.Mobile = mobile;
                    usr.mono = VerifyCode.Get6SzCode();
                    usr.Status = 6;

                    res = db.Insert<tblusers>(usr);
                }
                else
                {
                    usr.mono = VerifyCode.Get6SzCode();
                    res = db.Update<tblusers>(usr);
                }
                if (res > 0)
                    SMSHepler.SendRegSms(mobile, usr.mono);

                return res;
            }
        }

        /// <summary>
        /// 检查验证码是否正确
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="vercode"></param>
        /// <returns></returns>
        public string CheckSms(string mobile, string vercode, string matchid)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.mono == vercode && (p.Status == 6 || p.Status == 0));
                if (usr == null)
                    return "-1";
                else
                {
                    if (db.tblmatchusers.Any(p => p.Match_Id == matchid && p.Mobile == mobile && p.Leader == 1))
                        return "-2";

                    return usr.userid;
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

        private void SetYearOld(tblmatchusers item)
        {
            try
            {
                //if (item.Cardtype == "1")
                {
                    if (item.birthday.HasValue)
                    {
                        string dy = item.birthday.Value.ToString("yyyyMMdd");
                        string nw = DateTime.Now.ToString("yyyyMMdd");
                        string m = (int.Parse(nw) - int.Parse(dy) + 1).ToString();

                        if (m.Length > 4)
                            item.Age = int.Parse(m.Substring(0, m.Length - 4));
                        else
                            item.Age = 0;
                    }
                    else
                        item.Age = 0;
                }
            }
            catch (Exception ex)
            {
                item.Age = 0;
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
        public string RegTname(string uid, string tid, string tname, string tcom, string pwd, ref string teamid)
        {
            using (var db = new BFdbContext())
            {
                if (db.tblteams.Any(p => p.match_id == tid && p.Teamname == tname))
                    return "-1";
                var tms = db.tblteams.FirstOrDefault(p => p.match_id == tid && p.Userid == uid);
                if (tms != null)
                {
                    teamid = tms.teamid;
                    return "-3";
                }

                var usr = db.tblusers.FirstOrDefault(p => p.userid == uid);
                if (usr == null || usr.Isupt == "0")
                    return "-2";

                tblteams tm = new tblteams();
                tm.Company = string.IsNullOrEmpty(tcom) ? "个人" : tcom;
                tm.Createtime = DateTime.Now;
                tm.Eventid = 1;
                tm.Lineid = "";
                tm.match_id = tid;
                tm.Status = 6;
                tm.Teamname = tname;
                tm.Teamno = "0";
                tm.Userid = uid;
                tm.teamid = Guid.NewGuid().ToString();
                tm.Teamtype = 0;
                tm.Type_ = "8";
                db.TInsert<tblteams>(tm);

                tblmatchusers m = new tblmatchusers();
                m.Cardno = usr.cardno;
                m.Cardtype = usr.cardtype;
                m.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                m.Leader = 1;
                m.Match_Id = tid;
                m.Matchuserid = Guid.NewGuid().ToString();
                m.Mobile = usr.Mobile;
                m.Nickname = usr.Name;
                m.Pay = 0;
                m.birthday = usr.birthday;

                SetYearOld(m);

                int sx = 1;
                if (int.TryParse(usr.sexy, out sx))
                    m.Sexy = sx;
                else
                    m.Sexy = 1;
                m.Status = "1";

                m.Teamid = tm.teamid;
                m.Teamname = tm.Teamname;
                m.Userid = usr.userid;
                db.TInsert<tblmatchusers>(m);

                db.SaveChanges();

                teamid = tm.teamid;

                return tm.teamid;
            }
        }

        public tblmatch GetMatchById(string tid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblmatch.FirstOrDefault(p => p.Match_id == tid);
            }
        }

        public tblteammatchusers GetTeamByum(string usrid, string matchid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format("select a.*,b.lineid from tbl_match_users a,tbl_teams b where a.teamid=b.teamid and (a.status='1' or a.leader=1) and a.userid='{0}' and a.match_id='{1}'", usrid, matchid);
                return db.SqlQuery<tblteammatchusers>(sql).FirstOrDefault();
            }
        }

        public tblmatchentity GetMatchByTeamid(string tid)
        {
            using (var db = new BFdbContext())
            {
                return db.SqlQuery<tblmatchentity>("select a.*,b.teamname,b.userid,b.status as Paystatus from tbl_match a,tbl_teams b where a.match_id=b.match_id and b.teamid='" + tid + "'").FirstOrDefault();
            }
        }

        public List<tblline> GetTLines(string mid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format("select a.* from tbl_line a,tbl_teams b where a.match_id=b.match_id and b.teamid='{0}'", mid);
                return db.SqlQuery<tblline>(sql).ToList();
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
                var team = db.tblteams.FirstOrDefault(p => p.teamid == id);
                if (team == null)
                    return -1;

                var lines = db.tbllines.FirstOrDefault(p => p.Linesid == lid);
                if (lines == null)
                    return -2;

                team.Linesid = lid;
                team.Lineid = lines.Lineid;
                return db.Update<tblteams>(team);
            }
        }

        public int ChangeLine(string userid, string teamid, string lid)
        {
            using (var db = new BFdbContext())
            {
                var team = db.tblteams.FirstOrDefault(p => p.teamid == teamid);
                if (team == null)
                    return -1;

                if (team.Userid != userid)
                    return -3;

                if (team.Status.Value != 1)
                    return -4;

                var lines = db.tbllines.FirstOrDefault(p => p.Linesid == lid);
                if (lines == null)
                    return -2;

                team.Linesid = lid;
                team.Lineid = lines.Lineid;

                //重置状态
                if (team.Status.Value == 1)
                {
                    tblorders order = db.tblorders.FirstOrDefault(p => p.Teamid == teamid);
                    order.Lineid = team.Lineid;
                    order.Match_Id = team.match_id;
                    order.Ordertotal = lines.Price;
                    order.Status = 0;

                    db.Update<tblorders>(order);

                    tblmatchextra extra = db.tblmatchextra.FirstOrDefault(p => p.teamid == teamid);
                    if (extra != null)
                        db.Delete<tblmatchextra>(extra);
                }

                team.Status = 6;
                return db.Update<tblteams>(team);
            }
        }

        public int cancelteam(string teamid, string userid)
        {
            using (var db = new BFdbContext())
            {
                var team = db.tblteams.FirstOrDefault(p => p.teamid == teamid);
                if (team == null)
                    return -1;

                if (team.Userid != userid)
                    return -2;

                if (team.Status.Value != 6 && team.Status.Value != 2)
                    return -3;

                using (var tran = db.BeginTransaction())
                {
                    string sql1 = string.Format("insert into tbl_match_users_del select *,now() from tbl_match_users where teamid='{0}'", teamid);
                    string sql2 = string.Format("insert into tbl_teams_del select *,now() from tbl_teams where teamid='{0}'", teamid);
                    string sql3 = string.Format("delete from tbl_match_users where teamid='{0}'", teamid);
                    string sql4 = string.Format("delete from tbl_teams where teamid='{0}'", teamid);

                    db.ExecuteSqlCommand(sql1);
                    db.ExecuteSqlCommand(sql2);
                    db.ExecuteSqlCommand(sql3);
                    db.ExecuteSqlCommand(sql4);

                    tran.Commit();
                }

                return 0;
            }
        }

        public tblusers GetUserByTeamId(string tid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format("select a.* from tbl_users a,tbl_teams b where a.userid=b.userid and b.teamid='{0}'", tid);
                return db.SqlQuery<tblusers>(sql).FirstOrDefault();
            }
        }

        public tblusers GetUserById(string uid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format("select a.* from tbl_users a where a.userid='{0}'", uid);
                return db.SqlQuery<tblusers>(sql).FirstOrDefault();
            }
        }

        public tblline GetLineById(string tid)
        {
            using (var db = new BFdbContext())
            {
                return db.SqlQuery<tblline>("select a.* from tbl_line a,tbl_teams b where a.lineid=b.lineid and b.teamid='" + tid + "'").FirstOrDefault();
            }
        }

        public int AddMatchuser(string tid, string mobile)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.Status == 0);
                if (usr == null)
                    return -1;

                if (db.tblmatchusers.Any(p => p.Userid == usr.userid && p.Teamid == tid))
                    return -2;

                var team = db.tblteams.FirstOrDefault(p => p.teamid == tid);
                if (team.Userid == usr.userid)
                    return -3;

                var player = 0;

                int? pr = db.tbllines.FirstOrDefault(p => p.Linesid == team.Linesid).Playercount;
                if (pr.HasValue)
                    player = pr.Value;

                var cnt = db.tblmatchusers.Count(p => p.Teamid == tid && p.Status != "9");
                if (cnt >= player)
                    return -4;

                var match = db.tblmatch.FirstOrDefault(p => p.Match_id == team.match_id);
                var dusr = db.tblusers.FirstOrDefault(p => p.userid == team.Userid);

                tblmatchusers m = new tblmatchusers();
                m.Cardno = usr.cardno;
                m.Cardtype = usr.cardtype;
                m.birthday = usr.birthday;

                SetYearOld(m);

                m.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                m.Leader = 0;
                m.Match_Id = team.match_id;
                m.Matchuserid = Guid.NewGuid().ToString();
                m.Mobile = usr.Mobile;
                m.Nickname = usr.Name;
                m.Pay = 0;

                int sx = 1;
                if (int.TryParse(usr.sexy, out sx))
                    m.Sexy = sx;
                else
                    m.Sexy = 1;
                m.Status = "2";
                m.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                m.Teamid = team.teamid;
                m.Teamname = team.Teamname;
                m.Userid = usr.userid;
                db.TInsert<tblmatchusers>(m);

                tblinfomation info = new tblinfomation();
                info.Context = string.Format("用户[{0}]邀请你加入[{1}]队伍,参加[{2}],赶快去看看并接受邀请吧.", dusr.Mobile, team.Teamname, match.Match_name);
                info.createtime = DateTime.Now;
                info.Infoid = Guid.NewGuid().ToString();
                info.Mobile = m.Mobile;
                info.Status = "0";
                info.Type = "3";
                info.Userid = m.Userid;
                info.Field1 = m.Matchuserid;
                info.Field2 = "0";
                db.TInsert<tblinfomation>(info);

                return db.SaveChanges();
            }
        }

        public int DelMatchuser(string teamid, string mobile, string userid)
        {
            using (var db = new BFdbContext())
            {
                if (!db.tblmatchusers.Any(p => p.Teamid == teamid && p.Userid == userid && p.Leader == 1))
                    return -1;

                return db.ExecuteSqlCommand(string.Format("delete from tbl_match_users where teamid='{0}' and mobile='{1}' and leader=0", teamid, mobile));
            }
        }


        public tblusers GetUByToken(string token)
        {
            using (var db = new BFdbContext())
            {
                return db.tblusers.FirstOrDefault(p => p.DeviceToken == token);
            }
        }

        public tblusers GetUById(string usrid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblusers.FirstOrDefault(p => p.userid == usrid);
            }
        }

        public List<tblmatchusers> GetMatchuserByTeamId(string tid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblmatchusers.Where(p => p.Teamid == tid).ToList();
            }
        }

        public tblmatchusers GetMatchuserByTeamIdUsrId(string tid, string usrid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblmatchusers.Where(p => p.Teamid == tid && p.Userid == usrid).FirstOrDefault();
            }
        }

        public string CompleteSign(string tid, string userid)
        {
            using (var db = new BFdbContext())
            {
                var team = db.tblteams.FirstOrDefault(p => p.teamid == tid);
                if (team == null)
                    return "-1";

                if (team.Status == 1 || team.Status == 0)
                    return "-14";

                var line = db.tbllines.FirstOrDefault(p => p.Linesid == team.Linesid);

                if (db.tblmatchusers.Any(p => p.Teamid == team.teamid && p.Status == "2"))
                    return "-99";

                var tblmusers = db.tblmatchusers.Where(p => p.Teamid == team.teamid && p.Status == "1");

                if (!tblmusers.Any(p => p.Sexy == 2 && p.Teamid == team.teamid))
                    return "-2";

                if (tblmusers.Any(p => p.Age < 16 && p.Teamid == team.teamid))
                    return "-3";

                if (tblmusers.Any(p => p.Age > 60 && p.Teamid == team.teamid))
                    return "-4";

                if (tblmusers.Any(p => p.Nickname == null && p.Teamid == team.teamid))
                    return "-5";

                if (tblmusers.Any(p => p.Cardno == null && p.Teamid == team.teamid))
                    return "-6";

                if (tblmusers.Any(p => p.Age == null))
                    return "-7";

                var lder = tblmusers.FirstOrDefault(p => p.Leader == 1);
                if (lder == null || lder.Userid != userid)
                    return "-8";

                if (tblmusers.Count() != line.Playercount.Value)
                    return "-9";

                //if (line.Status == 0)
                //{
                //    if (tblmusers.Count() != 5)
                //        return "-9";
                //}
                //else 
                if (line.Status == 1)
                {
                    //if (tblmusers.Count() != 4)
                    //    return "-10";

                    var ex = db.tblmatchextra.Where(p => p.teamid == team.teamid && p.extype == "2").FirstOrDefault();
                    if (ex == null)
                        return "-11";
                }
                else if (line.Status == 2)
                {
                    //if (tblmusers.Count() != 2)
                    //    return "-12";

                    var ex = db.tblmatchextra.Where(p => p.teamid == team.teamid && p.extype == "1").FirstOrDefault();
                    if (ex == null)
                        return "-13";
                }

                //var user = db.tblusers.FirstOrDefault(p => p.userid == team.Userid);
                var match = db.tblmatch.FirstOrDefault(p => p.Match_id == team.match_id);
                if (match.Status != "1")
                    return "-15";

                team.Status = 1;
                db.TUpdate<tblteams>(team);

                //添加订单
                tblorders order = new tblorders();
                order.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                order.Id = Guid.NewGuid().ToString();
                order.Lineid = team.Lineid;
                order.Match_Id = team.match_id;
                order.Orderid = IDGenerator.GetIdF();
                order.Ordertotal = line.Price;
                order.Status = 0;
                order.Teamid = team.teamid;
                order.Title = string.Format("[{0}]报名费用", match.Match_name);
                order.Userid = team.Userid;

                db.TInsert<tblorders>(order);

                db.SaveChanges();
                return "";
            }
        }

        public int UpdateDeviceToken(string userid, string DeviceToken)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.userid == userid);
                if (usr == null)
                    return -1;

                usr.DeviceToken = DeviceToken;

                return db.Update<tblusers>(usr);
            }
        }

        public int AcceptMatch(string infoid, string userid)
        {
            using (var db = new BFdbContext())
            {
                var info2 = db.tblinfomation.FirstOrDefault(p => p.Infoid == infoid);

                if (info2.Field2 != "0")
                    return -99;

                var tblm = db.tblmatchusers.FirstOrDefault(p => p.Matchuserid == info2.Field1);
                string teamid = tblm.Teamid;

                var musr = db.tblmatchusers.FirstOrDefault(p => p.Teamid == teamid && p.Userid == userid && p.Status == "2");
                if (musr == null)
                    return -1;
                else
                {
                    var ur = db.tblusers.FirstOrDefault(p => p.userid == musr.Userid);
                    if (string.IsNullOrEmpty(ur.Isupt) || ur.Isupt == "0")
                        return -3;

                    var lst = db.tblmatchusers.Where(p => p.Match_Id == musr.Match_Id && p.Userid == musr.Userid && p.Matchuserid != musr.Matchuserid);
                    //已经接受别的队伍
                    if (lst.Any(p => p.Status == "1"))
                        return -2;

                    string sql = string.Format("select a.* from tbl_users a,tbl_teams b where a.userid=b.userid and b.teamid='{0}'", musr.Teamid);
                    var leader = db.SqlQuery<tblusers>(sql).FirstOrDefault();

                    tblinfomation info = new tblinfomation();
                    info.Context = string.Format("用户[{0}]已经接受了你的邀请,赶快去报名吧.", musr.Mobile);
                    info.createtime = DateTime.Now;
                    info.Infoid = Guid.NewGuid().ToString();
                    info.Mobile = leader.Mobile;
                    info.Status = "0";
                    info.Type = "3";
                    info.Userid = leader.userid;
                    db.TInsert<tblinfomation>(info);

                    foreach (var item in lst)
                    {
                        item.Status = "9";
                        db.TUpdate<tblmatchusers>(item);
                    }

                    info2.Field2 = "1";
                    db.TUpdate<tblinfomation>(info2);

                    musr.Status = "1";
                    musr.birthday = ur.birthday;
                    SetYearOld(musr);
                    musr.Cardno = ur.cardno;
                    musr.Cardtype = ur.cardtype;
                    musr.Nickname = ur.Name;
                    musr.Mobile = ur.Mobile;

                    int sx = 0;
                    if (int.TryParse(ur.sexy, out sx))
                        musr.Sexy = sx;

                    db.TUpdate<tblmatchusers>(musr);

                    return db.SaveChanges();
                }
            }
        }

        public int RejectMatch(string infoid, string userid)
        {
            using (var db = new BFdbContext())
            {
                var info = db.tblinfomation.FirstOrDefault(p => p.Infoid == infoid);
                if (info.Field2 != "0")
                    return -99;

                var tblm = db.tblmatchusers.FirstOrDefault(p => p.Matchuserid == info.Field1);
                string teamid = tblm.Teamid;

                var musr = db.tblmatchusers.FirstOrDefault(p => p.Teamid == teamid && p.Userid == userid && p.Status == "2");
                if (musr == null)
                    return -1;
                else
                {
                    info.Field2 = "2";
                    db.TUpdate<tblinfomation>(info);

                    musr.Status = "9";
                    db.TUpdate<tblmatchusers>(musr);

                    return db.SaveChanges();
                }
            }
        }

        public int ReLeader(string lid, string mid)
        {
            using (var db = new BFdbContext())
            {
                var player = db.tblmatchusers.FirstOrDefault(p => p.Matchuserid == mid);

                var leader = db.tblmatchusers.FirstOrDefault(p => p.Userid == lid && p.Teamid == player.Teamid);
                if (leader.Leader != 1)
                    return -1;

                var team = db.tblteams.FirstOrDefault(p => p.teamid == leader.Teamid);

                leader.Leader = 0;
                leader.Mono = DateTime.Now.ToString("yyMMddHHmmss");

                player.Leader = 1;
                team.Userid = player.Userid;

                db.TUpdate<tblmatchusers>(leader);
                db.TUpdate<tblmatchusers>(player);
                db.TUpdate<tblteams>(team);

                return db.SaveChanges();
            }
        }
        public List<tblmatchextra> GetExtra(string teamid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblmatchextra.Where(p => p.teamid == teamid).ToList();
            }
        }

        public List<tbllines> GetMyLine(string teamid)
        {
            using (var db = new BFdbContext())
            {
                tblteams tms = new tblteams();
                string sql = "select * from tbl_teams where teamid='" + teamid + "'";

                tms = db.SqlQuery<tblteams>(sql).FirstOrDefault();

                return db.tbllines.Where(p => p.Linesid == tms.Linesid).ToList();
            }
        }

        public List<tblline> GetLineByMatchid(string matchid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblline.Where(p => p.Match_id == matchid).OrderBy(p => p.Sort).ToList();
            }
        }

        public List<tblorders> GetMyOrder(string teamid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblorders.Where(p => p.Teamid == teamid).ToList();
            }
        }

        public bool checkYear(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber))
                return false;
            if (idNumber.Length != 18)
                return false;

            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            //string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            //string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            //char[] Ai = idNumber.Remove(17).ToCharArray();
            //int sum = 0;
            //for (int i = 0; i < 17; i++)
            //{
            //    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            //}
            //int y = -1;
            //Math.DivRem(sum, 11, out y);
            //if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            //{
            //    return false;//校验码验证  
            //}
            return true;//符合GB11643-1999标准  
        }

        public int AddExtra(string type, string teamid, string info1, string info2, string info3)
        {
            using (var db = new BFdbContext())
            {
                //检查年龄
                if (type == "1")
                {
                    if (!checkYear(info2))
                        return -81;

                    string dy = info2.Substring(6, 8);
                    string nw = DateTime.Now.ToString("yyyyMMdd");
                    string m = (int.Parse(nw) - int.Parse(dy) + 1).ToString();

                    if (m.Length > 4)
                    {
                        int y = int.Parse(m.Substring(0, m.Length - 4));
                        if (y < 7 || y > 15)
                            return -80;
                    }
                    else
                        return -80;
                }

                var ex = db.tblmatchextra.FirstOrDefault(p => p.teamid == teamid);

                if (ex == null)
                {
                    ex = new tblmatchextra();
                    ex.Id = Guid.NewGuid().ToString();
                    ex.updt = DateTime.Now;
                    ex.extype = type;
                    ex.teamid = teamid;
                    ex.info1 = info1;
                    ex.info2 = info2;
                    ex.info3 = info3;

                    return db.Insert<tblmatchextra>(ex);
                }
                else
                {
                    ex.updt = DateTime.Now;
                    ex.extype = type;
                    ex.info1 = info1;
                    ex.info2 = info2;
                    ex.info3 = info3;

                    return db.Update<tblmatchextra>(ex);
                }
            }
        }

        public int UpdateExtra(string exid, string info1, string info2, string info3)
        {
            using (var db = new BFdbContext())
            {
                var ex = db.tblmatchextra.FirstOrDefault(p => p.Id == exid);

                //检查年龄
                if (ex.extype == "1")
                {
                    if (!checkYear(info2))
                        return -81;

                    string dy = info2.Substring(6, 8);
                    string nw = DateTime.Now.ToString("yyyyMMdd");
                    string m = (int.Parse(nw) - int.Parse(dy) + 1).ToString();

                    if (m.Length > 4)
                    {
                        int y = int.Parse(m.Substring(0, m.Length - 4));
                        if (y < 7 || y > 15)
                            return -80;
                    }
                    else
                        return -80;
                }

                ex.updt = DateTime.Now;
                ex.info1 = info1;
                ex.info2 = info2;
                ex.info3 = info3;

                return db.Update<tblmatchextra>(ex);
            }
        }

        public int DelExtra(string exid)
        {
            using (var db = new BFdbContext())
            {
                var ex = db.tblmatchextra.FirstOrDefault(p => p.Id == exid);
                return db.Delete<tblmatchextra>(ex);
            }
        }

        public tblteammatchusers GetMyMacth(string usrid, string matchid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format("select a.*,b.lineid,b.status as teamstatus from tbl_match_users a,tbl_teams b where a.teamid=b.teamid and a.userid='{0}' and a.match_id='{1}' and a.status<>9", usrid, matchid);
                return db.SqlQuery<tblteammatchusers>(sql).FirstOrDefault();
            }
        }


        public tblmatchuserstatus GetMyMacthStatus(string usrid, string matchid)
        {
            using (var db = new BFdbContext())
            {
                tblmatchuserstatus mn = new tblmatchuserstatus();

                string sql = string.Format("select a.*,b.linesid as lineid,b.status as teamstatus,b.teamtype,c.isfore,b.teamno as teamno2 from tbl_match_users a,tbl_teams b,tbl_match c where a.teamid=b.teamid and b.match_id=c.match_id and a.userid='{0}' and a.match_id='{1}' and a.status<>9", usrid, matchid);
                var d = db.SqlQuery<tblteammatchusers>(sql).ToList();
                var m = db.tblmatch.FirstOrDefault(p => p.Match_id == matchid);
                if (m != null)
                    mn.MacthStatus = m.Status;

                tblteammatchusers tusr;

                if (d == null || d.Count < 1)
                {
                    mn.Status = "1";
                    return mn;
                }
                else if (d.Count() > 1)
                {
                    if (d.Any(p => p.Status == "1"))
                    {
                        tusr = d.FirstOrDefault(p => p.Status == "1");
                    }
                    else
                    {
                        mn.Status = "4";
                        return mn;
                    }
                }
                else
                    tusr = d.FirstOrDefault();

                mn.TeamNo2 = tusr.Teamno2;
                mn.Teamname = tusr.Teamname;
                mn.TeamType = tusr.TeamType.HasValue ? tusr.TeamType.Value : 0;

                if (!string.IsNullOrEmpty(tusr.Lineid))
                {
                    var lines = db.tbllines.FirstOrDefault(p => p.Linesid == tusr.Lineid);
                    if (lines != null)
                        mn.Linesname = lines.Linename;
                }

                if ((tusr.teamstatus == 6 || tusr.teamstatus == 2) && (string.IsNullOrEmpty(tusr.Lineid)))
                {
                    mn.Status = "2";
                    mn.Teamid = tusr.Teamid;
                    mn.IsLeader = tusr.Leader.ToString();
                    return mn;
                }
                else if ((tusr.teamstatus == 6 || tusr.teamstatus == 2) && tusr.Leader == 1 && (!string.IsNullOrEmpty(tusr.Lineid)))
                {
                    mn.Status = "3";
                    mn.Teamid = tusr.Teamid;
                    mn.IsLeader = tusr.Leader.ToString();
                    return mn;
                }
                else if ((tusr.teamstatus == 6 || tusr.teamstatus == 2) && (tusr.Status == "2"))
                {
                    mn.Status = "4";
                    mn.Teamid = tusr.Teamid;
                    mn.IsLeader = tusr.Leader.ToString();
                    return mn;
                }
                else if ((tusr.teamstatus == 6 || tusr.teamstatus == 2) && (tusr.Status == "1"))
                {
                    mn.Status = "5";
                    mn.Teamid = tusr.Teamid;
                    mn.IsLeader = tusr.Leader.ToString();
                    return mn;
                }
                else if (tusr.teamstatus == 1)
                {
                    //如果是立即支付的比赛，则直接status=7
                    if (tusr.isfore == "1")
                        mn.Status = "6";
                    else
                        mn.Status = "8";

                    mn.Teamid = tusr.Teamid;
                    mn.IsLeader = tusr.Leader.ToString();
                    return mn;
                }
                else if (tusr.teamstatus == 0)
                {
                    mn.Status = "7";
                    mn.Teamid = tusr.Teamid;
                    mn.IsLeader = tusr.Leader.ToString();
                    return mn;
                }
                else
                    return mn;
            }
        }

        public int UpdateTeamName(string teamid, string tname)
        {
            using (var db = new BFdbContext())
            {
                var tm = db.tblteams.FirstOrDefault(p => p.teamid == teamid);
                if (tm == null)
                    return -1;
                else if ((tm.Status != 6) && (tm.Status != 2))
                    return -2;

                if (db.tblteams.Any(p => p.match_id == tm.match_id && p.teamid != teamid && p.Teamname == tname.Trim()))
                    return -3;

                tm.Teamname = tname;
                db.Update<tblteams>(tm);

                string sql = string.Format("update tbl_match_users set teamname='' where teamid=''", teamid, tname);
                return db.ExecuteSqlCommand(sql);
            }
        }

        public int Replayer(string usrid, string mobile, string mid)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.Status == 0);
                if (usr == null)
                    return -4;

                var musr = db.tblmatchusers.FirstOrDefault(p => p.Matchuserid == mid);
                if (musr == null || musr.Status != "1")
                    return -3;

                var match = db.tblmatch.FirstOrDefault(p => p.Match_id == musr.Match_Id);
                var leader = db.tblmatchusers.FirstOrDefault(p => p.Teamid == musr.Teamid && p.Userid == usrid);

                if (leader.Leader != 1)
                    return -2;

                if (db.tblmatchusers.Any(p => p.Match_Id == musr.Match_Id && p.Userid == usr.userid && p.Status == "1"))
                    return -1;

                tblreplace tbl = new tblreplace();
                tbl.Createtime = DateTime.Now;
                tbl.D_Age = musr.Age;
                tbl.D_Birthday = musr.birthday;
                tbl.D_Cardno = musr.Cardno;
                tbl.D_Cardtype = musr.Cardtype;
                tbl.D_Flag = "0";
                tbl.D_Matchuserid = musr.Matchuserid;
                tbl.D_Mobile = musr.Mobile;
                tbl.D_Nickname = musr.Nickname;
                tbl.D_Sexy = musr.Sexy;
                tbl.D_Userid = musr.Userid;
                tbl.Id = Guid.NewGuid().ToString();
                tbl.Match_Id = musr.Match_Id;
                tbl.S_Flag = "0";
                tbl.S_Userid = usr.userid;
                tbl.Teamid = musr.Teamid;

                db.TInsert<tblreplace>(tbl);

                musr.Status = "8";
                db.TUpdate<tblmatchusers>(musr);


                tblinfomation tblA = new tblinfomation();
                tblA.Context = string.Format("用户[{0}]邀请你加入[{1}]队伍,参加[{2}],请火速接受邀请吧.", leader.Mobile, leader.Teamname, match.Match_name);
                tblA.createtime = DateTime.Now;
                tblA.Field1 = tbl.Id;
                tblA.Field2 = "0";
                tblA.Infoid = Guid.NewGuid().ToString();
                tblA.Mobile = usr.Mobile;
                tblA.Status = "0";
                tblA.Type = "4";
                tblA.Userid = usr.userid;
                db.TInsert<tblinfomation>(tblA);

                tblinfomation tblB = new tblinfomation();
                tblB.Context = string.Format("你参加的[{0}],已被队长替换;如果接受,请点击【同意】按钮.", match.Match_name);
                tblB.createtime = DateTime.Now;
                tblB.Field1 = tbl.Id;
                tblB.Field2 = "0";
                tblB.Infoid = Guid.NewGuid().ToString();
                tblB.Mobile = musr.Mobile;
                tblB.Status = "0";
                tblB.Type = "4";
                tblB.Userid = musr.Userid;
                db.TInsert<tblinfomation>(tblB);

                return db.SaveChanges();
            }
        }

        public int AcceptReplay(string infoid)
        {
            using (var db = new BFdbContext())
            {
                var info = db.tblinfomation.FirstOrDefault(p => p.Infoid == infoid);
                if (info == null || info.Field2 != "0")
                    return -1;

                var rep = db.tblreplace.FirstOrDefault(p => p.Id == info.Field1);
                var leader = db.tblmatchusers.FirstOrDefault(p => p.Teamid == rep.Teamid && p.Leader == 1);

                string mobile = "";

                //需要替换队员的处理
                if (rep.S_Userid == info.Userid)
                {
                    var usr = db.tblusers.FirstOrDefault(p => p.userid == info.Userid);
                    if (usr.Isupt != "1")
                        return -2;

                    if (db.tblmatchusers.Any(p => p.Match_Id == rep.Match_Id && p.Userid == usr.userid && p.Status == "1"))
                        return -3;

                    var musr = db.tblmatchusers.FirstOrDefault(p => p.Matchuserid == rep.D_Matchuserid);
                    if (musr == null)
                        return -8;

                    mobile = usr.Mobile;

                    //被替换队员已经同意
                    if (rep.D_Flag == "1")
                    {
                        tblmatchusers tm = new tblmatchusers();
                        tm.birthday = usr.birthday;
                        tm.Cardno = usr.cardno;
                        tm.Cardtype = usr.cardtype;
                        tm.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        tm.Leader = 0;
                        tm.Match_Id = rep.Match_Id;
                        tm.Matchuserid = Guid.NewGuid().ToString();
                        tm.Mobile = usr.Mobile;
                        tm.Nickname = usr.Name;
                        tm.Pay = 0;
                        tm.Sexy = int.Parse(usr.sexy);
                        tm.Status = "1";
                        tm.Teamid = rep.Teamid;
                        tm.Teamname = musr.Teamname;
                        tm.Userid = usr.userid;
                        SetYearOld(tm);

                        if (tm.Age < 16 || tm.Age > 60)
                            return -9;

                        db.TInsert<tblmatchusers>(tm);
                        db.TDelete<tblmatchusers>(musr);

                        rep.S_Matchuserid = tm.Matchuserid;
                    }

                    rep.S_Agreetime = DateTime.Now;
                    rep.S_Flag = "1";
                    db.TUpdate<tblreplace>(rep);
                }
                else
                {
                    var usr = db.tblusers.FirstOrDefault(p => p.userid == rep.S_Userid);

                    mobile = rep.D_Mobile;

                    if (rep.S_Flag == "1")
                    {
                        var musr = db.tblmatchusers.FirstOrDefault(p => p.Matchuserid == rep.D_Matchuserid);
                        if (musr == null)
                            return -8;

                        tblmatchusers tm = new tblmatchusers();
                        tm.birthday = usr.birthday;
                        tm.Cardno = usr.cardno;
                        tm.Cardtype = usr.cardtype;
                        tm.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        tm.Leader = 0;
                        tm.Match_Id = rep.Match_Id;
                        tm.Matchuserid = Guid.NewGuid().ToString();
                        tm.Mobile = usr.Mobile;
                        tm.Nickname = usr.Name;
                        tm.Pay = 0;
                        tm.Sexy = int.Parse(usr.sexy);
                        tm.Status = "1";
                        tm.Teamid = rep.Teamid;
                        tm.Teamname = leader.Teamname;
                        tm.Userid = usr.userid;
                        SetYearOld(tm);

                        if (tm.Age < 16 || tm.Age > 60)
                            return -9;

                        db.TInsert<tblmatchusers>(tm);
                        db.TDelete<tblmatchusers>(musr);

                        rep.S_Matchuserid = tm.Matchuserid;
                    }

                    rep.D_Agreetime = DateTime.Now;
                    rep.D_Flag = "1";
                    db.TUpdate<tblreplace>(rep);
                }

                info.Field2 = "1";
                db.TUpdate<tblinfomation>(info);

                tblinfomation tblA = new tblinfomation();
                tblA.Context = string.Format("用户[{0}]接受了你的替换队员请求,请查看.", mobile);
                tblA.createtime = DateTime.Now;
                tblA.Infoid = Guid.NewGuid().ToString();
                tblA.Mobile = leader.Mobile;
                tblA.Status = "0";
                tblA.Type = "2";
                tblA.Userid = leader.Userid;
                db.TInsert<tblinfomation>(tblA);

                return db.SaveChanges();
            }
        }

        public int RejectReplay(string infoid)
        {
            using (var db = new BFdbContext())
            {
                var info = db.tblinfomation.FirstOrDefault(p => p.Infoid == infoid);
                if (info == null || info.Field2 != "0")
                    return -1;

                var rep = db.tblreplace.FirstOrDefault(p => p.Id == info.Field1);
                var musr = db.tblmatchusers.FirstOrDefault(p => p.Matchuserid == rep.D_Matchuserid);
                var leader = db.tblmatchusers.FirstOrDefault(p => p.Teamid == rep.Teamid && p.Leader == 1);

                if (musr == null)
                    return -2;

                if (info.Userid == rep.S_Userid)
                {
                    rep.S_Agreetime = DateTime.Now;
                    rep.S_Flag = "2";
                }
                else
                {
                    rep.D_Agreetime = DateTime.Now;
                    rep.D_Flag = "2";
                }

                info.Field2 = "2";
                musr.Status = "1";

                tblinfomation tblA = new tblinfomation();
                tblA.Context = string.Format("用户[{0}]拒绝了你替换队员的请求,请查看.", info.Mobile);
                tblA.createtime = DateTime.Now;
                tblA.Infoid = Guid.NewGuid().ToString();
                tblA.Mobile = leader.Mobile;
                tblA.Status = "0";
                tblA.Type = "2";
                tblA.Userid = leader.Userid;
                db.TInsert<tblinfomation>(tblA);

                db.TUpdate<tblinfomation>(info);
                db.TUpdate<tblreplace>(rep);
                db.TUpdate<tblmatchusers>(musr);

                return db.SaveChanges();
            }
        }

        public void PayReturn(string orderno, string tradeno, string buyeremail, string tradestatus)
        {
            using (var db = new BFdbContext())
            {
                BFParameters bf = new BFParameters();
                bf.Add("@orderno", orderno);
                bf.Add("@tradeno", tradeno);
                bf.Add("@buyeremail", buyeremail);
                bf.Add("@tradestatus", tradestatus);
                db.MysqlExecuteProcedure("sp_payreturn", bf);

                //string sql = "select * from tbl_users where userid in(select userid from tbl_teams where teamid in(select teamid from tbl_orders where orderid='" + orderno + "'))";
                //tblusers leader = new tblusers();
                //leader = db.SqlQuery<tblusers>(sql).FirstOrDefault();
                //if(leader!=null)
                //    if (tradestatus == "9000")
                //        SMSHepler.SendCommonSms(leader.Mobile, string.Format("[{0}]报名费用已经成功支付,感谢你的参与!请妥善保管帐号信息,等待查询比赛编组.", "2017中国坐标·上海城市定向户外挑战赛"));

            }
        }

        public string CheckPay(string oid, int paycnt, string matchid)
        {
            using (var db = new BFdbContext())
            {
                //string sql = "select id from tbl_orders where id='" + oid + "' and teamid in (select teamid from tbl_teams where linesid='207bfe24-bef3-4e1d-95dc-22228e0e04e8')";
                string sql = @"select * from tbl_teams where linesid not in('207bfe24-bef3-4e1d-95dc-22228e0e04e8','119eeb28-935e-479c-b157-1dd17e6427f6','7e34f192-f909-4f2d-9f37-99a9ad7df092','f0efcd04-10d0-4a96-91ea-1f2e7e374250')
                                and teamid in (select teamid from tbl_orders where id='" + oid + "')";

                var d = db.SqlQuery<tblteams>(sql).ToList();
                if (d.Count == 0)
                    return "99";

                BFParameters bf = new BFParameters();
                bf.Add("@orderid", oid);
                bf.Add("@tcnt", paycnt, DbType.Int32);
                bf.Add("@matchid", matchid);
                bf.Add("@msg", null, DbType.String, ParameterDirection.Output);
                db.MysqlExecuteProcedure("sp_paycheck", bf);

                return bf.GetOutParameter("@msg").ToString();
            }
        }

        public tblorders GetOrderByTeamId(string tid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblorders.FirstOrDefault(p => p.Teamid == tid);
            }
        }

        public int changeteam(string userid, string teamid, string teamname)
        {
            using (var db = new BFdbContext())
            {
                var team = db.tblteams.FirstOrDefault(p => p.teamid == teamid);

                if (team == null)
                    return -1;

                if (team.Status.Value == 0)
                    return -2;

                if (team.Userid != userid)
                    return -4;

                if (db.tblteams.Any(p => p.teamid != teamid && p.match_id == team.match_id && p.Teamname == teamname))
                    return -3;

                string sql = string.Format("update tbl_match_users set teamname='{0}' where teamid='{1}'", teamname, teamid);
                db.ExecuteSqlCommand(sql);

                team.Teamname = teamname;
                return db.Update<tblteams>(team);
            }
        }

        public List<tbladprint> GetAD()
        {
            using (var db = new BFdbContext())
            {
                string sql = "select * from tbl_ad_print order by sort";
                return db.SqlQuery<tbladprint>(sql).ToList();
            }
        }

        public bool CheckSignedOn(string matchid, string userid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblsignedon.Any(p => p.matchid == matchid && p.Userid == userid);
            }
        }

        public int SignIn(tblsignedon sgo)
        {
            using (var db = new BFdbContext())
            {
                var m = db.tblmatch.FirstOrDefault(p => p.Match_id == sgo.matchid);
                if (m == null)
                    return -3;

                if (m.Status != "8")
                    return -2;

                var mu = db.tblmatchusers.FirstOrDefault(p => p.Match_Id == sgo.matchid && p.Userid == sgo.Userid);
                if (mu == null)
                    return -1;

                if (db.tblsignedon.Any(p => p.matchid == sgo.matchid && p.Userid == sgo.Userid))
                    return -9;

                sgo.teamid = mu.Teamid;

                return db.Insert<tblsignedon>(sgo);
            }
        }


        public void ttest()
        {
            using (var db = new BFdbContext())
            {
                int cnt = 3;
              var ents=  db.SqlQuery<tbluserstest>("select c.matchuserid,a.* from tbl_users a,tbl_teams b,tbl_match_users c where a.userid=c.userid and b.teamid=c.teamid and b.status=0 and c.mobile like '00000%'").ToList();
              foreach (var item in ents)
              {                  
                  string mm = string.Format("update tbl_match_users set mobile='2000000{0}' where matchuserid='{1}'",cnt.ToString().PadLeft(4,'0'), item.matchuserid);
                  string mu = string.Format("update tbl_users set mobile='2000000{0}',passwd='{2}' where userid='{1}'", cnt.ToString().PadLeft(4, '0'), item.userid, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(item.cardno.Substring(item.cardno.Length-6), "MD5"));

                  db.ExecuteSqlCommand(mm);
                  db.ExecuteSqlCommand(mu);

                  cnt++;
              }
            
            }
        }

        /// <summary>
        /// 根据dictid查询字典
        /// zzy 2019-01-26
        /// </summary>
        /// <param name="dictid"></param>
        /// <returns></returns>
        public List<dict> GetDict(int dictid)
        {
            using (var db = new BFdbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.code as value,a.name as text FROM tbl_dict a WHERE 1=1");

                sql.AppendFormat(" AND a.dictid = {0}", dictid);

                return db.SqlQuery<dict>(sql.ToString()).ToList();
            }
        }
    }
}
