namespace QueryBuilder.Mongo.Enums
{
    public enum SortOperation
    {
        SortBy,
        ThenBy,
        SortByDescending,
        ThenByDescending,
    }

    public enum IncludeOperation
    {
        Include,
        IncludeThen,
    }
    public enum WhereOperation
    {
        Find,
    }

}
