using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QueryBuilder.Enums;
using QueryBuilder.Mongo.Enums;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryBuilder.Mongo.Extensions
{
    public static class IMongoCollectionExtensions
    {
        public static IOrderedFindFluent<T,T> SortBy<T>(this IFindFluent<T,T> query, IList<QuerySorter> sorters)
        {
            IOrderedFindFluent<T,T> resultQuery = (IOrderedFindFluent<T,T>)query;

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
                        methodName = SortOperation.SortBy.ToString();
                    }
                    else
                    {
                        methodName = SortOperation.ThenBy.ToString();
                    }
                    break;
                case SortOrder.Desc:
                    if (isFirst)
                    {
                        methodName = SortOperation.SortByDescending.ToString();
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
        private static IOrderedFindFluent<T,T> CallOrderByQueryable<T>(IOrderedFindFluent<T,T> query, string methodName, string propertyName)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Empty method name");
            }

            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, propertyName);
            var propertyInfo = typeof(T).GetProperty(propertyName);
            var typeForValue = propertyInfo.PropertyType;
            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);
            var funcType = typeof(Func<,>).MakeGenericType(typeof(T), typeof(object));

            LambdaExpression lambda;
            if (typeForValue.IsClass == false && typeForValue.IsInterface == false)
                lambda = Expression.Lambda(funcType, Expression.Convert(property, typeof(object)), param);
            else
                lambda = Expression.Lambda(funcType, property, param);

            IOrderedFindFluent<T,T> result;
            switch (methodName)
            {
                case "SortBy":
                    result = query.SortBy((Expression<Func<T, object>>)lambda);
                    break;
                case "SortByDescending":
                    result = query.SortByDescending((Expression<Func<T, object>>)lambda);
                    break;
                case "ThenBy":
                    result = query.ThenBy((Expression<Func<T, object>>)lambda);
                    break;
                case "ThenByDescending":
                    result = query.ThenByDescending((Expression<Func<T, object>>)lambda);
                    break;
                default:
                    result = query;
                    break;
            }

            return result;
        }

        public static IFindFluent<T,T> Find<T>(this IMongoCollection<T> collection, IList<QueryFilter> filters)
        {
            IMongoCollection<T> resultCollection = collection;
            var filterBson = new BsonDocument();

            if (filters == null)
            {
                return resultCollection.Find(filterBson);
            }
            if (filters.Count == 0)
            {
                return resultCollection.Find(filterBson);
            }

            return collection.Find(GetFilterExpression<T>(filters));

            //var d = new MongoQueryProvider(collection as MongoCollection).CreateQuery(
            //    Expression.Call(
            //        typeof(IMongoCollectionExtensions),
            //        "Find",
            //        new[] { typeof(T), typeof(T) },
            //        Expression.Constant(collection, typeof(MongoCollection)),
            //        GetFilterExpression<T>(filters)));

            return null;
        }
        private static Expression<Func<T, bool>> GetFilterExpression<T>(IList<QueryFilter> filters)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression expression = GetFilterGroupExpression<T>(filters, param);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(expression ?? throw new InvalidOperationException(), param);

            return lambda;
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
