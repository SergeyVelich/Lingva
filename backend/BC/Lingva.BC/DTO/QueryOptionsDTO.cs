using Lingva.BC.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Lingva.BC.DTO
{
    public class QueryOptionsDTO
    {
        private readonly ICollection<QueryFilterDTO> _filters;
        private readonly ICollection<QuerySorterDTO> _sorters;
        private readonly QueryPagenatorDTO _pagenator;

        public QueryPagenatorDTO Pagenator => _pagenator;

        public QueryOptionsDTO(ICollection<QueryFilterDTO> filters, ICollection<QuerySorterDTO> sorters, QueryPagenatorDTO pagenator)
        {
            _filters = filters;
            _sorters = sorters;
            _pagenator = pagenator;
        }
        
        public Expression<Func<T, bool>> GetFiltersExpression<T>()
        {
            if (_filters == null)
            {
                return null;
            }
            if (_filters.Count == 0)
            {
                return null;
            }

            Expression<Func<T, bool>> exp = null;
            Expression expression = null;
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var filter in _filters)
            {
                if (filter.PropertyValue == null)
                {
                    continue;
                }

                var property = Expression.Property(parameter, filter.PropertyName);
                var propertyInfo = typeof(T).GetProperty(filter.PropertyName);
                var typeForValue = propertyInfo.PropertyType;
                var constant = Expression.Constant(Convert.ChangeType(filter.PropertyValue, typeForValue));

                Expression subExpression = null;

                switch (filter.Operation)
                {
                    case FilterOperation.Equal:
                        subExpression = Expression.Equal(property, constant);
                        break;
                    case FilterOperation.NotEqual:
                        subExpression = Expression.NotEqual(property, constant);
                        break;
                    case FilterOperation.Less:
                        subExpression = Expression.LessThan(property, constant);
                        break;
                    case FilterOperation.More:
                        subExpression = Expression.GreaterThan(property, constant);
                        break;
                    case FilterOperation.Contains:
                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        subExpression = Expression.Call(property, method, constant);
                        break;
                    case FilterOperation.NotContains:
                        method = typeof(string).GetMethod("NotContains", new[] { typeof(string) });
                        subExpression = Expression.Call(property, method, constant);
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }

                expression = expression == null ? subExpression : Expression.AndAlso(expression, subExpression);
            }

            if (expression != null)
            {
                exp = Expression.Lambda<Func<T, bool>>(expression ?? throw new InvalidOperationException(), parameter);
            }

            return exp;
        }

        public ICollection<string> GetSortersCollection<T>()
        {
            if (_sorters == null)
            {
                return null;
            }
            if (_sorters.Count == 0)
            {
                return null;
            }

            ICollection<string> sorters = new List<string>();//??
            foreach (var sorter in _sorters)
            {
                sorters.Add(sorter.PropertyName + " " + sorter.SortOrder.ToString());
            }

            return sorters;
        }
    }
}
