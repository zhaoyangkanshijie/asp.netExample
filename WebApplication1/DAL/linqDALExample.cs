//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using CRM.Model;
//using System.Configuration;
//using System.Linq.Expressions;
//using System.Data.SqlClient;
//using System.Data.Linq;

//namespace CRM.DAL
//{
//    /// <summary>
//    /// Linq数据访问层
//    /// </summary>
//    public class LinqHelper
//    {
//        /// <summary>
//        /// 获得数据上下文
//        /// </summary>
//        /// <returns></returns>
//        public CRMDBDataContext GetDataContext()
//        {
            
//            string conn = ConfigurationManager.ConnectionStrings["LinqConnection"].ConnectionString;
//            //string conn = "server=.;uid=sa;pwd=123456;database=lf_crm_db2";
//            CRMDBDataContext crmDB = new CRMDBDataContext(conn);
//            crmDB.Connection.Open();
//            return crmDB;
//        }
//        /// <summary>
//        /// 获取所有的数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public List<T> GetList<T>() where T : class
//        {
//            var db = GetDataContext();
//            if (!IsAtOnceLoad.IsLazyLoad)//为false时立即加载
//            {
//                return db.GetTable<T>().ToList();
//            }
//            else
//            {
//                db.DeferredLoadingEnabled = false;
//                return db.GetTable<T>().ToList();
//            }
//        }
//        /// <summary>
//        /// 获取指定的单个实体
//        /// 如果不存在则返回null
//        /// 如果存在多个则抛异常
//        /// </summary>
//        /// <typeparam name="T">实体类型</typeparam>
//        /// <param name="predicate">Lamda表达式</param>
//        /// <returns>Entity</returns>
//        public T GetEntity<T>(Expression<Func<T, bool>> predicate) where T : class
//        {
//            var db = GetDataContext();
//            if (!IsAtOnceLoad.IsLazyLoad)
//            {
//                return db.GetTable<T>().Where<T>(predicate).SingleOrDefault();
//            }
//            else
//            {
//                db.DeferredLoadingEnabled = false;
//                return db.GetTable<T>().Where<T>(predicate).SingleOrDefault();
//            }
//        }
//        /// <summary>
//        /// 用SQL语句查询
//        /// </summary>
//        /// <typeparam name="T">实体类类型</typeparam>
//        /// <param name="sql">sql语句</param>
//        /// <param name="parameters">sql参数</param>
//        /// <returns>集合</returns>
//        public List<T> GetListBySql<T>(string sql, params SqlParameter[] parameters) where T : class
//        {
//            var db = GetDataContext();
//            if (!IsAtOnceLoad.IsLazyLoad)
//            {
//                return db.ExecuteQuery<T>(sql).ToList();
//            }
//            else
//            {
//                db.DeferredLoadingEnabled = false;
//                return db.ExecuteQuery<T>(sql).ToList();
//            }
//        }

//        /// <summary>
//        /// 添加实体
//        /// </summary>
//        /// <typeparam name="T">实体类类型</typeparam>
//        /// <param name="eneiey">实体对象</param>
//        public void InsertEntity<T>(T eneiey) where T : class
//        {
//            using (CRMDBDataContext db = GetDataContext())
//            {
//                db.GetTable<T>().InsertOnSubmit(eneiey);
//                db.SubmitChanges();
//            }
//        }
//        /// <summary>
//        /// 删除实体
//        /// </summary>
//        /// <typeparam name="T">实体类类型</typeparam>
//        /// <param name="predicate">Lamda表达式</param>
//        public void DeleteEntity<T>(Expression<Func<T, bool>> predicate) where T : class
//        {
//            using (CRMDBDataContext db = GetDataContext())
//            {
//                T entity = db.GetTable<T>().Where(predicate).First();
//                db.GetTable<T>().DeleteOnSubmit(entity);
//                db.SubmitChanges();
//            }
//        }
//        /// <summary>
//        /// 批量删除
//        /// </summary>
//        /// <typeparam name="T">实体类类型</typeparam>
//        /// <param name="list">实体集合</param>
//        public void DeletesEntity<T>(List<T> list) where T : class
//        {
//            using (CRMDBDataContext db = GetDataContext())
//            {
//                db.Transaction = db.Connection.BeginTransaction();
//                try
//                {
//                    foreach (var item in list)
//                    {
//                        db.GetTable<T>().Attach(item);
//                        db.Refresh(RefreshMode.KeepCurrentValues, item);
//                    }
//                    db.GetTable<T>().DeleteAllOnSubmit(list.AsEnumerable<T>());
//                    db.SubmitChanges();
//                    db.Transaction.Commit();
//                }
//                catch (Exception ex)
//                {
//                    db.Transaction.Rollback();
//                    throw new Exception(ex.Message);
//                }
//            }
//        }

//        /// <summary>
//        /// 修改实体
//        /// </summary>
//        /// <typeparam name="T">实体类类型</typeparam>
//        /// <param name="entity">实体对象</param>
//        public void UpadateEntity<T>(T entity) where T : class
//        {
//            using (CRMDBDataContext db = GetDataContext())
//            {
//                db.GetTable<T>().Attach(entity);
//                db.Refresh(RefreshMode.KeepCurrentValues, entity);
//                db.SubmitChanges(ConflictMode.ContinueOnConflict);
//            }
//        }
//    }
//}