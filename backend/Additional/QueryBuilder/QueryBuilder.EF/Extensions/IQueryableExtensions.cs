using Microsoft.EntityFrameworkCore;
using QueryBuilder.EF.Enums;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions.Filter;
using QueryBuilder.QueryOptions.Includer;
using QueryBuilder.QueryOptions.Sorter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QueryBuilder.EF.Extensions
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
                        methodName = SortOperation.OrderBy.ToString();
                    }
                    else
                    {
                        methodName = SortOperation.ThenBy.ToString();
                    }
                    break;
                case SortOrder.Desc:
                    if (isFirst)
                    {
                        methodName = SortOperation.OrderByDescending.ToString();
                    }
                    else
                    {
                        methodName = SortOperation.ThenByDescending.ToString();
                    }
                    break;
                default:
                    methodName = string.Empty;
                    break;
            }

            return methodName;
        }
        private static IOrderedQueryable<T> CallOrderByQueryable<T>(IOrderedQueryable<T> query, string methodName, string propertyName)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Empty method name");
            }

            var param = Expression.Parameter(typeof(T), "x");
            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            IOrderedQueryable<T> result = (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), body.Type },
                    query.Expression,
                    Expression.Lambda(body, param)));

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
                methodName = IncludeOperation.Include.ToString();
            }
            else
            {
                methodName = IncludeOperation.IncludeThen.ToString();
            }

            return methodName;
        }
        private static IQueryable<T> CallIncludeQueryable<T>(IQueryable<T> query, string methodName, string propertyName)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Empty method name");
            }

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

            resultQuery = CallWhereQueryable(resultQuery, WhereOperation.Where.ToString(), filters);

            return resultQuery;
        }
        private static IQueryable<T> CallWhereQueryable<T>(IQueryable<T> query, string methodName, IList<QueryFilter> filters)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Empty method name");
            }

            Expression<Func<T, bool>> lambda = Tools.GetFilterExpression<T>(filters);

            IQueryable<T> result = (IQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T) },
                    query.Expression,
                    lambda));

            return result;
        }   
    }
}
