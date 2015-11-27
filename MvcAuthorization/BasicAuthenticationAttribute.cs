using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Security;
using Common.Config;

namespace MvcAuthorization
{
    /// <summary>
    /// 基本验证Attribtue，用以Action的权限处理
    /// </summary>
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 检查用户是否有该Action执行的操作权限
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)//验证WebApi的参数与特性是否有效
            {
                if (actionContext.ModelState.FirstOrDefault(item => item.Value.Errors.Count > 0).Value.Errors.Count > 0)
                    actionContext.Response = Web_Response.ResponseResult(
                        new ResponseModel() { StatusCode = HttpStatusCode.OK, ErrorMsg = actionContext.ModelState.FirstOrDefault(item => item.Value.Errors.Count > 0).Value.Errors.FirstOrDefault().ErrorMessage });
            }
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)//获取Authorization值
            {
                System.Net.Http.Headers.AuthenticationHeaderValue authValue = new System.Net.Http.Headers.AuthenticationHeaderValue(HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                actionContext.Request.Headers.Authorization = authValue;
            }
            //http://www.faceye.net/search/102356.html
            //检验用户ticket信息，用户ticket信息来自调用发起方
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                if (ValidateUserTicket(encryptTicket))
                    base.OnActionExecuting(actionContext);
                else
                    actionContext.Response = Web_Response.ResponseResult(
                        new ResponseModel() { StatusCode = HttpStatusCode.Unauthorized, ErrorMsg = "登录失效" });
            }
            else
            {
                //如果请求Header不包含ticket，则判断是否是匿名调用
                var attr = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);

                //是匿名用户，则继续执行；非匿名用户，抛出“未授权访问”信息
                if (isAnonymous)
                    base.OnActionExecuting(actionContext);
                else
                    actionContext.Response = Web_Response.ResponseResult(
                        new ResponseModel() { StatusCode = HttpStatusCode.Unauthorized, ErrorMsg = "未授权访问" });

            }
        }

        /// <summary>
        /// 校验用户ticket信息
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        private bool ValidateUserTicket(string encryptTicket)
        {
            var userTicket = FormsAuthentication.Decrypt(encryptTicket);
            var userTicketData = userTicket.UserData;

            string userName = userTicketData.Substring(0, userTicketData.IndexOf(":"));
            string password = userTicketData.Substring(userTicketData.IndexOf(":") + 1);
            //获取用户权限
            //AuthorizedUser.Current.UserAuthList
            //检查用户名、密码是否正确，验证是合法用户
            return new AccountModel().ValidateUserLogin(userName, password);
        }
    }
}
