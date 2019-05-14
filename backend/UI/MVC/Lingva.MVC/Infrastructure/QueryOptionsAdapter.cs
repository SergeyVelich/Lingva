﻿using Lingva.MVC.Models.Request;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Infrastructure
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
            if (optionsModel.Name != null && optionsModel.Name.Trim().Length > 0)
                filters.Add(new QueryFilter("Name", optionsModel.Name, FilterOperation.Contains));
            if (optionsModel.LanguageId > 0)
                filters.Add(new QueryFilter("LanguageId", optionsModel.LanguageId, FilterOperation.Equal));

            List<QuerySorter> sorters = new List<QuerySorter>();
            SortOrder sortOrder = Enum.Parse<SortOrder>(optionsModel.SortOrder);
            sorters.Add(new QuerySorter(optionsModel.SortProperty, sortOrder));

            List<QueryIncluder> includers = new List<QueryIncluder>();
            includers.Add(new QueryIncluder("Language"));

            int take = optionsModel.PageRecords;
            int skip = optionsModel.PageRecords * (optionsModel.Page - 1);
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