namespace QueryBuilder.Enums
{
    public enum SortOrder
    {
        Asc,
        Desc,
    }

    public enum FilterElementOperation
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

    public enum FilterGroupOperation
    {
        And,
        Or,
    }

    public enum EFSortOperation
    {
        OrderBy,
        ThenBy,
        OrderByDescending,
        ThenByDescending,
    }

    public enum EFIncludeOperation
    {
        Include,
        IncludeThen,
    }
    public enum EFWhereOperation
    {
        Where,
    }

}
