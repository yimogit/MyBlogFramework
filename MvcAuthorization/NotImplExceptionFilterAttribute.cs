using Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace MvcAuthorization
{

    /// <summary>
    /// 异常日志
    /// </summary>
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            try
            {
                //日志路径
                string path = HttpContext.Current.Server.MapPath("/Logs/WebApi/" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt");
                Exception ex = context.Exception;
                StringBuilder errMsg = new StringBuilder();
                errMsg.Append("请求地址：" + context.Request.RequestUri + "\r\n");
                errMsg.Append("请求时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                errMsg.Append("请求IP：" + Common.IPHelper.GetIP() + "\r\n");
                errMsg.Append("错误消息：" + ex.Message + "\r\n");
                errMsg.Append("错误方法：" + ex.TargetSite.ToString() + "\r\n");
                errMsg.Append("错误对象：" + ex.Source + "\r\n");
                errMsg.Append("栈堆信息：" + ex.StackTrace + "\r\n");
                Common.FileHelper.WriteLog(path, true, errMsg.ToString());
            }
            finally
            {
                context.Response =Web_Response.ResponseResult(
                    new ResponseModel() { StatusCode = HttpStatusCode.InternalServerError, ErrorMsg = "服务器响应失败,错误原因："+context.Exception.Message });
            }
        }
    }

}
