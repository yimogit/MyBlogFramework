using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class BaseDAO<T> where T:class
    {
        /// <summary>
        /// 数据库操作上下文
        /// </summary>
        protected BaseDbContext<T> db { get; set; }

        public BaseDAO()
        {
            db = DbContextFactory.GetContext<T>();
        }
        /// <summary>
        /// 实现对数据库的新增功能
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSaveChange">是否自动提交</param>
        /// <returns></returns>
        public T AddEntity(T entity, bool isSaveChange = true)
        {
            db.dbSet.Add(entity);
            if (isSaveChange)
                return db.SaveChanges() > 0 ? entity : null;
            else
                return entity;
        }
        /// <summary>
        /// 实现对数据库的修改功能
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSaveChange">是否自动提交</param>
        /// <returns></returns>
        public T UpdateEntity(T entity, bool isSaveChange = true)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            if (isSaveChange)
                return db.SaveChanges() > 0 ? entity : null;
            else
                return entity;
        }

        /// <summary>
        /// 实现对数据库的查询  --简单查询
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns></returns>
        public T LoadFirstEntities(Func<T, bool> whereLambda)
        {
            return db.Set<T>().FirstOrDefault<T>(whereLambda);
        }
        /// <summary>
        /// 实现对数据库的删除功能
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSaveChange">是否自动提交</param>
        /// <returns></returns>
        public bool DeleteEntity(T entity, bool isSaveChange = true)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            if (isSaveChange)
                return db.SaveChanges() > 0 ? true : false;
            else
                return true;
        }
        /// <summary>
        /// 实现对数据库的查询  --简单查询
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Func<T, bool> whereLambda)
        {

            return db.Set<T>().Where<T>(whereLambda).AsQueryable();
        }
        /// <summary>
        /// 查询满足条件的条数
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns></returns>
        public int LoadEntitiesCount(Func<T, bool> whereLambda)
        {
            return db.Set<T>().Count(whereLambda);
        }
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        /// <typeparam name="S">按照某个类进行排序</typeparam>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">一页显示多少条数据</param>
        /// <param name="total">总条数</param>
        /// <param name="whereLambda">取得排序的条件</param>
        /// <param name="isAsc">如何排序，根据倒叙还是升序</param>
        /// <param name="orderByLambda">根据那个字段进行排序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out  int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            var temp = db.Set<T>().Where<T>(whereLambda);
            total = temp.Count(); //得到总的条数
            //排序,获取当前页的数据
            if (isAsc)
            {
                temp = temp.OrderBy<T, S>(orderByLambda)
                    .Skip<T>(pageSize * (pageIndex - 1))
                    .Take<T>(pageSize).AsQueryable();
            }
            else
            {
                temp = temp.OrderByDescending<T, S>(orderByLambda)
                    .Skip<T>(pageSize * (pageIndex - 1))
                    .Take<T>(pageSize).AsQueryable();
            }
            return temp.AsQueryable();
        }

        /// <summary>
        /// 执行sql 生成DataTable
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="type">执行命令类型</param>
        /// <returns></returns>
        protected System.Data.DataTable Query2DataTable(string sql, System.Data.CommandType type)
        {
            System.Data.IDbCommand command = db.Database.Connection.CreateCommand();
            System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            dataAdapter.SelectCommand = (System.Data.SqlClient.SqlCommand)command;
            System.Data.DataTable dt = new System.Data.DataTable("dt1");
            if (command.Connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            command.CommandText = sql;
            command.CommandType = type;
            //System.Data.IDataReader reader = command.ExecuteReader();
            dataAdapter.Fill(dt);
            if (command.Connection.State == System.Data.ConnectionState.Open)
            {
                command.Connection.Close();
            }
            return dt;
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
