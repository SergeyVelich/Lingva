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
            List<QueryFilterDTO> filters = new List<QueryFilterDTO>();
            filters.Add(new QueryFilterDTO("Name", optionsModel.Name, FilterOperation.Contains));
            filters.Add(new QueryFilterDTO("LanguageId", optionsModel.LanguageId, FilterOperation.Equal));

            List<QuerySorterDTO> sorters = new List<QuerySorterDTO>();
            sorters.Add(new QuerySorterDTO(optionsModel.SortProperty, optionsModel.SortOrder));

            List<QueryIncluderDTO> includers = new List<QueryIncluderDTO>();
            sorters.Add(new QueryIncluderDTO(optionsModel.SortProperty));

            int take = optionsModel.PageRecords;
            int skip = optionsModel.PageRecords * (optionsModel.Page - 1);
            QueryPagenatorDTO pagenator = new QueryPagenatorDTO(take, skip);

            QueryOptionsDTO queryOptions = new QueryOptionsDTO(filters, sorters, pagenator);

            return queryOptions;
        }
    }
}
