using System.Linq;
using System.Linq.Expressions;

namespace ABPProject.Extend
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 排序扩展方法（字符串方式指定排序字段）
        /// </summary>
        /// <param name="propertyName">排序字段</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName)
        {
            return OrderBy(queryable, propertyName, false);
        }
        /// <summary>
        /// 排序扩展方法（字符串方式指定排序字段）
        /// </summary>
        /// <param name="propertyName">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName, bool desc)
        {
            var param = Expression.Parameter(typeof(T));
            var body = Expression.Property(param, propertyName);
            dynamic keySelector = Expression.Lambda(body, param);
            return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
        }
    }
}
