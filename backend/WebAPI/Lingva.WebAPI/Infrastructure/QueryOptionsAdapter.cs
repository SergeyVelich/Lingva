using Lingva.WebAPI.Models.Request;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class QueryOptionsAdapter
    {
        public QueryOptionsAdapter()
        {

        }

        public QueryOptions Map(OptionsModel optionsModel)
        {
            List<QueryFilter> filters = new List<QueryFilter>();
            filters.Add(new QueryFilter("Name", optionsModel.Name, FilterOperation.Contains));
            filters.Add(new QueryFilter("LanguageId", optionsModel.LanguageId, FilterOperation.Equal));

            List<QuerySorter> sorters = new List<QuerySorter>();
            SortOrder sortOrder = Enum.Parse<SortOrder>(optionsModel.SortOrder);
            sorters.Add(new QuerySorter(optionsModel.SortProperty, sortOrder));

            List<QueryIncluder> includers = new List<QueryIncluder>();
            includers.Add(new QueryIncluder("Language"));

            int take = optionsModel.PageRecords;
            int skip = optionsModel.PageRecords * (optionsModel.Page - 1);
            QueryPagenator pagenator = new QueryPagenator(take, skip);

            QueryOptions queryOptions = new QueryOptions(filters, sorters, includers, pagenator);

            return queryOptions;
        }
    }
}
