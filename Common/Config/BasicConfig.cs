using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Config
{
    /// <summary>
    /// 默认设置
    /// </summary>
    public static class BasicConfig
    {
        /// <summary>
        /// 后台默认显示条数
        /// </summary>
        public static int AdminPageSize = 10;
        /// <summary>
        /// 前台默认显示条数
        /// </summary>
        public static int WebPageSize = 10;
        /// <summary>
        /// 后台默认首页URL
        /// </summary>
        public static string AdminIndexUrl = "/Admin/Index";
        /// <summary>
        /// 默认后台登录页面URL
        /// </summary>
        public static string AdminLoginUrl = "/Admin/Login";

        /// <summary>
        /// 默认无权限页面URL
        /// </summary>
        public static string DefaultUnAuthorizedUrl = "/Admin/Error";
        /// <summary>
        /// 默认错误页面URL
        /// </summary>
        public static string DefaultErrorUrl = "/Admin/Error";
    }
}