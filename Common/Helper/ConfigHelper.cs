using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ConfigHelper
    {
        /// <summary>
        /// 根据Key获取配置文件中对应的value值
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="isLower">是否转为小写</param>
        /// <param name="ps">参数长度可变数组</param>
        /// <returns>key对应的value值(默认转为小写)</returns>
        public static string GetConfigValueByKey(string key,bool isLower=true, params string[] ps)
        {
            return isLower ? string.Format(System.Web.Configuration.WebConfigurationManager.AppSettings[key] ?? "", ps).ToLower() : string.Format(System.Web.Configuration.WebConfigurationManager.AppSettings[key] ?? "", ps);
        }
    }
}
