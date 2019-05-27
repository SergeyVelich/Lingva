using Microsoft.EntityFrameworkCore;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryBuilder.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, IList<QuerySorter> sorters)
        {
            IOrderedQueryable<T> resultQuery = (IOrderedQueryable<T>)query;

            if (sorters == null)
            {
                return resultQuery;
            }
            if (sorters.Count == 0)
            {
                return resultQuery;
            }
            
            bool isFirst = true;
            foreach (QuerySorter sorter in sorters)
            {
                string methodName = GetOrderByMethodName(sorter.SortOrder, isFirst);
                resultQuery = CallOrderByQueryable(resultQuery, methodName, sorter.PropertyName);
                isFirst = false;
            }

            return resultQuery;
        }
        private static string GetOrderByMethodName(SortOrder sortOrder = SortOrder.Asc, bool isFirst = true)
        {
            string methodName;

            switch (sortOrder)
            {
                case SortOrder.Asc:
                    if (isFirst)
                    {
                        methodName = EFSortOperation.OrderBy.ToString();
                    }
                    else
                    {
                        methodName = EFSortOperation.ThenBy.ToString();
                    }
                    break;
                case SortOrder.Desc:
                    if (isFirst)
                    {
                        methodName = EFSortOperation.OrderByDescending.ToString();
                    }
                    else
                    {
                        methodName = EFSortOperation.ThenByDescending.ToString();
                    }
                    break;
                default:
                    methodName = string.Empty;
                    break;
            }

            return methodName;
        }
        private static IOrderedQueryable<T> CallOrderByQueryable<T>(IOrderedQueryable<T> query, string methodName, string propertyName, IComparer<object> comparer = null)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Empty method name");
            }

            var param = Expression.Parameter(typeof(T), "x");
            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            IOrderedQueryable<T> result = comparer != null
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

            return result;
        }        

        public static IQueryable<T> Include<T>(this IQueryable<T> query, IList<QueryIncluder> includers)
        {
            IQueryable<T> resultQuery = query;

            if (includers == null)
            {
                return resultQuery;
            }
            if (includers.Count == 0)
            {
                return resultQuery;
            }
           
            foreach (QueryIncluder includer in includers)
            {
                string[] includerDetails = includer.PropertyName.Split('.');
                bool isFirst = true;
                foreach (string includerDetail in includerDetails)
                {
                    string methodName = GetIncludeMethodName(isFirst);
                    resultQuery = CallIncludeQueryable(resultQuery, methodName, includer.PropertyName);
                    isFirst = false;
                };
            }

            return resultQuery;
        }
        private static string GetIncludeMethodName(bool isFirst = true)
        {
            string methodName;

            if (isFirst)
            {
                methodName = EFIncludeOperation.Include.ToString();
            }
            else
            {
                methodName = EFIncludeOperation.IncludeThen.ToString();
            }

            return methodName;
        }
        private static IQueryable<T> CallIncludeQueryable<T>(IQueryable<T> query, string methodName, string propertyName)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Empty method name");
            }

            //var param = Expression.Parameter(typeof(T), "x");
            //var constant = Expression.Constant(propertyName);

            //IQueryable<T> result = (IQueryable<T>)query.Provider.CreateQuery(
            //    Expression.Call(
            //        typeof(EntityFrameworkQueryableExtensions),
            //        methodName,
            //        new[] { typeof(T) },
            //        query.Expression,
            //        constant));

            var param = Expression.Parameter(typeof(T), "x");
            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);
            var lambda = Expression.Lambda(body, param);

            IQueryable<T> result = (IQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(EntityFrameworkQueryableExtensions), //static class that contains the method
                    methodName,                                 //method name
                    new[] { typeof(T), body.Type },             //method generic types
                    query.Expression,                           //method parameters as expression
                    lambda));

            return result;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, IList<QueryFilter> filters)
        {
            IQueryable<T> resultQuery = query;

            if (filters == null)
            {
                return resultQuery;
            }
            if (filters.Count == 0)
            {
                return resultQuery;
            }

            string methodName = GetWhereMethodName();
            resultQuery = CallWhereQueryable(resultQuery, methodName, filters);

            return resultQuery;
        }
        private static string GetWhereMethodName()
        {
            string methodName;
            methodName = EFWhereOperation.Where.ToString();
            return methodName;
        }
        private static IQueryable<T> CallWhereQueryable<T>(IQueryable<T> query, string methodName, IList<QueryFilter> filters)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Empty method name");
            }

            var param = Expression.Parameter(typeof(T), "x");

            Expression expression = GetFilterGroupExpression<T>(filters, param);
            
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(expression ?? throw new InvalidOperationException(), param);

            IQueryable<T> result = (IQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T) },
                    query.Expression,
                    lambda));

            return result;
        }
        private static Expression GetFilterGroupExpression<T>(IList<QueryFilter> filters, ParameterExpression param, FilterGroupOperation operation = FilterGroupOperation.And)
        {
            Expression expression = null;

            foreach (QueryFilter filter in filters)
            {
                Expression subExpression = null;
                if (filter is QueryFilterGroup filterGroup)
                {
                    subExpression = GetFilterGroupExpression<T>(filterGroup.FilterElements, param, operation);
                }
                else
                {
                    subExpression = GetFilterElementExpression<T>((QueryFilterElement)filter, param);
                }

                switch (operation)
                {
                    case FilterGroupOperation.And:
                        expression = expression == null ? subExpression : Expression.AndAlso(expression, subExpression);
                        break;
                    case FilterGroupOperation.Or:
                        expression = expression == null ? subExpression : Expression.OrElse(expression, subExpression);
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            return expression;
        }
        private static Expression GetFilterElementExpression<T>(QueryFilterElement filter, ParameterExpression param)
        {
            Expression expression;

            var property = Expression.Property(param, filter.PropertyName);
            var propertyInfo = typeof(T).GetProperty(filter.PropertyName);
            var typeForValue = propertyInfo.PropertyType;
            var constant = Expression.Constant(Convert.ChangeType(filter.PropertyValue, typeForValue));

            switch (filter.Operation)
            {
                case FilterElementOperation.Equal:
                    expression = Expression.Equal(property, constant);
                    break;
                case FilterElementOperation.NotEqual:
                    expression = Expression.NotEqual(property, constant);
                    break;
                case FilterElementOperation.LessThan:
                    expression = Expression.LessThan(property, constant);
                    break;
                case FilterElementOperation.LessThanOrEqual:
                    expression = Expression.LessThanOrEqual(property, constant);
                    break;
                case FilterElementOperation.GreaterThan:
                    expression = Expression.GreaterThan(property, constant);
                    break;
                case FilterElementOperation.GreaterThanOrEqual:
                    expression = Expression.GreaterThanOrEqual(property, constant);
                    break;
                case FilterElementOperation.Contains:
                    MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    expression = Expression.Call(property, method, constant);
                    break;
                case FilterElementOperation.NotContains:
                    method = typeof(string).GetMethod("NotContains", new[] { typeof(string) });
                    expression = Expression.Call(property, method, constant);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            return expression;
        }
    }
}
