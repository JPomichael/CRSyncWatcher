using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CRS.Sync.Watcher.DLL
{
     public static  class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
        /// <summary>
        /// 递归串联删除
        /// </summary>
        public static void CascadingDelete(this DataContext context, object entity)
        {
            Type _entityType = entity.GetType();
            var _propertyInfoArr = _entityType.GetProperties();


            var _forginKeyPropertys = _propertyInfoArr.Where(
                 c => c.GetCustomAttributes(true).Any(
                     attrbute => attrbute.GetType().Name == "AssociationAttribute")
                     & c.PropertyType.IsGenericType);//该属性必需是泛型

            //其他表有外键关联的记录
            foreach (var associationProperty in _forginKeyPropertys)
            {
                //获取Property值
                object propertyValue = associationProperty.GetValue(entity, null);
                //Property是EntitySet`1类型的值，如EntitySet<DataSetStructure>，
                //而EntitySet`1有IEnumerable接口
                IEnumerable enumerable = (IEnumerable)propertyValue;
                foreach (object o in enumerable)
                {
                    //递归
                    CascadingDelete(context, o);
                }
            }

            try
            {
                //删除没外键关联的记录
                context.GetTable(entity.GetType()).DeleteOnSubmit(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 获取匿名对象的属性值 create li 2012-12-25
        /// </summary>
        /// <param name="anounsObj">匿名对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static object getAnounsObjectPropertyValue(object anounsObj, string propertyName)
        {

            var _pinfo = from p in anounsObj.GetType().GetProperties()
                         where p.Name == propertyName
                         select p;
            PropertyInfo _PropertyInfo = _pinfo.First();
            return _PropertyInfo.GetValue(anounsObj, null);
        }
    }
}
