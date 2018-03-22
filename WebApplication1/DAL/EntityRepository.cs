using System;
using System.Linq.Expressions;
using System.Linq;
using System.Data;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Dal
{
    public class EntityRepository<T> where T : class
    {
        protected DbSet<T> dbSet;
        protected context context;

        public EntityRepository(context c)
        {
            this.context = c;
            this.dbSet = c.Set<T>();
        }

        /// <summary>
        /// 暴露dbSet便于跨表查询和直接执行T-SQL
        /// </summary>
        public DbSet<T> DbSet
        {
            get { return this.dbSet; }
        }

        /// <summary>
        /// 可override的Get方法
        /// </summary>
        /// <param name="pageIndex">页索引，从1开始</param>
        /// <param name="pageSize">每页的项数</param>
        /// <param name="filter">t=>t.id==10</param>
        /// <param name="orderBy">orderBy:q => q.OrderBy(d => d.UpdateDate)</param>
        /// <param name="includeProperties">要包含的导航属性</param>
        /// <returns>便于后续扩展，应返回IQueryable，返回IEnumerable就提前向数据库发送请求了</returns>
        public virtual IQueryable<T> Get(
            int? pageIndex = null,
            int? pageSize = null,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageIndex != null && pageSize != null)
            {
                return query
                    .Skip(Convert.ToInt32((pageIndex - 1) * pageSize))
                    .Take(Convert.ToInt32(pageSize));
            }
            else { return query; }
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
        }

    }
}
