using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LogService
    {
        DAL.T_Logs_DAL dal = new DAL.T_Logs_DAL();
        public string WriteLog()
        {
            return "日志消息";
        }
    }
}
