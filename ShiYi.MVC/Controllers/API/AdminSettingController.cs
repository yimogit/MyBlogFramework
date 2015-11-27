using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MvcAuthorization;

namespace ShiYi.MVC.Controllers.API
{
    public class AdminSettingController : ApiControllerBase
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            Service.LogService log = new Service.LogService();
            string msg=log.WriteLog();
            return new HttpResponseMessage() { Content = new StringContent(msg) };
        }
    }
}
