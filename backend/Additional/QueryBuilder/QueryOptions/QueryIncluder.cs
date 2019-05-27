﻿using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions
{
    [ExcludeFromCodeCoverage]
    public class QueryIncluder
    {
        public string PropertyName { get; set; }

        public QueryIncluder(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
