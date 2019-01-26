using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Utls
{
    /// <summary>
    /// 短信接口发送
    /// </summary>
    public class SMSHepler
    {
        /*
         * https://luosimao.com/
         * sh_moa@163.com
         * dengshan64697960
         */
        private static string username = "api";//"sh_moa@163.com";
        private static string password = "key-6121bb0475c35f8cc902a69c66a94393";
        private static string url = "http://sms-api.luosimao.com/v1/send.json";

        /// <summary>
        /// 发送用户注册码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SMSResponse SendRegSms(string mobile, string code)
        {
            string message = string.Format("你的注册验证码:{0},请尽快验证。【中国坐标】", code);
            return SendSms(mobile, message);
        }

        /// <summary>
        /// 找回密码验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SMSResponse SendGetSms(string mobile, string code)
        {
            string message = string.Format("你的找回密码验证码:{0},请尽快验证。【中国坐标】", code);
            return SendSms(mobile, message);
        }

        /// <summary>
        /// 通用消息接口
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static SMSResponse SendCommonSms(string mobile, string msg)
        {
            string message = string.Format("{0}【中国坐标】", msg);
            return SendSms(mobile, message);
        }

        private static SMSResponse SendSms(string mobile, string message)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("mobile=" + mobile + "&message=" + message);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(username + ":" + password));
            webRequest.Headers.Add("Authorization", auth);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;

            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string Message = php.ReadToEnd();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SMSResponse>(Message);
        }
    }

    public class SMSResponse
    {
        private string _error;
        /// <summary>
        ///  错误码	错误描述	                    解决方案
        ///    -10	验证信息失败	                检查api key是否和各种中心内的一致，调用传入是否正确
        ///    -11	用户接口被禁用	                滥发违规内容，验证码被刷等，请联系客服解除
        ///    -20	短信余额不足	                进入个人中心购买充值
        ///    -30	短信内容为空	                检查调用传入参数：message
        ///    -31	短信内容存在敏感词	            接口会同时返回  hit 属性提供敏感词说明，请修改短信内容，更换词语
        ///    -32	短信内容缺少签名信息	        短信内容末尾增加签名信息eg.【公司名称】
        ///    -33	短信过长，超过300字（含签名）	调整短信内容或拆分为多条进行发送
        ///    -34	签名不可用	                    在后台 短信->签名管理下进行添加签名
        ///    -40	错误的手机号	                检查手机号是否正确
        ///    -41	号码在黑名单中	                号码因频繁发送或其他原因暂停发送，请联系客服确认
        ///    -42	验证码类短信发送频率过快	    前台增加60秒获取限制
        ///    -50	请求发送IP不在白名单内	        查看触发短信IP白名单的设置
        /// </summary>
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }

        private string _msg;
        /// <summary>
        /// 
        /// </summary>
        public string msg
        {
            get { return _msg; }
            set { _msg = value; }
        }


    }
}
