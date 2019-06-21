using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QueryBuilder.Enums;
using QueryBuilder.Mongo.Enums;
using QueryBuilder.QueryOptions.Filter;
using QueryBuilder.QueryOptions.Sorter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

            return collection.Find(Tools.GetFilterExpression<T>(filters));

            //var d = new MongoQueryProvider(collection as MongoCollection).CreateQuery(
            //    Expression.Call(
            //        typeof(IMongoCollectionExtensions),
            //        "Find",
            //        new[] { typeof(T), typeof(T) },
            //        Expression.Constant(collection, typeof(MongoCollection)),
            //        GetFilterExpression<T>(filters)));

            return null;
        }
    }
}
