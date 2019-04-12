﻿using Dapper;
using Lingva.DAL.Entities;
using System;
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
        private string _tableName;

        public string TableName
        {
            get
            {
                if(_tableName == null)
                {
                    _tableName = GetTableNameByType();
                }
                return _tableName;
            }
        }

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
            IEnumerable<string> fields = GetFields(entity);
            T result = await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(SqlAdd(fields), entity, transaction: transaction);
            return result;        
        }
        public async Task<T> UpdateAsync(T entity, IDbTransaction transaction = null)
        {
            IEnumerable<string> fields = GetFields(entity);
            T result = await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(SqlUpdate(fields), entity, transaction: transaction);
            return result;
        }
        public async Task RemoveAsync(T entity, IDbTransaction transaction = null)
        {
            await _dbContext.Connection.ExecuteAsync(SqlRemove(), new { entity.Id }, transaction: transaction);
        }

        protected string SqlSelectAll()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.*");
            queryStringBuilder.Append("FROM " + TableName + " AS g");

            return queryStringBuilder.ToString();
        }
        protected string SqlFind()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.*");
            queryStringBuilder.AppendLine("FROM " + TableName + " AS g");
            queryStringBuilder.Append("WHERE g.Id = @Id");

            return queryStringBuilder.ToString(); ;
        }    
        protected string SqlAdd(IEnumerable<string> fields)
        {
            StringBuilder fieldsStringBuilder = new StringBuilder();
            StringBuilder valuesStringBuilder = new StringBuilder();
            bool isFirst = true;
            foreach(string field in fields)
            {
                if (isFirst != true)
                {
                    fieldsStringBuilder.Append(", ");
                    valuesStringBuilder.Append(", ");
                }

                fieldsStringBuilder.Append(field);
                valuesStringBuilder.Append("@" + field);
                isFirst = false;
            }

            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("INSERT INTO " + TableName + "(" + fieldsStringBuilder.ToString() + ")");
            queryStringBuilder.AppendLine("VALUES(" + valuesStringBuilder.ToString() + ")");
            queryStringBuilder.AppendLine("SELECT g.*");
            queryStringBuilder.AppendLine("FROM " + TableName + " AS g");
            queryStringBuilder.Append("WHERE g.Id = (SELECT MAX(Id) FROM " + TableName + ")");

            return queryStringBuilder.ToString();
        }
        protected string SqlUpdate(IEnumerable<string> fields)
        {
            StringBuilder conditionsStringBuilder = new StringBuilder();
            bool isFirst = true;
            foreach (string field in fields)
            {
                if (isFirst != true)
                {
                    conditionsStringBuilder.Append(", ");
                }

                conditionsStringBuilder.Append(field + "=@" + field);
                isFirst = false;
            }

            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("UPDATE " + TableName);
            queryStringBuilder.AppendLine("SET " + conditionsStringBuilder.ToString());
            queryStringBuilder.Append("WHERE Id = @Id");

            return queryStringBuilder.ToString();
        }
        protected string SqlRemove()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("DELETE");
            queryStringBuilder.AppendLine("FROM " + TableName);
            queryStringBuilder.Append("WHERE Id = @Id");

            return queryStringBuilder.ToString();
        }

        protected IEnumerable<string> GetFields(T entity)
        {
            var properties = entity.GetType().GetProperties(
              BindingFlags.Instance
              | BindingFlags.NonPublic
              | BindingFlags.Public);

            List<string> propertiesList = new List<string>();

            foreach(var property in properties)
            {
                if(property.Name != "Id" && 
                    property.PropertyType.IsPrimitive 
                    || property.PropertyType == typeof(String)
                    || property.PropertyType == typeof(DateTime)
                    || property.PropertyType == typeof(Decimal))
                {
                    propertiesList.Add(property.Name);
                }                
            }

            return propertiesList;
        }

        protected string GetTableNameByType()
        {
            Type type = typeof(T);
            return type.Name + "s";
        }
    }
}