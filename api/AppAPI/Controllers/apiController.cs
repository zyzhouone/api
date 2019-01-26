using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

using Utls;
using Model;
using BLL;

namespace AppAPI.Controllers
{
    /// <summary>
    /// API接口
    /// </summary>
    public class apiController : BaseController
    {
        [AllowAnonymous]
        public JsonResult NoAudit()
        {
            return RepReurnError("token传入错误");
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult createteam()
        {
            return RepReurnOK();
        }

        /// <summary>
        ///队伍详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /*public JsonResult getteam(string id)
        {
            int intId = 0;

            if (!int.TryParse(id, out intId))
                return RepReurnError("id传入错误");


            ApiBll bll = new ApiBll();
            var team = bll.GetTeamById(intId);
            if (team == null)
                return RepReurnError("查询的队伍信息不存在");

            var data = new { id = team.Id, teamname = team.Teamname, userid = team.Userid, company = team.Company, lineid = team.Lineid, createtime = team.Createtime };
            return RepReurnOK(data);
        }*/

        public JsonResult getmatchlist(string dt)
        {
            ApiBll bll = new ApiBll();
            var match = bll.GetMatcheByDt(dt);
            foreach (var m in match)
            {
                if (m.Status == "0")
                    m.Status = "即将开始";
                else if (m.Status == "1")
                    m.Status = "预报名中";
                else if (m.Status == "2")
                    m.Status = "预报名结束";
                else if (m.Status == "3")
                    m.Status = "正式报名";
                else if (m.Status == "4")
                    m.Status = "比赛开始";
                else if (m.Status == "5")
                    m.Status = "比赛结束";
                else if (m.Status == "9")
                    m.Status = "报名结束";
                else if (m.Status == "8")
                    m.Status = "进行中";
            }
            if (match == null)
                return RepReurnError("没有比赛信息");

            return RepReurnOK(match);
        }
        public JsonResult getmatchstart(string userid)
        {
            ApiBll bll = new ApiBll();
            var match = bll.GetMatcheStart(userid);
            foreach (var m in match)
            {
                if (m.Status == "0")
                    m.Status = "即将开始";
                else if (m.Status == "1")
                    m.Status = "预报名中";
                else if (m.Status == "2")
                    m.Status = "预报名结束";
                else if (m.Status == "3")
                    m.Status = "正式报名中";
                else if (m.Status == "4")
                    m.Status = "比赛开始";
                else if (m.Status == "5")
                    m.Status = "比赛结束";
                else if (m.Status == "9")
                    m.Status = "报名结束";
                else if (m.Status == "8")
                    m.Status = "进行中";
            }

            if (match == null)
                return RepReurnError("没有比赛信息");

            return RepReurnOK(match);
        }

        [AllowAnonymous]
        public JsonResult userlogin(string account, string passwd, string typ)
        {

            //typ=1 ios typ=2 android

            if (account == null)
                return RepReurnError("请输入用户名");

            if (passwd == null)
                return RepReurnError("请输入密码");

            string md5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(passwd, "MD5");
            ApiBll bll = new ApiBll();
            var login = bll.Login(account, md5Pwd, typ);

            string ip;
            GetWebClientIp(out ip);

            if (login == null)
            {
                bll.OptLog(1, "", ip, string.Format("用户[{0}]登录失败", account));
                return RepReurnError("错误的用户名密码");
            }

            bll.OptLog(1, login.userid, ip, string.Format("用户[{0}]成功登录", account));

            if (login.Name == null)
                login.Name = "";

            login.Name = login.Name.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            login.Name = get_uft8(login.Name);
            login.Name = AESEncryption.AESEncrypt(login.Name, "124", "1234567890123456");

            //Utls.Caching.Set(login.Mobile, login.DeviceToken);

            return RepReurnOK(login);
        }

        [AllowAnonymous]
        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }

        [AllowAnonymous]
        public JsonResult reg_step1(string account)
        {
            string ip = "";

            //ip判断
            if (!GetWebClientIp(out ip))
                return RepReurnError("错误访问");

            //日志记录
            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());
            log.Error(ip);

            
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^1(3|5|7|8)\d{9}$");
            if (!regex.IsMatch(account))
            {
                log.Error(string.Format("手机号：{0}-IP:{1}", account, ip));
                return RepReurnError("手机号错误");
            }

            if (string.IsNullOrEmpty(account) || account.Trim().Length < 1)
                return RepReurnError("请输入手机号");

            if (account.Trim().Length > 15)
                return RepReurnError("手机号输入错误");
            if (account.Trim() == "4654984351984156")
            return RepReurnError("兄弟请收手吧！这样不好.");

            MembershipBll bll = new MembershipBll();
            var reg = bll.GetSMS(account, ip);
            if (reg == 0)
                return RepReurnError("错误的输入");

            if (reg == -1)
                return RepReurnError("已存在此用户，请使用密码找回");
            else if (reg == -2)
                return RepReurnError("获取验证码次数太频繁，请于明日再进行");
            else if (reg == -3)
                return RepReurnError("今日获取验证码次数太频繁，已被限制");

            return RepReurnOK(reg);
        }

        /// <summary>
        /// 判断接口是否网站域调用
        /// </summary>
        /// <param name="CustomerIP"></param>
        /// <returns></returns>
        private bool GetWebClientIp(out string CustomerIP)
        {
            CustomerIP = "";

            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return false;


                //bool normal = false;

                //string allhttp = System.Web.HttpContext.Current.Request.ServerVariables.Get("ALL_HTTP");
                //if (!string.IsNullOrEmpty(allhttp) && allhttp.Contains("HTTP_REFERER"))
                //    normal = true;
                //else
                //{
                //    string allraw = System.Web.HttpContext.Current.Request.ServerVariables.Get("ALL_RAW");
                //    if (!string.IsNullOrEmpty(allraw) && allraw.Contains("Referer"))
                //        normal = true;
                //}
                bool normal = true;
                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                    return normal;

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!String.IsNullOrEmpty(CustomerIP))
                    return normal;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    CustomerIP = System.Web.HttpContext.Current.Request.UserHostAddress;

                return normal;
            }
            catch { }

            return false;
        }


        [AllowAnonymous]
        public JsonResult reg_check(string account, string vercode)
        {

            if (account == null)
                return RepReurnError("请输入用户名");

            MembershipBll bll = new MembershipBll();
            string id = "";
            var reg = bll.CheckSms(account, vercode, ref id);
            if (reg == 0)
                return RepReurnOK(id);

            if (reg == -1)
                return RepReurnError("验证码输入错误");

            return RepReurnError("已存在此用户，请使用密码找回");

        }

        [AllowAnonymous]
        public JsonResult set_pwd(string account, string pwd1, string pwd2, DateTime? birthday, string cardtype, string cardno, string mobile, string name, string sexy)
        {

            if (account == null)
                return RepReurnError("请输入用户名");

            string userid = account;

            if (pwd1 == null)
                return RepReurnError("请输入密码");

            if (pwd2 == null)
                return RepReurnError("请输入确认密码");

            if (pwd1 != pwd2)
                return RepReurnError("两次密码输入不一致");

            if (string.IsNullOrEmpty(userid))
                return RepReurnError("没有获取用户信息");

            if (!birthday.HasValue)
                return RepReurnError("请填写生日");

            if (string.IsNullOrEmpty(cardtype))
                return RepReurnError("请选择证件类别");

            if (string.IsNullOrEmpty(cardno))
                return RepReurnError("请填写证件");

            if (string.IsNullOrEmpty(mobile))
                return RepReurnError("请填写手机号");

            if (string.IsNullOrEmpty(name))
                return RepReurnError("请填写姓名");

            if (string.IsNullOrEmpty(sexy))
                return RepReurnError("请填写性别");


            if (cardtype == "1")
            {
                if (cardno.Length == 15)
                {
                    if (cardno.Substring(6, 6) != birthday.Value.ToString("yyMMdd"))
                        return RepReurnError("身份证号与生日不一致");
                }
                else if (cardno.Length == 18)
                {
                    if (cardno.Substring(6, 8) != birthday.Value.ToString("yyyyMMdd"))
                        return RepReurnError("身份证号与生日不一致");
                }
                else
                    return RepReurnError("请检查身份证号是否正确");
            }

            int yearold = 0;
            try
            {
                string dy = birthday.Value.ToString("yyyyMMdd");
                string nw = DateTime.Now.ToString("yyyyMMdd");
                string m = (int.Parse(nw) - int.Parse(dy) + 1).ToString();

                if (m.Length > 4)
                    yearold = int.Parse(m.Substring(0, m.Length - 4));
            }
            catch (Exception ex)
            {
                yearold = 0;
            }

            if (yearold < 16 || yearold > 60)
                return RepReurnError("你的年龄需要在16-60之间");

            MembershipBll bll = new MembershipBll();
            string md5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd1, "MD5");
            var reg = bll.SubmitPwd(account, md5Pwd);
            if (reg == 0)
                return RepReurnError("错误的输入");

            if (reg == -1)
                return RepReurnError("已存在此用户，请使用密码找回");


            MembershipBll bll1 = new MembershipBll();

            tblusers usr = new tblusers();

            usr.userid = userid;
            usr.birthday = birthday;
            usr.cardno = cardno;
            usr.cardtype = cardtype;
            usr.Mobile = mobile;
            usr.Name = name;
            usr.sexy = sexy;
            usr.Isupt = "1";

            var reg1 = bll1.UpdateUser(usr);
            if (reg1 == 0)
                return RepReurnError("输入错误,请重新输入");

            if (reg1 == -1)
                return RepReurnError("更新失败");

            //return RepReurnOK(reg);

            return RepReurnOK("用户注册成功");

        }

        [AllowAnonymous]
        public JsonResult forget_pwd(string account)
        {

            if (account == null)
                return RepReurnError("请输入用户名");

            MembershipBll bll = new MembershipBll();
            var reg = bll.GetGSMS(account);
            if (reg == 0)
                return RepReurnError("错误的输入");

            if (reg == -1)
                return RepReurnError("此用户不存在，请直接注册");

            return RepReurnOK("短信已发送，请填写验证码");

        }

        [AllowAnonymous]
        public JsonResult forget_check(string account, string vercode)
        {

            if (account == null)
                return RepReurnError("请输入用户名");

            MembershipBll bll = new MembershipBll();
            var reg = bll.CheckGSms(account, vercode);
            if (reg == 0) { 
                
                string md5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "MD5");
                var reg1 = bll.ResetPwd(account, md5Pwd);
                return RepReurnOK("请重新设置密码,或使用123456登陆");
            }

            if (reg == -1)
                return RepReurnError("验证码输入错误");

            return RepReurnError("输入错误");

        }

        [AllowAnonymous]
        public JsonResult reset_pwd(string account, string pwd)
        {

            if (account == null)
                return RepReurnError("请输入用户名");

            if (pwd == null)
                return RepReurnError("请输入密码");

            MembershipBll bll = new MembershipBll();
            string md5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            var reg = bll.ResetPwd(account, md5Pwd);
            if (reg == 0)
                return RepReurnError("错误的输入");

            if (reg == -1)
                return RepReurnError("重置失败");

            return RepReurnOK(reg);

        }

        public JsonResult update_pwd(string userid, string oldpwd, string newpwd)
        {

            if (userid == null)
                return RepReurnError("没有获取用户信息");

            if (oldpwd == null)
                return RepReurnError("请输入旧密码");

            if (newpwd == null)
                return RepReurnError("请输入新密码");

            MembershipBll bll = new MembershipBll();
            string omd5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(oldpwd, "MD5");
            string nmd5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(newpwd, "MD5");
            var reg = bll.UpdatePwd(userid, omd5Pwd, nmd5Pwd);
            if (reg == 0)
                return RepReurnError("输入错误,请重新输入");

            if (reg == -1)
                return RepReurnError("重置失败");


            return RepReurnOK(reg);

        }

        public JsonResult update_info(string userid, DateTime? birthday, string cardtype, string cardno, string mobile, string name, string sexy)
        {

            if (string.IsNullOrEmpty(userid))
                return RepReurnError("没有获取用户信息");

            if (!birthday.HasValue)
                return RepReurnError("请填写生日");

            if (string.IsNullOrEmpty(cardtype))
                return RepReurnError("请选择证件类别");

            if (string.IsNullOrEmpty(cardno))
                return RepReurnError("请填写证件");

            if (string.IsNullOrEmpty(mobile))
                return RepReurnError("请填写手机号");

            if (string.IsNullOrEmpty(name))
                return RepReurnError("请填写姓名");

            if (string.IsNullOrEmpty(sexy))
                return RepReurnError("请填写性别");


            if (cardtype == "1")
            {
                if (cardno.Length == 15)
                {
                    if (cardno.Substring(6, 6) != birthday.Value.ToString("yyMMdd"))
                        return RepReurnError("身份证号与生日不一致");
                }
                else if (cardno.Length == 18)
                {
                    if (cardno.Substring(6, 8) != birthday.Value.ToString("yyyyMMdd"))
                        return RepReurnError("身份证号与生日不一致");
                }
                else
                    return RepReurnError("请检查身份证号是否正确");
            }

            int yearold = 0;
            try
            {
                string dy = birthday.Value.ToString("yyyyMMdd");
                string nw = DateTime.Now.ToString("yyyyMMdd");
                string m = (int.Parse(nw) - int.Parse(dy) + 1).ToString();

                if (m.Length > 4)
                    yearold = int.Parse(m.Substring(0, m.Length - 4));
            }
            catch (Exception ex)
            {
                yearold = 0;
            }

            if (yearold < 16 || yearold > 60)
                return RepReurnError("你的年龄需要在16-60之间");

            MembershipBll bll = new MembershipBll();

            tblusers usr = new tblusers();

            usr.userid = userid;
            usr.birthday = birthday;
            usr.cardno = cardno;
            usr.cardtype = cardtype;
            usr.Mobile = mobile;
            usr.Name = name;
            usr.sexy = sexy;
            usr.Isupt = "1";

            var reg = bll.UpdateUser(usr);
            if (reg == 0)
                return RepReurnError("输入错误,请重新输入");

            if (reg == -1)
                return RepReurnError("更新失败");

            return RepReurnOK(reg);
        }

        public JsonResult getlines(string userid)
        {

            ApiBll bll = new ApiBll();
            var lines = bll.GetLines(userid);

            if (lines == null)
                return RepReurnError("没有线路信息");

            return RepReurnOK(lines);
        }

        public JsonResult getpoints(string linesid)
        {

            ApiBll bll = new ApiBll();
            var points = bll.GetPoints(linesid);

            if (points == null)
                return RepReurnError("没有点标信息");

            foreach (var m in points)
            {
                m.Pointname = AESEncryption.AESEncrypt(m.Pointname, "123", "1234567890123456");
                //m.Content = m.Content.Replace("/UploadFiles", "http://img.chengshidingxiang.com");
                m.Content = AESEncryption.AESEncrypt("-", "123", "1234567890123456");
                //m.Pointno = AESEncryption.AESEncrypt(m.Pointno, "123", "1234567890123456");
                m.Pointout = AESEncryption.AESEncrypt(m.Pointout, "123", "1234567890123456");
                if (!string.IsNullOrEmpty(m.Pointtask))
                    m.Pointtask = m.Pointtask.Replace("/UploadFiles", "http://img.chengshidingxiang.com/");
                //m.Pointtask = m.Pointtask.Replace("/UploadFiles", "http://139.196.107.169:9000/UploadFiles/");

                m.Pointtask = AESEncryption.AESEncrypt(m.Pointtask, "123", "1234567890123456");
                //m.Pointtype = AESEncryption.AESEncrypt(m.Pointtype, "123", "1234567890123456");


                if (!string.IsNullOrEmpty(m.Sketchmap))
                    //m.Sketchmap = "http://139.196.107.169:9000/UploadFiles/" + m.Sketchmap;
                    m.Sketchmap = "http://img.chengshidingxiang.com/" + m.Sketchmap;

                if (!string.IsNullOrEmpty(m.Sketchvoice))
                    //m.Sketchvoice = "http://139.196.107.169:9000/UploadFiles/" + m.Sketchvoice;
                    m.Sketchvoice = "http://img.chengshidingxiang.com/" + m.Sketchvoice;
            }

            return RepReurnOK(points);
        }

        public JsonResult getmessage(string userid)
        {

            ApiBll bll = new ApiBll();
            var msg = bll.GetInformation(userid);

            if (msg == null)
                return RepReurnError("您暂时没有消息信息");

            return RepReurnOK(msg);
        }

        public JsonResult getmessagedetail(string infoid)
        {
            ApiBll bll = new ApiBll();
            var msg = bll.GetInformationDetail(infoid);

            if (msg == null)
                return RepReurnError("您暂时没有消息信息");

            return RepReurnOK(msg);
        }

        public JsonResult getnewmsgcount(string userid)
        {
            ApiBll bll = new ApiBll();
            var msg = bll.GetInformationCount(userid);

            if (msg == null)
                return RepReurnError("您暂时没有消息信息");

            return RepReurnOK(msg);
        }

        public JsonResult setreadmsg(string userid)
        {
            ApiBll bll = new ApiBll();
            int cnt = bll.setreadmsg(userid);

            return RepReurnOK(cnt);
        }

        public JsonResult getpic(string userid)
        {

            ApiBll bll = new ApiBll();
            var msg = bll.GetPicList(DateTime.Now);

            if (msg == null)
                return RepReurnError("没有图片信息");

            return RepReurnOK(msg);
        }

        public JsonResult getpicdetail(string userid, string match_id)
        {

            ApiBll bll = new ApiBll();
            var msg = bll.GetPicDetail(match_id);

            if (msg == null)
                return RepReurnError("没有图片信息");

            return RepReurnOK(msg);
        }

        public JsonResult getusermatch(string userid, string issuc)
        {
            if (userid == null)
                return RepReurnError("没有获取用户信息");

            ApiBll bll = new ApiBll();
            var msg = bll.GetUserMatch(userid, issuc);

            if (msg == null)
                return RepReurnError("没有比赛信息");

            foreach (var m in msg)
            {
                m.teamname = AESEncryption.AESEncrypt(m.teamname, "124", "1234567890123456");
            }

            return RepReurnOK(msg);
        }

        public JsonResult gettask(string userid)
        {
            if (userid == null)
                return RepReurnError("错误信息");

            ApiBll bll = new ApiBll();
            var msg = bll.TaskCheck(userid);

            if (msg == null)
                return RepReurnError("错误信息");

            foreach (var item in msg)
            {
                item.linename = AESEncryption.AESEncrypt(item.linename, "124", "1234567890123456");
                item.match_name = AESEncryption.AESEncrypt(item.match_name, "124", "1234567890123456");
                item.nickname = AESEncryption.AESEncrypt(item.nickname, "124", "1234567890123456");
                item.teamname = AESEncryption.AESEncrypt(item.teamname, "124", "1234567890123456");

                if (!string.IsNullOrEmpty(item.tasklogo))
                    item.tasklogo = "http://img.chengshidingxiang.com/" + item.tasklogo;

                if (!string.IsNullOrEmpty(item.logopic))
                    item.logopic = "http://img.chengshidingxiang.com/" + item.logopic;
            }
            return RepReurnOK(msg);
        }

        public JsonResult settask(string matchuserid)
        {
            if (matchuserid == null)
                return RepReurnError("请返回正确编号");

            ApiBll bll = new ApiBll();
            var msg = bll.UpdateTask(matchuserid);

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);

            /*string sql = AESEncryption.Encrypt("你已经成功签到，请妥善保管好签到领取的物料，集合您队伍中的其他队员，穿戴好比赛服装与号码布，从体育场2号通道进入内场，等待比赛开始，预祝你们玩的开心，取得好成绩！");
            sql = AESEncryption.Decrypt(sql);
            return RepReurnOK(sql);*/

        }

        public JsonResult uploadtask(string matchuserid, string pointid, DateTime pointtime)
        {
            if (matchuserid == null)
                return RepReurnError("请返回正确编号");

            ApiBll bll = new ApiBll();
            var msg = bll.UploadRecord(matchuserid, pointid, pointtime);

            if (msg == "-1")
                return RepReurnError("没有查询到该队伍信息");

            if (msg == "-2")
                return RepReurnError("没有查询到点标信息");

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);
        }

        public JsonResult getteamrecord(string matchuserid)
        {
            if (matchuserid == null)
                return RepReurnError("请返回正确编号");

            ApiBll bll = new ApiBll();
            var msg = bll.GetTeamRecord(matchuserid);

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);
        }

        [AllowAnonymous]
        public JsonResult checkversion(string ver)
        {
            if (ver == null)
                return RepReurnError("请输入版本号");

            if (ver != "1.1.3")
                return RepReurnError("download.chengshidingxiang.com/zuobiao1.1.3.apk");

            return RepReurnOK(ver);
        }

        public JsonResult settestpoint(string pointid)
        {
            if (pointid == null)
                return RepReurnError("错误信息");

            ApiBll bll = new ApiBll();
            var msg = bll.UpdateTestRecord(pointid);

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);
        }

        public JsonResult uploadresult(string teamid, string match_id, string teamno, string userno, DateTime? starttime, DateTime? settime, string timeline)
        {
            if (string.IsNullOrEmpty(userno))
                return RepReurnError("登录用户名不能为空");

            if (userno.ToLower().StartsWith("admin"))
                return RepReurnError("admin用户不能上传成绩");

            if (teamno == null)
                return RepReurnError("队伍信息不能为空");

            if (!starttime.HasValue)
                return RepReurnError("开始时间不能为空");

            ApiBll bll = new ApiBll();
            var msg = bll.SetResult(teamid, match_id, teamno, userno, starttime, settime, timeline);

            if (msg == null)
                return RepReurnError("错误：数据上传失败");

            return RepReurnOK(msg);
        }

        public JsonResult downloadresult(string userno, string match_id)
        {
            if (string.IsNullOrEmpty(userno))
                return RepReurnError("用户信息不能为空");
            if (string.IsNullOrEmpty(match_id))
                return RepReurnError("赛事信息不能为空");

            ApiBll bll = new ApiBll();
            List<tblresult> result = bll.GetResult(userno, match_id);

            if (result == null || result.Count == 0)
                return RepReurnOK(result);

            List<tblresultcopy> msg = new List<tblresultcopy>();
            tblresultcopy trp;
            foreach (tblresult tr in result)
            {
                trp = new tblresultcopy();
                trp.teamid = tr.teamid;
                trp.match_id = tr.match_id;
                trp.userno = tr.userno;
                trp.teamno = tr.teamno;
                if (tr.starttime.HasValue)
                    trp.starttime = tr.starttime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                if (tr.settime.HasValue)
                    trp.settime = tr.settime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                trp.createtime = tr.createtime;
                trp.timeline = tr.timeline;
                trp.status = tr.status;
                msg.Add(trp);
            }

            return RepReurnOK(msg);
        }

        public JsonResult getscanmatch()
        {

            ApiBll bll = new ApiBll();
            var msg = bll.GetScanMatch();

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);
        }

        public JsonResult getscanteam(string match_id)
        {
            if (match_id == null)
                return RepReurnError("请选择比赛");

            ApiBll bll = new ApiBll();
            var msg = bll.GetScanTeams(match_id);

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);
        }

        /// <summary>
        /// 获取路线信息
        /// </summary>
        /// <param name="match_id"></param>
        /// <returns></returns>
        public JsonResult getline(string matchid)
        {
            if (matchid == null)
                return RepReurnError("请选择比赛");

            ApiBll bll = new ApiBll();
            //var msg = bll.Getline(matchid);
            var msg = bll.GetlinesByMatchid(matchid);

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);
        }

        public JsonResult getlinebytype(string typeid)
        {
            if (typeid == null)
                return RepReurnError("请选择线路类别");

            ApiBll bll = new ApiBll();
            //var msg = bll.Getline(matchid);
            var msg = bll.GetlinesByLineid(typeid);

            if (msg == null)
                return RepReurnError("错误信息");

            return RepReurnOK(msg);
        }



        //--------------报名----------------
        public JsonResult GetSMS(string mobile)
        {
            ApiBll bll = new ApiBll();
            int res = bll.Step1(mobile);

            if (res == 1)
                return RepReurnOK();
            else
                return RepReurnError("获取验证码时出现错误");
        }

        public JsonResult CheckSMS(string mobile, string vercode, string matchid)
        {
            ApiBll bll = new ApiBll();

            string res = bll.CheckSms(mobile, vercode, matchid);

            if (res == "-1")
                return RepReurnError("输入的验证码错误");
            else if (res == "-2")
                return RepReurnError("抱歉，这个号码已经被注册过");
            else
                return RepReurnOK(res);
        }


        public JsonResult Step2(string userid, string matchid)
        {
            ApiBll bll = new ApiBll();

            var mt = bll.GetMatchById(matchid);
            if (mt != null && mt.Status != "1")
                return RepReurnError("这个比赛不能报名");
            else if (mt == null)
                return RepReurnError("比赛不存在");

            var tm = bll.GetTeamByum(userid, matchid);
            if (tm != null)
            {
                //已经注册过队伍，则跳转到对应步骤
                return RepReurn("-1", "info", "注册未完成，请根据情况跳转到对应步骤", tm);
            }

            return RepReurnOK();
        }

        public JsonResult CheckTname(string matchid, string teamname)
        {
            if (string.IsNullOrEmpty(matchid) || string.IsNullOrEmpty(teamname))
                return RepReurnError("请确认参数是否正确输入");

            int len = System.Text.Encoding.Default.GetBytes(teamname).Length;
            if (len < 4)
                return RepReurnError("队伍名称长度太小");
            if (len > 12)
                return RepReurnError("队伍名称长度太大");

            if (System.Text.RegularExpressions.Regex.Matches(teamname, @"^\w*$").Count <= 0)
                return RepReurnError("队伍名称不能包含符号");


            ApiBll bll = new ApiBll();
            bool res = bll.CheckTname(matchid, teamname);

            if (res)
                return RepReurnError("已经存在相同的队伍名称");
            else
                return RepReurnOK();
        }

        public JsonResult RegTeamName(string userid, string matchid, string tname, string tcom)
        {
            if (string.IsNullOrEmpty(tname))
                return RepReurnError("请传入队伍名称");

            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(matchid))
                return RepReurnError("请传入用户或比赛ID");

            int len = System.Text.Encoding.Default.GetBytes(tname).Length;
            if (len < 4)
                return RepReurnError("队伍名称长度太小");
            if (len > 12)
                return RepReurnError("队伍名称长度太大");

            if (System.Text.RegularExpressions.Regex.Matches(tname, @"^\w*$").Count <= 0)
                return RepReurnError("队伍名称不能包含符号");

            string teamid = "";
            ApiBll bll = new ApiBll();
            string res = bll.RegTname(userid, matchid, tname, tcom, "", ref teamid);

            if (res == "-3")
                return RepReurnOK(teamid);
            else if (res == "-2")
                return RepReurnError("你的基本信息还不完整，请先完善你的信息");
            else if (res == "-1")
                return RepReurnError("已经存在相同的队伍名称");
            else
                return RepReurnOK(teamid);
        }

        public JsonResult checkTeam(string teamid)
        {
            ApiBll bll = new ApiBll();
            var team = bll.GetTeamById(teamid);
            if (team == null)
                return RepReurnOK("-1");
            else if (string.IsNullOrEmpty(team.Lineid))
                return RepReurnOK("0");
            else
            {
                if (team.Status == 6)
                    return RepReurnOK("0");
                else if (team.Status == 0)
                    return RepReurnOK("2");
                else if (team.Status == 1)
                    return RepReurnOK("3");
                else
                    return RepReurnOK("1");
            }
        }

        public JsonResult getmatchinfo(string matchid)
        {
            ApiBll bll = new ApiBll();
            var match = bll.GetMatchById(matchid);
            //foreach (var m in match)
            //{
            //    if (m.Status == "0")
            //        m.Status = "即将开始";
            //    else if (m.Status == "1")
            //        m.Status = "预报名中";
            //    else if (m.Status == "2")
            //        m.Status = "预报名结束";
            //    else if (m.Status == "3")
            //        m.Status = "正式报名";
            //    else if (m.Status == "4")
            //        m.Status = "比赛开始";
            //    else if (m.Status == "5")
            //        m.Status = "比赛结束";
            //    else if (m.Status == "9")
            //        m.Status = "报名结束";
            //    else if (m.Status == "8")
            //        m.Status = "进行中";
            //}

            return RepReurnOK(match);
        }

        public JsonResult getmatchnotice(string matchid)
        {
            ApiBll bll = new ApiBll();
            var match = bll.GetMatchById(matchid);

            if (match == null)
                return RepReurnError("无数据");
            else
                return RepReurnOK(match.Notice);
        }


        public JsonResult Step3(string teamid)
        {
            ApiBll bll = new ApiBll();
            var mt = bll.GetMatchByTeamid(teamid);
            if (mt != null)
            {
                if (mt.Status != "1")
                    return RepReurnError("这个比赛不能报名");
            }

            return RepReurnOK(bll.GetTLines(teamid));
        }

        public JsonResult SelLine(string teamid, string lineid)
        {
            ApiBll bll = new ApiBll();
            int res = bll.SelectLine(teamid, lineid);

            if (res == -2)
                return RepReurnError("路线传递错误");
            else if (res < 0)
                return RepReurnError("操作中出现错误");
            else
                return RepReurnOK();
        }

        public JsonResult ChangeLine(string userid, string teamid, string linesid)
        {
            ApiBll bll = new ApiBll();
            int res = bll.ChangeLine(userid, teamid, linesid);

            if (res == -2)
                return RepReurnError("路线传递错误");
            else if (res == -3)
                return RepReurnError("不是队长,不能操作");
            else if (res == -4)
                return RepReurnError("你已完成报名不能更换路线");
            else if (res == -1)
                return RepReurnError("队伍不存在");
            else if (res < 0)
                return RepReurnError("操作中出现错误");
            else
                return RepReurnOK();
        }

        public JsonResult changeteamname(string userid, string teamid, string teamname)
        {
            ApiBll bll = new ApiBll();
            int res = bll.changeteam(userid, teamid, teamname);

            if (res >= 0)
                return RepReurnOK();
            else if (res == -1)
                return RepReurnError("队伍不存在");
            else if (res == -2)
                return RepReurnError("报名已经完成，不能修改");
            else if (res == -3)
                return RepReurnError("队伍名称重复，不能修改");
            else if (res == -4)
                return RepReurnError("你不是队长，不能修改");
            else
                return RepReurnError("操作中出现错误");
        }

        public JsonResult getuserinfo(string userid)
        {
            ApiBll bll = new ApiBll();
            var usr = bll.GetUserById(userid);

            return RepReurnOK(usr);
        }

        public JsonResult Step4(string teamid)
        {
            ApiBll bll = new ApiBll();
            var usr = bll.GetUserByTeamId(teamid);

            var line = bll.GetLineById(teamid);

            ViewBag.cnt = line.Players - 1;

            return RepReurnOK(new { usr, line });
        }

        public JsonResult addplayer(string teamid, string mobile)
        {
            ApiBll bll = new ApiBll();
            int res = bll.AddMatchuser(teamid, mobile);
            if (res == -3)
                return RepReurnError("不能邀请自己");
            else if (res == -2)
                return RepReurnError("已经被邀请");
            else if (res == -1)
                return RepReurnError("邀请的队员没有注册");
            else if (res == -4)
                return RepReurnError("邀请的队员已经超出总人数");
            else
                return RepReurnOK();
        }

        public JsonResult delplayer(string teamid, string mobile, string userid)
        {
            if (string.IsNullOrEmpty(teamid) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(userid))
                return RepReurnError("请检查teamid、mobile、userid参数是否传递");

            ApiBll bll = new ApiBll();
            int res = bll.DelMatchuser(teamid, mobile, userid);
            if (res > 0)
                return RepReurnOK();
            else if (res == -1)
                return RepReurnError("只有队长才可以此操作");
            else
                return RepReurnError("删除中出现错误");
        }

        public JsonResult getplayer(string teamid)
        {
            ApiBll bll = new ApiBll();
            var muser = bll.GetMatchuserByTeamId(teamid);

            if (muser == null)
                return RepReurnError("没有获取到队员信息");

            foreach (var m in muser)
            {
                if (m.Nickname == null)
                    m.Nickname = "";
                if (m.Teamname == null)
                    m.Teamname = "";
                m.Teamname = AESEncryption.AESEncrypt(m.Teamname, "124", "1234567890123456");
                m.Nickname = AESEncryption.AESEncrypt(m.Nickname, "124", "1234567890123456");
            }

            return RepReurnOK(muser);
        }


        public JsonResult getmatchplayer(string teamid, string userid)
        {
            ApiBll bll = new ApiBll();
            return RepReurnOK(bll.GetMatchuserByTeamIdUsrId(teamid, userid));
        }

        public JsonResult CompleteSign(string teamid, string userid)
        {
            if (string.IsNullOrEmpty(teamid) || string.IsNullOrEmpty(userid))
                return RepReurnError("请检查teamid、userid参数是否传递");

            ApiBll bll = new ApiBll();
            string res = bll.CompleteSign(teamid, userid);
            switch (res)
            {
                case "-1":
                    return RepReurnError("请检查teamid是否正确");
                case "-2":
                    return RepReurnError("队伍成员必须保证至少一名美女呦");
                case "-3":
                    return RepReurnError("队员年龄要16周岁以上");
                case "-4":
                    return RepReurnError("队员年龄要60周岁以下");
                case "-5":
                    return RepReurnError("团队中有成员姓名为空");
                case "-6":
                    return RepReurnError("团队中有成员身份证为空");
                case "-7":
                    return RepReurnError("团队中有成员年龄为空");
                case "-8":
                    return RepReurnError("只有队长才可以此操作");
                case "-9":
                    //return RepReurnError("团队中必须是5名队员");
                    return RepReurnError("确认队员未符合规定人数");
                case "-10":
                    return RepReurnError("团队中必须是4名队员");
                case "-11":
                    return RepReurnError("请检查是否提交了附加信息");
                case "-12":
                    return RepReurnError("团队中必须是2名队员");
                case "-13":
                    return RepReurnError("请检查是否提交了宝宝信息");
                case "-14":
                    return RepReurnError("这个队伍已经完成了报名，不能再次操作");
                case "-15":
                    return RepReurnError("比赛不是预报名状态");
                case "-99":
                    return RepReurnError("请等待队员接受邀请");
                default:
                    break;
            }
            return RepReurnOK("已成功预报名");
        }

        public JsonResult cancelteam(string teamid, string userid)
        {
            if (string.IsNullOrEmpty(teamid) || string.IsNullOrEmpty(userid))
                return RepReurnError("请检查teamid、userid参数是否传递");
            ApiBll bll = new ApiBll();
            int res = bll.cancelteam(teamid, userid);

            switch (res)
            {
                case -1:
                    return RepReurnError("队伍信息不存在");
                case -2:
                    return RepReurnError("不是队长，不能取消");
                case -3:
                    return RepReurnError("目前状态不能取消");
                default:
                    return RepReurnOK("取消成功");
            }
        }
        //-----------------------------

        public JsonResult UpdateDeviceToken(string userid, string devtoken)
        {
            ApiBll bll = new ApiBll();
            int res = bll.UpdateDeviceToken(userid, devtoken);
            if (res < 0)
                return RepReurnError("userid参入错误");
            else
                return RepReurnOK();
        }

        public JsonResult Accept(string infoid, string userid)
        {
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(infoid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.AcceptMatch(infoid, userid);

            if (res >= 0)
                return RepReurnOK();
            else if (res == -1)
                return RepReurnError("您已经接受了其它队伍或删除了报名数据,不能再次操作,请忽略此条信息");
            else if (res == -99)
                return RepReurnError("此条消息已经处理过,请忽略此条信息");
            else if (res == -2)
                return RepReurnError("你已经参加了本次比赛的一个队伍，不能接受其他队伍");
            else
                return RepReurnError("请先完善基本信息再参加比赛");
        }

        public JsonResult Reject(string infoid, string userid)
        {
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(infoid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.RejectMatch(infoid, userid);

            if (res >= 0)
                return RepReurnOK();
            else if (res == -99)
                return RepReurnError("此条消息已经处理过,请忽略此条信息");
            else
                return RepReurnError("您已经接受了其它队伍或删除了报名数据,不能再次操作,请忽略此条信息");
        }

        public JsonResult releader(string userid, string matchuserid)
        {
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(matchuserid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.ReLeader(userid, matchuserid);

            if (res > 0)
                return RepReurnOK();
            else if (res == -1)
                return RepReurnError("你不是队长，没有权限更换");
            else
                return RepReurnError("操作中出现错误");
        }

        public JsonResult getextra(string teamid)
        {
            if (string.IsNullOrEmpty(teamid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            return RepReurnOK(bll.GetExtra(teamid));
        }

        [HttpPost]
        public JsonResult addextra(string type, string teamid, string info1, string info2, string info3)
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(teamid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();

            //添加图片
            if (type == "2")
            {
                if (Request.Files == null || Request.Files.Count < 1)
                    return RepReurnError("请上传图片");

                string path = Server.MapPath("~/uploadimg");
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                string filename = string.Format("{0}{1}", Guid.NewGuid(), System.IO.Path.GetExtension(Request.Files[0].FileName));

                Request.Files[0].SaveAs(System.IO.Path.Combine(path, filename));
                //Request.Files[0].SaveAs(System.IO.Path.Combine(Server.MapPath("~/uploadimg"), filename));

                info3 = "/uploadimg/" + filename;
            }

            int res = bll.AddExtra(type, teamid, info1, info2, info3);

            if (res > 0)
                return RepReurnOK();
            else if (res == -80)
                return RepReurnError("宝宝年龄需要在7-15周岁之间");
            else if (res == -81)
                return RepReurnError("请检查身份证号是否正确");
            else
                return RepReurnError("操作中出现错误");
        }

        [HttpPost]
        public JsonResult updateextra(string exid, string info1, string info2, string info3)
        {
            if (string.IsNullOrEmpty(exid))
                return RepReurnError("请检查参数是否输入");

            //添加图片
            if (Request.Files.Count > 0)
            {
                string path = Server.MapPath("~/uploadimg");
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                string filename = string.Format("{0}{1}", Guid.NewGuid(), System.IO.Path.GetExtension(Request.Files[0].FileName));

                Request.Files[0].SaveAs(System.IO.Path.Combine(path, filename));

                info3 = "/uploadimg/" + filename;
            }

            ApiBll bll = new ApiBll();
            int res = bll.UpdateExtra(exid, info1, info2, info3);

            if (res > 0)
                return RepReurnOK();
            else if (res == -80)
                return RepReurnError("宝宝年龄需要在7-15周岁之间");
            else if (res == -81)
                return RepReurnError("请检查身份证号是否正确");
            else
                return RepReurnError("操作中出现错误");
        }

        public JsonResult delextra(string exid)
        {
            if (string.IsNullOrEmpty(exid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.DelExtra(exid);

            if (res > 0)
                return RepReurnOK();
            else
                return RepReurnError("操作中出现错误");
        }

        public JsonResult getmymatch(string userid, string matchid)
        {
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(matchid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            var k = bll.GetMyMacth(userid, matchid);
            if (k == null)
                return RepReurnOK("");
            else
            {
                if (!string.IsNullOrEmpty(k.Lineid))
                {
                    var line = bll.GetLineById(k.Lineid);

                    k.LineName = line.Name;
                    k.LineType = line.Status;
                }

                return RepReurnOK(k);
            }
        }


        public JsonResult getmymatchstatus(string userid, string matchid)
        {
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(matchid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            var k = bll.GetMyMacthStatus(userid, matchid);

            if (!string.IsNullOrEmpty(k.Teamname))
                k.Teamname = AESEncryption.AESEncrypt(k.Teamname, "124", "1234567890123456");
            if (!string.IsNullOrEmpty(k.Linesname))
                k.Linesname = AESEncryption.AESEncrypt(k.Linesname, "124", "1234567890123456");

            return RepReurnOK(k);
        }

        public JsonResult getmyline(string teamid)
        {
            if (string.IsNullOrEmpty(teamid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            return RepReurnOK(bll.GetMyLine(teamid));
        }

        public JsonResult getLineType(string matchid)
        {
            if (string.IsNullOrEmpty(matchid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            return RepReurnOK(bll.GetLineByMatchid(matchid));
        }

        public JsonResult getmyorder(string teamid)
        {
            if (string.IsNullOrEmpty(teamid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            return RepReurnOK(bll.GetMyOrder(teamid));
        }

        public JsonResult updateteamname(string teamid, string teamname)
        {
            if (string.IsNullOrEmpty(teamid) || string.IsNullOrEmpty(teamname))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.UpdateTeamName(teamid, teamname);

            if (res > 0)
                return RepReurnOK();
            else if (res == -1)
                return RepReurnError("teamid不存在");
            else if (res == -2)
                return RepReurnError("已经完成报名，不能修改");
            else
                return RepReurnError("这个比赛中已经存在相同的名称");
        }

        public JsonResult changeplayer(string userid, string mobile, string matchuserid)
        {
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(matchuserid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.Replayer(userid, mobile, matchuserid);

            if (res > 0)
                return RepReurnOK();
            else if (res == -1)
                return RepReurnError("更换队员已经参加了比赛");
            else if (res == -2)
                return RepReurnError("你不是队长，没有权限更换");
            else if (res == -3)
                return RepReurnError("替换的队员不存在");
            else
                return RepReurnError("输入的手机号没有注册或者信息不完善");
        }

        public JsonResult acceptchange(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.AcceptReplay(infoid);

            if (res >= 0)
                return RepReurnOK();
            else if (res == -1)
                return RepReurnError("此消息已经过期");
            else if (res == -2)
                return RepReurnError("请先完善基本信息再参加比赛");
            else if (res == -3)
                return RepReurnError("你已经参加了本次比赛的一个队伍，不能接受其他队伍");
            else if (res == -9)
                return RepReurnError("参赛的队员年龄不能大于60岁或者小于16岁");
            else
                return RepReurnError("操作中出现错误");
        }

        public JsonResult rejectchange(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            int res = bll.RejectReplay(infoid);

            if (res >= 0)
                return RepReurnOK();
            else
                return RepReurnError("此消息已经过期");
        }

        public JsonResult completepay(string orderid, string trade_no, string trade_status, string buyer_email)
        {
            if (string.IsNullOrEmpty(orderid) || string.IsNullOrEmpty(trade_status))
                return RepReurnError("请检查参数是否输入");

            ////商户订单号
            //string out_trade_no = Request.QueryString["out_trade_no"];

            ////支付宝交易号
            //string trade_no = Request.QueryString["trade_no"];

            ////交易状态
            //string trade_status = Request.QueryString["trade_status"];

            //string buyer_email = Request.QueryString["buyer_email"];

            string out_trade_no = orderid;

            ApiBll bll = new ApiBll();

            bll.PayReturn(out_trade_no, trade_no, buyer_email, trade_status);

            return RepReurnOK();
        }

        public JsonResult completepay2(string result)
        {
            if (string.IsNullOrEmpty(result))
                return RepReurnError("请检查参数是否输入");

            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());
            log.Fatal(result);

            //result = "{\"alipay_trade_app_pay_response\":{\"code\":\"10000\",\"msg\":\"Success\",\"app_id\":\"2017010604887216\",\"auth_app_id\":\"2017010604887216\",\"charset\":\"utf-8\",\"timestamp\":\"2017-02-04 15:38:54\",\"total_amount\":\"0.01\",\"trade_no\":\"2017020421001004980218472260\",\"seller_id\":\"2088801855353392\",\"out_trade_no\":\"F63621742884058569968\"},\"sign\":\"Ide7koyuXZ6zqiv6He1Kfo43j1FOdJqBz3uMIst/yRxQrNenZ6SM3uqq5uCcBhBQuJ4U6Al8lajMzJEC3GTTYioio3n/zY5WpGwkHNuTNi78LsLYQnN6pXStrN6VOdWjaJQYFVwb0xtAJv2jBcuBjy/4XX5oLdgQcRcpRHAkXH9rSDfziV2JBUiu/2HXh8bv3r1IgI9vXnvGPdX9Zp/wFrz2BRjsT6HpJlxsCtlUV03XMlCpTyp8FY6N2Ej+J03e6SSOgl7DuRSgJTB/bl9xXhTQH9GP6u+HS7tlU710s7DAum8qEIx/8nNgPlkJm39C8parj01LKjXBBKkgBvdv+w==\",\"sign_type\":\"RSA2\"";
            ////商户订单号
            //string out_trade_no = Request.QueryString["out_trade_no"];

            ////支付宝交易号
            //string trade_no = Request.QueryString["trade_no"];

            ////交易状态
            //string trade_status = Request.QueryString["trade_status"];

            //string buyer_email = Request.QueryString["buyer_email"];

            string out_trade_no = "";
            string trade_no = "";
            string buyer_email = "";
            string trade_status = "";

            if (result.Contains("\"9000\""))
            {
                trade_status = "TRADE_SUCCESS";
            }

            int j1 = result.IndexOf("\"trade_no\"") + 12;
            int j2 = result.IndexOf("\"seller_id\"") - 2;

            trade_no = result.Substring(j1, j2 - j1);

            int j3 = result.IndexOf("\"seller_id\"") + 13;
            int j4 = result.IndexOf("\"out_trade_no\"") - 2;
            buyer_email = result.Substring(j3, j4 - j3);

            int j5 = result.IndexOf("\"out_trade_no\"") + 16;
            int j6 = result.IndexOf("\"sign\"") - 3;
            out_trade_no = result.Substring(j5, j6 - j5);

            ApiBll bll = new ApiBll();
            bll.PayReturn(out_trade_no, trade_no, buyer_email, trade_status);

            return RepReurnOK();
        }

        public JsonResult checkpay(string teamid)
        {
            if (string.IsNullOrEmpty(teamid))
                return RepReurnError("请检查参数是否输入");

            ApiBll bll = new ApiBll();
            var order = bll.GetOrderByTeamId(teamid);
            var cnt = bll.GetPaycountByTeamid(teamid);

            if (order == null)
                return RepReurnError("请选择正确队伍信息");

            string msg = bll.CheckPay(order.Id, cnt, order.Match_Id);
            payresult p = new payresult();

            if (msg == "99")
                return RepReurnError("您所选择的线路，尚未开始支付，请等待正式开始之后再支付");

            if (string.IsNullOrEmpty(msg))
                p.status = "0";
            else if (msg == "目前不能支付,谢谢你的参与.")
                p.status = "1";
            else if (msg == "正在支付，请等待")
                p.status = "2";
            else if (msg == "已经成功支付")
                p.status = "3";
            else if (msg == "抱歉!报名数量已经完成.")
                p.status = "4";
            else if (msg == "队列已满,请等待之后再重试.")
                p.status = "5";

            return RepReurnOK(p);
        }

        public JsonResult getad(string dt)
        {
            ApiBll bll = new ApiBll();
            var ad = bll.GetAD();
            if (ad == null)
                return RepReurnError("没有比赛信息");

            return RepReurnOK(ad);
        }

        public JsonResult CheckSignedOn(string matchid, string userid)
        {
            string cid = Utls.AESEncryption.AESDecrypt(matchid, "124", "1234567890123456");

            ApiBll bll = new ApiBll();
            if (bll.CheckSignedOn(cid, userid))
                return RepReurnError("已经签到");
            else
                return RepReurnOK();
        }


        public JsonResult SignIn(string matchid, string userid)
        {
            //string ucode= HttpContext.Server.UrlEncode("E99d4WkoQJY7FRpyEYScAvVWHx5cn8tLX/sJxNg2+jYmZe9xKVchNqt/ukkjQo6m");

            List<SelectListItem> dictMatch = new ApiBll().GetDict(20);
            string dictMatchid = "";
            if (dictMatch.Count == 1)
            {
                dictMatchid = dictMatch[0].Text.ToString();
            }

            if (string.IsNullOrEmpty(matchid) || matchid != dictMatchid)
                return RepReurnError("请检查参数是否正确");

            //string cid = Utls.AESEncryption.AESDecrypt(matchid, "124", "1234567890123456");

            ApiBll bll = new ApiBll();
            tblsignedon tb = new tblsignedon();
            tb.dtime = DateTime.Now;
            //tb.matchid = cid;
            tb.matchid = matchid;
           // tb.matchid = "24983c2c-7540-422f-8455-8c5a02592f7c";
            tb.signid = Guid.NewGuid().ToString();
            tb.Userid = userid;

            int res = bll.SignIn(tb);

            if (res == -1)
                return RepReurnError("请检查此用户是否参加比赛");
            else if (res == -2)
                return RepReurnError("比赛还没有准备开始，请耐心等待");
            else if (res == -3)
                return RepReurnError("请检查参数是否正确");
            else if (res == -9)
                return RepReurnError("已经签到");
            else
                return RepReurnOK();
        }

        public JsonResult matchdetail(string userid, string matchid)
        {
            TeamRegBll bll = new TeamRegBll();
            List<tblmatchdetailentity> lst = bll.GetMatchUsersByUidMid(userid, matchid);
            if (lst == null || lst.Count < 1)
                return RepReurnError("没有比赛信息");

            tblmatchdetailentity ent = new tblmatchdetailentity();
            foreach (tblmatchdetailentity item in lst)
            {
                if (item.leader.HasValue && item.leader.Value == 1)
                {
                    ent.Nickname += "," + item.Nickname + "[队长]";
                    //ent.LeaderName = item.Nickname;
                }
                else
                    ent.Nickname += "," + item.Nickname;
            }

            if (!string.IsNullOrEmpty(ent.Nickname))
                ent.Nickname = ent.Nickname.Substring(1);
            ent.Nickname = AESEncryption.AESEncrypt(ent.Nickname, "124", "1234567890123456");
            //ent.LeaderName = AESEncryption.AESEncrypt(ent.LeaderName, "124", "1234567890123456");

            ent.Lineno = lst[0].Lineno;
            ent.Teamno_t = lst[0].Teamno_t;
            //if (!string.IsNullOrEmpty(ent.Match_name))
            ent.Match_name = AESEncryption.AESEncrypt(lst[0].Match_name, "124", "1234567890123456");
            ent.MatchDate = lst[0].MatchDate;
            ent.Mobile = lst[0].Mobile;
            //if (!string.IsNullOrEmpty(ent.Teamname))
            ent.Teamname = AESEncryption.AESEncrypt(lst[0].Teamname, "124", "1234567890123456");
            ent.Matchstatus = lst[0].Matchstatus;

            if (lst[0].Teamstatus == "0")
            {
                if (lst[0].Matchstatus == "5")
                    ent.Teamstatus = "已完赛";
                else
                    ent.Teamstatus = "正式报名";
            }
            else if (lst[0].Teamstatus == "1")
                ent.Teamstatus = "预报名";
            else
                ent.Teamstatus = "已创建";

            //ent.Teamstatus = AESEncryption.AESEncrypt(ent.Teamstatus, "124", "1234567890123456");

            return RepReurnOK(ent);
        }

        public JsonResult matchcert(string userid, string matchid)
        {
            TeamRegBll bll = new TeamRegBll();
            tblmatchentity ent = bll.GetMatchUserByUidMid(userid, matchid);
            if (ent == null)
            {
                ent = new tblmatchentity();
                ent.Mono = "NONE";
            }
            return RepReurnOK(ent);
        }

        public JsonResult matchcert2(string userid, string matchid)
        {
            TeamRegBll bll = new TeamRegBll();
            List<tblmatchentity> ent = bll.GetMatchUserByUidMid2(userid, matchid);

            tblmatchentity tp = new tblmatchentity();
            if (ent.Count > 0)
            {
                tp = ent[0];
                foreach (tblmatchentity item in ent)
                {
                    if (item.Leader.HasValue && item.Leader.Value == 1)
                        //tp.Nickname += "," + item.Nickname + "[队长]";
                        tp.LeaderName = item.Nickname;
                    else
                        tp.Nickname += "," + item.Nickname;
                }

                tp.Nickname = tp.Nickname.Substring(1);
            }
            else
                tp = null;

            return RepReurnOK(tp);
        }
    }
}
