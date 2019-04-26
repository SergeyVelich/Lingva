using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QueryBuilder.QueryOptions
{
    public interface IQueryOptions
    {
        QueryPagenator Pagenator { get; }

        Expression<Func<T, bool>> GetFiltersExpression<T>();

        ICollection<string> GetSortersCollection<T>();

        ICollection<Expression<Func<T, bool>>> GetIncludersCollection<T>();               
    }
}
