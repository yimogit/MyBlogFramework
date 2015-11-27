using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAuthorization
{
    /// <summary>
    /// api响应模型
    /// </summary>
    public class ResponseModel
    {
        public ResponseModel()
        {
            StatusCode = System.Net.HttpStatusCode.OK;
            ErrorMsg = string.Empty;
        }
        /// <summary>
        /// 响应状态
        /// </summary>
        public System.Net.HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 响应结果
        /// </summary>
        public object Data { get; set; }

    }
}