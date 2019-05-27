﻿namespace QueryBuilder.Enums
{
    public enum SortOrder
    {
        Asc,
        Desc,
    }

    public enum FilterOperation
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Contains,
        NotContains,
    }

    public enum EFSortOperation
    {
        OrderBy,
        ThenBy,
        OrderByDescending,
        ThenByDescending,
    }
}
