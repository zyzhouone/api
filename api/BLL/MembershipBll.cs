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
    public class MembershipBll : BaseBll
    {
        /// <summary>
        /// 根据密码获取用户信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public tblusers GetUser(string mobile, string pwd)
        {
            using (var db = new BFdbContext())
            {
                return db.tblusers.FirstOrDefault(p => p.Passwd == pwd && p.Status == 0 && p.Mobile == mobile);
            }
        }

        /// <summary>
        /// 用户注册时，生成验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int GetSMS(string mobile, string ip)
        {

            using (var db = new BFdbContext())
            {
                if (db.tblusers.Any(p => p.Mobile == mobile && p.Status == 0))
                    return -1;

                DateTime dt = DateTime.Now.AddDays(-2);
                int cntt = db.tbluserstime.Count(p => p.RomateIp == ip && p.crtdate >= dt);
                if (cntt >= 10)
                    return -3;

                int cnt = db.tbluserstime.Count(p => p.Mobile == mobile && p.crtdate >= dt);
                if (cnt >= 10)
                    return -2;

                tblusers usr = new tblusers();
                usr.Mobile = mobile;
                usr.Passwd = "-";
                usr.mono = VerifyCode.Get6SzCode();
                //测试注册验证码改成固定数值
                //usr.mono = "123456";
                usr.Status = 4;
                usr.Playerid = 0;
                usr.userid = Guid.NewGuid().ToString();
                usr.Isupt = "0";
                usr.Type = "8";
                usr.Ismod = "";

                int res = db.Insert<tblusers>(usr);

                tbluserstime tm = new tbluserstime();
                tm.crtdate = DateTime.Now;
                tm.Mobile = mobile;
                tm.tid = Guid.NewGuid().ToString();
                tm.RomateIp = ip;
                db.Insert<tbluserstime>(tm);

                if (res > 0)
                    SMSHepler.SendRegSms(mobile, usr.mono);

                return res;
            }
        }

        /// <summary>
        /// 忘记密码时，生成验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int GetGSMS(string mobile)
        {
            using (var db = new BFdbContext())
            {
                tblusers usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.Status == 0);
                if (usr == null)
                    return -1;

                usr.mono = VerifyCode.Get6SzCode();

                int res = db.Update<tblusers>(usr);

                if (res > 0)
                    SMSHepler.SendGetSms(mobile, usr.mono);

                return res;
            }
        }

        /// <summary>
        /// 注册用户时，检验验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="vercode"></param>
        /// <param name="usrid"></param>
        /// <returns></returns>
        public int CheckSms(string mobile, string vercode, ref string usrid)
        {
            using (var db = new BFdbContext())
            {
                if (db.tblusers.Any(p => p.Mobile == mobile && p.Status == 0))
                    return -2;

                var usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.mono == vercode && p.Status == 4);
                if (usr == null)
                    return -1;
                else
                {
                    usrid = usr.userid;
                    return 0;
                }
            }
        }

        /// <summary>
        /// 忘记密码时，检验验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="vercode"></param>
        /// <param name="usrid"></param>
        /// <returns></returns>
        public int CheckGSms(string mobile, string vercode)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.mono == vercode && p.Status == 0);
                if (usr == null)
                    return -1;
                else                
                    return 0;
            }
        }
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="usrid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int SubmitPwd(string usrid, string pwd)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.userid==usrid && p.Status == 4);
                if (usr == null)
                    return -1;
                else
                {
                    usr.Status = 0;
                    usr.Passwd = pwd;

                    return db.Update<tblusers>(usr);
                }
            }
        }

        private int SetYearOld(DateTime birthday)
        {
            try
            {
                string dy = birthday.ToString("yyyyMMdd");
                string nw = DateTime.Now.ToString("yyyyMMdd");
                string m = (int.Parse(nw) - int.Parse(dy) + 1).ToString();

                if (m.Length > 4)
                    return int.Parse(m.Substring(0, m.Length - 4));
                else
                    return 0;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateUser(tblusers usr)
        {
            using (var db = new BFdbContext())
            {
                var tusr = db.tblusers.FirstOrDefault(p => p.userid == usr.userid);
                if (tusr == null)
                    return -1;
                else
                {
                    tusr.birthday = usr.birthday;
                    tusr.cardno = usr.cardno;
                    tusr.cardtype = usr.cardtype;
                    tusr.Mobile = usr.Mobile;
                    tusr.Name = usr.Name;
                    tusr.sexy = usr.sexy;
                    tusr.Isupt = "1";
                    tusr.Ismod = "1";
                    tusr.Modtime = DateTime.Now;

                    // or match_id='6a61b95b-2d5d-4373-abaf-bf4e4c438800')
                    string sql = string.Format(@"update tbl_match_users set birthday='{0}',age={1},cardno='{2}',cardtype='{3}',sexy={4},mobile='{5}',nickname='{6}' 
                    where userid='{7}' and (match_id in (select match_id from tbl_match where status in (0,1)) or match_id='c83aa363-873e-489e-ac07-373489c94320')",
                        tusr.birthday.Value.ToString("yyyy-MM-dd"), SetYearOld(tusr.birthday.Value), tusr.cardno, tusr.cardtype, tusr.sexy, tusr.Mobile, tusr.Name, tusr.userid);

                    db.ExecuteSqlCommand(sql);

                    return db.Update<tblusers>(tusr);                   
                }
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="usrid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int ResetPwd(string mobile, string pwd)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.Mobile == mobile && p.Status == 0);
                if (usr == null)
                    return -1;
                else
                {
                    usr.Passwd = pwd;

                    return db.Update<tblusers>(usr);
                }
            }
        }

        /// <summary>
        /// 获取我的比赛
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<tblmatch> GetMymatch(string userid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format(@"select a.* from tbl_match a,tbl_match_users b,tbl_users c where a.match_id=b.match_id and b.mobile=c.mobile and c.userid='{0}'", userid);
                return db.SqlQuery<tblmatch>(sql).ToList();
            }
        }

        /// <summary>
        /// 我的消息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<tblinfomation> GetMyinfo(string userid)
        {
            using (var db = new BFdbContext())
            {
                string sql = string.Format(@"select a.* from tbl_infomation a where a.type='2' and a.userid='{0}'", userid);
                return db.SqlQuery<tblinfomation>(sql).ToList();
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="oldpwd"></param>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        public int UpdatePwd(string userid,string oldpwd, string newpwd)
        {
            using (var db = new BFdbContext())
            {
                var usr = db.tblusers.FirstOrDefault(p => p.userid == userid&&p.Passwd==oldpwd && p.Status == 0);
                if (usr == null)
                    return -1;
                else
                {
                    usr.Passwd = newpwd;

                    return db.Update<tblusers>(usr);
                }
            }
        }

        public tblusers GetUserById(string userid)
        {
            using (var db = new BFdbContext())
            {
                return db.tblusers.FirstOrDefault(p => p.userid == userid);
            }
        }
    }
}
