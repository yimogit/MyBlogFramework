using MvcAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiYi.MVC.Controllers
{
    public class AdminController : WebControllerBase
    {
        [RequireAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
    }
}