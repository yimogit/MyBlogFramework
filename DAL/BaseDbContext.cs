using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDbContext<T> : System.Data.Entity.DbContext where T : class
    {
        public DbSet<T> dbSet { get; set; }
        static BaseDbContext()
        {
            System.Data.Entity.Database.SetInitializer<BaseDbContext<T>>(null);
        }
        public BaseDbContext(string connectstring)
            : base(connectstring)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 禁用默认表名复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 禁用一对多级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 禁用多对多级联删除
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //为T_Logs添加主键
            modelBuilder.Entity<Model.T_Logs>().HasKey(d => d.C_LID);
            //base.OnModelCreating(modelBuilder);
        }

    }
}
