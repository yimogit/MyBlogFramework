using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DAL.Tests
{
    [TestClass]
    public class BaseDAOTests
    {

        DAL.T_Logs_DAL dal = new T_Logs_DAL();
        [TestMethod]
        public void TestLoadEntities()
        {
            Model.T_Logs p = new Model.T_Logs()
            {
                C_LogIP = "127.0.0.1",
                C_LogTime = DateTime.Now,
                C_LogContent = "测试"
            };
            p = dal.AddEntity(p);
            Assert.IsTrue(p.C_LID > 0);
            int total = -1;
            int count1=dal.LoadEntitiesCount(item => 1 == 1);
            int count2 = dal.LoadEntities(item => 1 == 1).Count();
            int count3 = dal.LoadPageEntities<string>(1, 10, out total, new Func<Model.T_Logs, bool>(item => 1 == 1), true, new Func<Model.T_Logs, string>(item => item.C_LogIP)).Count();
            Assert.IsTrue(count1 > 0 && count2 > 0 && count3 > 0);
            p.C_LogTime = DateTime.Now;
            Assert.IsTrue(dal.UpdateEntity(p)!=null);
            Assert.IsTrue(dal.DeleteEntity(p));
            Assert.IsTrue(dal.LoadFirstEntities(item=>item.C_LID==p.C_LID)==null);
        }
    }
}
