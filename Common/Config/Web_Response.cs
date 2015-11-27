using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Common.Config
{
    public static class Web_Response
    {
        public static HttpResponseMessage ResponseResult(object rm)
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