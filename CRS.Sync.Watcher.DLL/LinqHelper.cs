using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRS.Sync.Watcher.DLL
{
    public class LinqHelper
    {
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>() where T : class
        {
            estay_ecsdbDataContext db = ConnHelper.estay_ecsdb();
            return db.GetTable<T>().ToList();

        }
        /// <summary>
        /// 获取指定的单个实体
        /// 如果不存在则返回null
        /// 如果存在多个则抛异常
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">Lamda表达式</param>
        /// <returns>Entity</returns>
        public T GetEntity<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            estay_ecsdbDataContext db = ConnHelper.estay_ecsdb();
            return db.GetTable<T>().Where<T>(predicate).SingleOrDefault();

        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="eneiey">实体对象</param>
        public void InsertEntity<T>(T eneiey) where T : class
        {
            using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
            {
                db.GetTable<T>().InsertOnSubmit(eneiey);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eneiey"></param>
        public void InsertEntity<T>(IEnumerable<T> eneiey) where T : class
        {
            using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
            {
                db.GetTable<T>().InsertAllOnSubmit(eneiey);
                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="predicate">Lamda表达式</param>
        public void DeleteEntity<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
            {
                T entity = db.GetTable<T>().Where(predicate).First();
                db.GetTable<T>().DeleteOnSubmit(entity);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="list">实体集合</param>
        public void DeletesEntity<T>(List<T> list) where T : class
        {
            using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
            {
                db.Transaction = db.Connection.BeginTransaction();
                try
                {
                    foreach (var item in list)
                    {
                        db.GetTable<T>().Attach(item);
                        db.Refresh(RefreshMode.KeepCurrentValues, item);
                    }
                    db.GetTable<T>().DeleteAllOnSubmit(list.AsEnumerable<T>());
                    db.SubmitChanges();
                    db.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    db.Transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void UpadateEntity<T>(T entity) where T : class
        {
            using (estay_ecsdbDataContext db = ConnHelper.estay_ecsdb())
            {
                db.GetTable<T>().Attach(entity);
                db.Refresh(RefreshMode.KeepCurrentValues, entity);
                db.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }
    }
}
