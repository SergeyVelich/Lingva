using Lingva.ASP.Infrastructure.Models;
using Lingva.Common.Extensions;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.ASP.Infrastructure.Adapters
{
    [ExcludeFromCodeCoverage]
    public class QueryOptionsAdapter
    {
        public QueryOptionsAdapter()
        {

        }

        public virtual IQueryOptions Map(GroupsListOptionsModel optionsModel)
        {
            List<QueryFilter> filters = new List<QueryFilter>();
            if (optionsModel.Name != null)
            {
                filters.Add(new QueryFilterElement("Name", optionsModel.Name, FilterElementOperation.Contains));
            }                
            if (optionsModel.LanguageId != null && optionsModel.LanguageId != 0)
            {
                filters.Add(new QueryFilterElement("LanguageId", optionsModel.LanguageId, FilterElementOperation.Equal));
            }               
            if (optionsModel.DateFrom != null || optionsModel.DateTo != null)
            {
                filters.Add(new QueryFilterElement("Date", optionsModel.DateFrom?.AbsoluteStart() ?? DateTime.MinValue, FilterElementOperation.GreaterThanOrEqual));
                filters.Add(new QueryFilterElement("Date", optionsModel.DateTo?.AbsoluteEnd() ?? DateTime.MaxValue, FilterElementOperation.LessThanOrEqual));
            }              

            List<QuerySorter> sorters = new List<QuerySorter>();
            SortOrder sortOrder = Enum.Parse<SortOrder>(optionsModel.SortOrder, true);
            sorters.Add(new QuerySorter(optionsModel.SortProperty, sortOrder));

            List<QueryIncluder> includers = new List<QueryIncluder>();
            includers.Add(new QueryIncluder("Language"));

            int take = optionsModel.PageSize;
            int skip = optionsModel.PageSize * (optionsModel.PageIndex - 1);
            QueryPagenator pagenator = new QueryPagenator(take, skip);

            IQueryOptions queryOptions = new QueryOptions(
                filters: filters,
                sorters: sorters,
                includers: includers,
                pagenator: pagenator);

            return queryOptions;
        }
    }
}
