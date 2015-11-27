using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbContextFactory
    {
        private static string efKey="ef_key";
        public static BaseDbContext<T> GetContext<T>() where T:class
        {
            BaseDbContext<T> db = CallContext.GetData(efKey) as BaseDbContext<T>;
            string dbConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["BaseDAO"].ConnectionString;
            if (db == null)
            {
                db = new BaseDbContext<T>(dbConnStr);
                CallContext.SetData(efKey, db);
            }
            return db;
        }

        public static void Dispose()
        { 
            DbContext db=(CallContext.GetData(efKey) as DbContext);
            if (db != null)
            {
                db.Dispose();
            }
        }
    }
}
