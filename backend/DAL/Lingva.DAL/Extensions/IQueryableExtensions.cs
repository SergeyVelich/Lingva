using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lingva.DAL.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderBy", propertyName, comparer);
        }
        
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderByDescending", propertyName, comparer);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenBy", propertyName, comparer);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenByDescending", propertyName, comparer);
        }

        //public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, IEnumerable<IFilterModel> filterModel)
        //{
        //    var parameter = Expression.Parameter(typeof(T), "x");

        //    Expression expression = null;

        //    foreach (var filter in filterModel)
        //    {
        //        var property = Expression.Property(parameter, filter.PropertyName);

        //        var constant = Expression.Constant(filter.ValueForCompare);

        //        //var condition = new List<BinaryExpression>();

        //        Expression subExpression = null;

        //        switch (filter.SqlOpertaionForProperty)
        //        {
        //            case SqlOpertaion.Equal:
        //                subExpression = Expression.Equal(property, constant);
        //                break;
        //            case SqlOpertaion.NotEqual:
        //                subExpression = Expression.NotEqual(property, constant);
        //                break;
        //            case SqlOpertaion.Less:
        //                subExpression = Expression.LessThan(property, constant);
        //                break;
        //            case SqlOpertaion.More:
        //                subExpression = Expression.GreaterThan(property, constant);
        //                break;
        //            default: throw new ArgumentOutOfRangeException();
        //        }

        //        switch (filter.SqlCondition)
        //        {
        //            case SqlCondition.And:
        //                expression = expression == null ? subExpression : Expression.AndAlso(expression, subExpression);
        //                break;
        //            case SqlCondition.Or:
        //                expression = expression == null ? subExpression : Expression.OrElse(expression, subExpression);
        //                break;
        //            default: throw new ArgumentOutOfRangeException();
        //        }
        //    }
        //    var exp = Expression.Lambda<Func<T, bool>>(expression ?? throw new InvalidOperationException(), parameter).Compile();

        //    Debug.Write(expression);

        //    return collection.Where(exp);

        //}

        public static IOrderedQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName, string propertyName,
            IComparer<object> comparer = null)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return comparer != null
                ? (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param),
                        Expression.Constant(comparer)
                    )
                )
                : (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param)
                    )
                );
        }
    }
}
