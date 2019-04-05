using Lingva.BC.Common.Enums;
using Lingva.BC.DTO;
using Lingva.WebAPI.Models.Request;
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

        public QueryOptionsDTO Map(OptionsModel optionsModel)
        {
            QueryOptionsDTO queryOptions = new QueryOptionsDTO();
            queryOptions.Filters = new List<QueryFilterDTO>();

            QueryFilterDTO filter;
            filter = new QueryFilterDTO();
            filter.PropertyName = "Name";
            filter.PropertyValue = optionsModel.Name;
            filter.Operation = FilterOperation.Contains;
            queryOptions.Filters.Add(filter);

            filter = new QueryFilterDTO();
            filter.PropertyName = "LanguageId";
            filter.PropertyValue = optionsModel.LanguageId;
            filter.Operation = FilterOperation.Equal;
            queryOptions.Filters.Add(filter);

            queryOptions.Sorters = new List<QuerySorterDTO>();
            QuerySorterDTO sorter;
            sorter = new QuerySorterDTO();
            sorter.PropertyName = optionsModel.SortProperty;
            sorter.SortOrder = Enum.Parse<SortOrder>(optionsModel.SortOrder);
            queryOptions.Sorters.Add(sorter);

            queryOptions.Pagenator = new QueryPagenatorDTO();
            queryOptions.Pagenator.Skip = optionsModel.PageRecords * (optionsModel.Page - 1);
            queryOptions.Pagenator.Take = optionsModel.PageRecords;

            return queryOptions;
        }
    }
}
