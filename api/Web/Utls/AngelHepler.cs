using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Web
{
    public static class AngelHepler
    {
        /// <summary>
        /// 用户状态
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString GetStatus(this HtmlHelper helper, int id)
        {
            string status = "";
            switch (id)
            {
                case 0:
                    status = "正常";
                    break;
                case 1:
                    status = "暂停";
                    break;
                case 2:
                    status = "删除";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(status);
        }

        public static MvcHtmlString GetTrainingStatus(this HtmlHelper helper, int id)
        {
            string status = "";
            switch (id)
            {
                case 0:
                    status = "未提交";
                    break;
                case 1:
                    status = "已创建";
                    break;
                case 2:
                    status = "已结束";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(status);
        }

        public static MvcHtmlString GetIsTest(this HtmlHelper helper, int id)
        {
            string status = "";
            switch (id)
            {
                case 0:
                    status = "任务未反馈";
                    break;
                case 1:
                    status = "已反馈/未建考试";
                    break;
                case 2:
                    status = "已创建考试";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(status);
        }

        public static MvcHtmlString GetTestStatus(this HtmlHelper helper, int id)
        {
            string status = "";
            switch (id)
            {
                case 0:
                    status = "未开始";
                    break;
                case 1:
                    status = "进行中";
                    break;
                case 2:
                    status = "已结束";
                    break;
                case 3:
                    status = "已延长";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(status);
        }

        public static MvcHtmlString GetTaskStatus(this HtmlHelper helper, int id)
        {
            string status = "";
            switch (id)
            {
                case 0:
                    status = "未下发";
                    break;
                case 1:
                    status = "进行中";
                    break;
                case 2:
                    status = "已取消";
                    break;
                case 3:
                    status = "已完成";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(status);
        }

        public static MvcHtmlString GetTestDetailStatus(this HtmlHelper helper, int id)
        {
            string status = "";
            switch (id)
            {
                case 0:
                    status = "不及格";
                    break;
                case 1:
                    status = "及格";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(status);
        }

        public static MvcHtmlString GetTestResult(this HtmlHelper helper, int id)
        {
            string status = "";
            switch (id)
            {
                case 0:
                    status = "按时完成";
                    break;
                case 1:
                    status = "延时完成";
                    break;
                case 2:
                    status = "未考试";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(status);
        }
    }
}