using Dapper;
using Lingva.DAL.Entities;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper
{
    public class DapperSet<T> where T : BaseBE, new()
    {
        private readonly DapperContext _dbContext;
        private readonly string _tableName;


        public DapperSet(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            IEnumerable<T> result = await _dbContext.Connection.QueryAsync<T>(SqlSelectAll());
            return result;
        }
        public async Task<T> FindAsync(int id)
        {
            T result = await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(SqlFind(), new { Id = id });
            return result;
        }
        public async Task<T> AddAsync(T entity, IDbTransaction transaction = null)
        {
            T result = await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(SqlAdd(entity), new { entity }, transaction: transaction);
            return result;

            //try
            //{
            //    var rowsAffected = _dbConnection.Execute(SqlInsert<T>(entity), this);

            //    if (PrimaryKeyPropertyInfo != null)
            //    {
            //        var pinfo = PrimaryKeyPropertyInfo;
            //        object[] columnAttributes = pinfo.GetCustomAttributes(typeof(ColumnAttribute), true);
            //        if (columnAttributes.Length == 1)
            //        {
            //            var columnAttribute = columnAttributes[0] as ColumnAttribute;
            //            if (columnAttribute != null && columnAttribute.IsPrimaryKey && columnAttribute.IsAutoIncrement)
            //            {
            //                dynamic identity = _dbConnection.Query("SELECT @@IDENTITY AS Id").Single();
            //                pinfo.SetValue(this, Convert.ChangeType(identity.Id, TypeCode.Int32), null);
            //                var i = identity.Id;
            //            }
            //        }
            //    }
            //    return rowsAffected;
            //}
            //catch (Exception)
            //{
            //}
            //return -1;
        }
        public async Task<T> UpdateAsync(T entity, IDbTransaction transaction = null)
        {
            T result = await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(SqlUpdate(entity), new { entity }, transaction: transaction);
            return result;
        }
        public async Task RemoveAsync(T entity, IDbTransaction transaction = null)
        {
            await _dbContext.Connection.ExecuteAsync(SqlRemove(entity), new { entity.Id }, transaction: transaction);
        }


        protected string SqlSelectAll()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.*");
            queryStringBuilder.AppendLine("FROM Groups AS g");

            return queryStringBuilder.ToString();

            //try
            //{
            //    var stringBuilder = new StringBuilder();
            //    var fieldsSql = new StringBuilder("");
            //    var fromSql = new StringBuilder();
            //    Type type = this.GetType();
            //    object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            //    if (tableAttributes.Length == 1)
            //    {
            //        stringBuilder.Append(String.Format("SELECT "));
            //        fromSql.Append(String.Format(" from {0}", ((TableAttribute)tableAttributes[0]).Name));
            //        int fieldCount = 0;
            //        foreach (var propertyInfo in type.GetProperties())
            //        {
            //            object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);
            //            if (columnAttributes.Length == 1)
            //            {
            //                var columnAttribute = columnAttributes[0] as ColumnAttribute;
            //                if (fieldCount == 0)
            //                {
            //                    if (columnAttribute != null)
            //                    {
            //                        fieldsSql.Append(columnAttribute.Name);
            //                    }

            //                }
            //                else
            //                {
            //                    if (columnAttribute != null)
            //                    {
            //                        fieldsSql.Append("," + columnAttribute.Name);
            //                    }

            //                }
            //                fieldCount++;
            //            }
            //        }
            //        stringBuilder.Append(fieldsSql);
            //        stringBuilder.Append(fromSql);
            //    }
            //    return stringBuilder.ToString();
            //}
            //catch (Exception ex)
            //{
            //    //PMLogger.Logger.DebugWriteException("BasicModelTemplate.GetSqliteSelect", ex);
            //}
            //return null;
        }
        protected string SqlFind()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.*");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            queryStringBuilder.AppendLine("WHERE g.Id = @Id");

            return queryStringBuilder.ToString(); ;
        }
        protected string SqlSelect()
        {
            //try
            //{
            //    var stringBuilder = new StringBuilder();
            //    var fieldsSql = new StringBuilder("");
            //    var fromSql = new StringBuilder();
            //    Type type = this.GetType();
            //    object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            //    if (tableAttributes.Length == 1)
            //    {
            //        stringBuilder.Append(String.Format("SELECT "));
            //        fromSql.Append(String.Format(" from {0}", ((TableAttribute)tableAttributes[0]).Name));
            //        int fieldCount = 0;
            //        foreach (var propertyInfo in type.GetProperties())
            //        {
            //            object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);
            //            if (columnAttributes.Length == 1)
            //            {
            //                var columnAttribute = columnAttributes[0] as ColumnAttribute;
            //                if (fieldCount == 0)
            //                {
            //                    if (columnAttribute != null)
            //                    {
            //                        fieldsSql.Append(columnAttribute.Name);
            //                    }

            //                }
            //                else
            //                {
            //                    if (columnAttribute != null)
            //                    {
            //                        fieldsSql.Append("," + columnAttribute.Name);
            //                    }

            //                }
            //                fieldCount++;
            //            }
            //        }
            //        stringBuilder.Append(fieldsSql);
            //        stringBuilder.Append(fromSql);
            //    }
            //    return stringBuilder.ToString();
            //}
            //catch (Exception ex)
            //{
            //    //PMLogger.Logger.DebugWriteException("BasicModelTemplate.GetSqliteSelect", ex);
            //}
            return null;
        }
        protected string SqlAdd(T entity)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("INSERT INTO Books");
            queryStringBuilder.AppendLine("");
            queryStringBuilder.AppendLine("SELECT g.*");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            queryStringBuilder.AppendLine("WHERE g.Id = (SELECT MAX(Id) FROM Books");

            return queryStringBuilder.ToString();

            //try
            //{
            //    var stringBuilder = new StringBuilder();
            //    var fields = new StringBuilder("(");
            //    var values = new StringBuilder(" values (");

            //    Type type = this.GetType();
            //    object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);

            //    if (tableAttributes.Length == 1)
            //    {
            //        stringBuilder.Append(String.Format("INSERT INTO {0} ", ((TableAttribute)tableAttributes[0]).Name));

            //        int fieldCount = 0;
            //        foreach (var propertyInfo in type.GetProperties())
            //        {
            //            object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);
            //            if (columnAttributes.Length == 1)
            //            {
            //                var columnAttribute = columnAttributes[0] as ColumnAttribute;
            //                if (columnAttribute != null && columnAttribute.IsPrimaryKey && columnAttribute.IsAutoIncrement)
            //                    continue;
            //                if (fieldCount == 0)
            //                {
            //                    if (columnAttribute != null)
            //                    {
            //                        fields.Append(columnAttribute.Name);
            //                        values.Append("@" + columnAttribute.Name);
            //                    }

            //                }
            //                else
            //                {
            //                    if (columnAttribute != null)
            //                    {
            //                        fields.Append("," + columnAttribute.Name);
            //                        values.Append(",@" + columnAttribute.Name);
            //                    }

            //                }
            //                fieldCount++;
            //            }
            //        }
            //        fields.Append(")");
            //        values.Append(")");
            //        stringBuilder.Append(fields);
            //        stringBuilder.Append(values);
            //    }
            //    return stringBuilder.ToString();
            //}
            //catch (Exception ex)
            //{

            //}

            return null;

        }
        protected string SqlUpdate(T entity)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("");

            return queryStringBuilder.ToString();
            //try
            //{
            //    var stringBuilder = new StringBuilder();
            //    var fieldsSql = new StringBuilder("");
            //    var whereSql = new StringBuilder(" WHERE ");
            //    Type type = this.GetType();
            //    object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            //    if (tableAttributes.Length == 1)
            //    {
            //        stringBuilder.Append(String.Format("UPDATE {0} SET ", ((TableAttribute)tableAttributes[0]).Name));
            //        int fieldCount = 0;
            //        foreach (var propertyInfo in type.GetProperties())
            //        {
            //            object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute),
            //                                                                         true);

            //            if (columnAttributes.Length == 1)
            //            {
            //                var columnAttribute = columnAttributes[0] as ColumnAttribute;
            //                if (columnAttribute != null && columnAttribute.IsPrimaryKey)
            //                {
            //                    whereSql.Append(String.Format("{0}=@{0}", columnAttribute.Name));
            //                }
            //                if (fieldCount == 0)
            //                {
            //                    if (columnAttribute != null && !columnAttribute.IsAutoIncrement)
            //                    {
            //                        fieldsSql.Append(String.Format("{0}=@{0}", columnAttribute.Name));
            //                        fieldCount++;
            //                    }

            //                }
            //                else
            //                {
            //                    if (columnAttribute != null && !columnAttribute.IsAutoIncrement)
            //                    {
            //                        fieldsSql.Append(String.Format(" ,{0}=@{0}", columnAttribute.Name));
            //                        fieldCount++;
            //                    }
            //                }

            //            }
            //        }
            //        stringBuilder.Append(fieldsSql);
            //        stringBuilder.Append(whereSql);
            //    }
            //    return stringBuilder.ToString();
            //}
            //catch (Exception ex)
            //{

            //}
            return null;

        }
        protected string SqlRemove(T entity)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("DELETE");
            queryStringBuilder.AppendLine("FROM Books");
            queryStringBuilder.AppendLine("WHERE Id = @Id");

            return queryStringBuilder.ToString();

            //try
            //{
            //    var stringBuilder = new StringBuilder();
            //    var fieldsSql = new StringBuilder("");
            //    var whereSql = new StringBuilder(" WHERE ");
            //    Type type = this.GetType();
            //    object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            //    if (tableAttributes.Length == 1)
            //    {
            //        stringBuilder.Append(String.Format("DELETE FROM {0} ", ((TableAttribute)tableAttributes[0]).Name));
            //        foreach (var propertyInfo in type.GetProperties())
            //        {
            //            object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);

            //            if (columnAttributes.Length == 1)
            //            {
            //                var columnAttribute = columnAttributes[0] as ColumnAttribute;
            //                if (columnAttribute != null && columnAttribute.IsPrimaryKey)
            //                {
            //                    whereSql.Append(String.Format("{0}=@{0}", columnAttribute.Name));
            //                    break;
            //                }
            //            }
            //        }
            //        stringBuilder.Append(fieldsSql);
            //        stringBuilder.Append(whereSql);
            //    }
            //    return stringBuilder.ToString();
            //}
            //catch (Exception ex)
            //{

            //}

            return null;
        }

        protected PropertyInfo PrimaryKeyPropertyInfo
        {
            get
            {
                //Type type = this.GetType();
                //object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
                //if (tableAttributes.Length == 1)
                //{
                //    foreach (var propertyInfo in type.GetProperties())
                //    {
                //        object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);
                //        if (columnAttributes.Length == 1)
                //        {
                //            var columnAttribute = columnAttributes[0] as ColumnAttribute;
                //            if (columnAttribute != null && columnAttribute.IsPrimaryKey)
                //                return propertyInfo;
                //        }
                //    }
                //}
                return null;
            }
        }
    }
}
