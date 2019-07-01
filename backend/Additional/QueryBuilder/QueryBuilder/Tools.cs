using QueryBuilder.Enums;
using QueryBuilder.QueryOptions.Filter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryBuilder
{
    public static class Tools
    {
        public static Expression<Func<T, bool>> GetFilterExpression<T>(IList<QueryFilter> filters)
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
        private static Expression GetFilterElementExpression<T>(QueryFilterElement filter, ParameterExpression paramExp)
        {          
            Expression propertyExp = null;
            Type typeForValue = null;

            string[] properties = filter.PropertyName.Split(".");
            foreach (string propertyName in properties)
            {
                propertyExp = Expression.Property(propertyExp ?? paramExp, propertyName);
                typeForValue = (typeForValue ?? typeof(T)).GetProperty(propertyName).PropertyType;
            };

            var constantExp = Expression.Constant(Convert.ChangeType(filter.PropertyValue, typeForValue));

            Expression expression;
            MethodInfo method;

            switch (filter.Operation)
            {
                case FilterElementOperation.Equal:
                    expression = Expression.Equal(propertyExp, constantExp);
                    break;
                case FilterElementOperation.NotEqual:
                    expression = Expression.NotEqual(propertyExp, constantExp);
                    break;
                case FilterElementOperation.LessThan:
                    expression = Expression.LessThan(propertyExp, constantExp);
                    break;
                case FilterElementOperation.LessThanOrEqual:
                    expression = Expression.LessThanOrEqual(propertyExp, constantExp);
                    break;
                case FilterElementOperation.GreaterThan:
                    expression = Expression.GreaterThan(propertyExp, constantExp);
                    break;
                case FilterElementOperation.GreaterThanOrEqual:
                    expression = Expression.GreaterThanOrEqual(propertyExp, constantExp);
                    break;
                case FilterElementOperation.Contains:
                    if(filter.Options != null && filter.Options.IgnoreCase)
                    {
                        method = typeof(string).GetMethod("ToLower", new Type[] { });
                        propertyExp = Expression.Call(propertyExp, method);
                    }
                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    expression = Expression.Call(propertyExp, method, constantExp);
                    break;
                case FilterElementOperation.NotContains:
                    if (filter.Options != null && filter.Options.IgnoreCase)
                    {
                        method = typeof(string).GetMethod("ToLower", new Type[] { });
                        propertyExp = Expression.Call(propertyExp, method);
                    }
                    method = typeof(string).GetMethod("NotContains", new[] { typeof(string) });
                    expression = Expression.Call(propertyExp, method, constantExp);
                    break;               
                default: throw new ArgumentOutOfRangeException();
            };

            return expression;
        }
    }
}
