using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace MvcAuthorization
{
    /// <summary>
    /// Controller的基类，用于实现适合业务场景的基础功能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [NotImplExceptionFilterAttribute]
    [BasicAuthentication]
    public abstract class ApiControllerBase : ApiController
    {
        public HttpResponseMessage ResponseResult(ResponseModel rm)
        {
            if (rm == null)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
            else
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(rm),
                        System.Text.Encoding.UTF8, "application/json")
                };
            }
        }

    }
}

