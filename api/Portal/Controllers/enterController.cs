using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using log4net;

using Model;
using BLL;
using Utls;

namespace Portal.Controllers
{
    public class enterController : BaseController
    {
        //
        // GET: /register/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public JsonResult GetSMS(string mobile)
        {
            TeamRegBll bll = new TeamRegBll();
            int res = bll.Step1(mobile);

            if (res == 1)
                return RepReurnOK();
            else
                return RepReurnError("获取验证码时出现错误");
        }

        public JsonResult CheckSMS(string mobile,string vercode)
        {
            TeamRegBll bll = new TeamRegBll();

            int res = bll.CheckSms(mobile, vercode, "6a61b95b-2d5d-4373-abaf-bf4e4c438900");

            if (res > 0)
                return RepReurn(0, res.ToString(), null);
            else if(res==-2)
                return RepReurnError("抱歉，这个号码已经被注册过");
            else
                return RepReurnError("输入的验证码错误");
        }

        public ActionResult Step2(string id,string tid)
        {
            ViewBag.id = id;
            ViewBag.tid = tid;
            return View();
        }

        public JsonResult CheckTname(string matchid, string tname)
        {
            TeamRegBll bll = new TeamRegBll();
            bool res = bll.CheckTname(matchid, tname);

            if (res)
                return RepReurnError("已经存在相同的队伍名称");
            else
                return RepReurnOK();
        }

        public JsonResult RegTname(string id, string tid, string tname, string tcom, string pwd)
        {
            string md5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");

            TeamRegBll bll = new TeamRegBll();
            int res = bll.RegTname(id, tid, tname, tcom, md5Pwd);

            if (res == -1)
                return RepReurnError("已经存在相同的队伍名称");
            else
                return RepReurn(0, res.ToString(), null);
        }

        public ActionResult Step3(string tid)
        {
            ViewBag.tid = tid;
            TeamRegBll bll = new TeamRegBll();
            return View(bll.GetLines());
        }

        public JsonResult SelLine(string tid,string lid)
        {
            TeamRegBll bll = new TeamRegBll();
            int res = bll.SelectLine(tid, lid);

            if (res<0)
                return RepReurnError("操作中出现错误");
            else
                return RepReurnOK();
        }

        public ActionResult Step4(string tid)
        {
            ViewBag.tid = tid;
            TeamRegBll bll = new TeamRegBll();
            return View(bll.GetLineById(int.Parse(tid)));
        }

        [HttpPost]
        public JsonResult inputmb(List<tblmatchusers> mus, string tid)
        {            
            TeamRegBll bll = new TeamRegBll();
            int res = bll.InputMb(mus, int.Parse(tid));

            if (res < 0)
                return RepReurnError("操作中出现错误");
            else
                return RepReurnOK();
        }

        
        public JsonResult ImpTeams(string matchid) 
        {
            try
            {
                DataTable data = NpoiHelper.XlSToDataTable(@"C:\Work\Vki\MyDoc\MyFile\dx_dev\定向赛团体报名统计表.xlsx", "test", 0);

                if (data == null || data.Rows.Count < 1)
                    return RepReurnError("上传的文件中没有数据");

                StringBuilder sbtError=new StringBuilder();

                List<tblusers> lstUsers = new List<tblusers>();
                List<tblteams> lstTeams = new List<tblteams>();
                List<tblmatchusers> lstMatchusers = new List<tblmatchusers>();

                int sn = 0;
                string lineid = "";
                string teamno = "";
                string teamname = "";
                string company = "";

                foreach (DataRow row in data.Rows)
                {
                    //记录序号，以标记团队
                    if (!string.IsNullOrEmpty(row["序号"].ToString().Trim()))
                        sn = int.Parse(row["序号"].ToString().Trim());

                    if (!string.IsNullOrEmpty(row["线路号"].ToString().Trim()))
                        lineid = row["线路号"].ToString().Trim();

                    if (!string.IsNullOrEmpty(row["队列号"].ToString().Trim()))
                        teamno = row["队列号"].ToString().Trim();

                    if (!string.IsNullOrEmpty(row["队名(6个字符以内)"].ToString().Trim()))
                        teamname = row["队名(6个字符以内)"].ToString().Trim();

                    if (!string.IsNullOrEmpty(row["公司名称"].ToString().Trim()))
                        company = row["公司名称"].ToString().Trim();

                    if (string.IsNullOrEmpty(row["队员编号"].ToString().Trim()))
                        continue;

                    if(string.IsNullOrEmpty(row["性别"].ToString().Trim()))
                         sbtError.Append("[性别]不能为空;");

                    if(string.IsNullOrEmpty(row["身份证/护照"].ToString().Trim()))
                         sbtError.Append("[身份证/护照]不能为空;");

                    if(!System.Text.RegularExpressions.Regex.IsMatch(row["手机号"].ToString().Trim(), @"^[1]+[0-9]+\d{9}"))
                        sbtError.AppendFormat("[手机号:{0}]格式错误;",row["手机号"]);

                    tblusers usr = new tblusers();
                    usr.cardtype = "1";
                    usr.Mobile = row["手机号"].ToString().Trim();
                    usr.sexy = row["性别"].ToString().Trim() == "男" ? "1" : "0";
                    usr.Status = 1;
                    usr.userid=Guid.NewGuid().ToString();                   
                    lstUsers.Add(usr);
                    
                    tblteams tm = new tblteams();
                    tm.teamid = Guid.NewGuid().ToString();
                    tm.Company = "";
                    tm.Createtime = DateTime.Now;
                    tm.Eventid = 1;
                    tm.Lineid = lineid;
                    tm.match_id = matchid;
                    tm.Status = 1;
                    tm.Teamname = teamname;
                    tm.Teamno = teamno;
                    //tm.Userid
                    lstTeams.Add(tm);
                }

            }
            catch (Exception ex)
            {
                ILog log = LogManager.GetLogger(this.GetType());
                log.Error(ex);
                return RepReurnError("导入过程中出现错误");
            }

            return RepReurnOK();
        }
    }
}
