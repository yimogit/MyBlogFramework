using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Config
{
    public static class EmailApplication
    {
        /// <summary>
        /// 使用邮箱发送邮件
        /// </summary>
        /// <param name="toMailAddress">邮箱地址，多个请以,分隔</param>
        /// <param name="subjectInfo">标题</param>
        /// <param name="bodyInfo">正文(HTML)</param>
        /// <param name="attachPath">附件地址（物理路径）</param>
        /// <returns>成功：null 失败:错误消息</returns>
        public static string SendEmail(string toMailAddress, string subjectInfo, string bodyInfo, string attachPath = "")
        {

            string fromMailAddress = Common.ConfigHelper.GetConfigValueByKey("EmailAddress");
            string mailUsername = Common.ConfigHelper.GetConfigValueByKey("EmailName");
            string mailPassword = Common.ConfigHelper.GetConfigValueByKey("EmailPwd");
            string senderServerIp = Common.ConfigHelper.GetConfigValueByKey("EmailSmtp");
            string mailPort = Common.ConfigHelper.GetConfigValueByKey("EmailPort");
            Common.EmailHelper.MyEmail email = new Common.EmailHelper.MyEmail(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, true);
            if (!string.IsNullOrEmpty(attachPath))
            {
                email.AddAttachments(attachPath);
            }
            string msg = email.Send();
            if (!string.IsNullOrEmpty(msg))
            {
                string path = HttpContext.Current.Server.MapPath("/Logs/Email/" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt");
                Common.FileHelper.WriteLog(path, true, msg);
            }
            return msg;
        }
    }
}