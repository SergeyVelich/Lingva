using Lingva.DAL.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lingva.DAL.CosmosSqlApi
{
    public class CosmosSqlApiContext : IDisposable
    {
        private const string EndpointUri = "https://testacc.documents.azure.com:443/";
        private const string PrimaryKey = "rKrh4y8caT5wTyrUZKBz6BDj8ZShd9RIuzR7kSlK64RMbn0ofsS5zh817d3RE2qByhnhGKRHLtetyM7CcAshLw==";

        public DocumentClient Client { get; }
        private Dictionary<Type, object> sets;
        protected bool disposed = false;

        public CosmosSqlApiSet<Group> Groups { get => Set<Group>(); }
        public CosmosSqlApiSet<Language> Languages { get => Set<Language>(); }
        public CosmosSqlApiSet<Entities.User> Users { get => Set<Entities.User>(); }

        public CosmosSqlApiContext(IConfiguration config)
        {
            Client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);

            sets = new Dictionary<Type, object>();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Client.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public CosmosSqlApiSet<T> Set<T>() where T : BaseBE, new()
        {
            CosmosSqlApiSet<T> set = null;

            Type type = typeof(T);
            if(sets.TryGetValue(type, out object setObject) && setObject != null)
            {
                if(setObject is CosmosSqlApiSet<T>)
                {
                    set = (CosmosSqlApiSet<T>)setObject;
                }              
            }
            else
            {
                string collectionName = GetTableName<T>();
                set = new CosmosSqlApiSet<T>(this, collectionName);
                sets.Add(type, set);
            }

            return set;
        }

        protected string GetTableName<T>() where T : BaseBE, new()
        {
            string tableName = null;
            var properties = typeof(CosmosSqlApiContext).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(CosmosSqlApiSet<T>))
                {
                    tableName = property.Name;
                    break;
                }
            }

            if (tableName == null)
            {
                tableName = typeof(T).Name + "s";
            }

            return tableName;
        }

        public async Task InitializeAsync()
        {
            await Client.CreateDatabaseIfNotExistsAsync(new Database { Id = "Lingva" });

            //var assembliesToScan = new List<Assembly>(new[] { Assembly.Load("Lingva.DAL") });
            //var allTypes = assembliesToScan.SelectMany(a => a.ExportedTypes).ToArray();
            //var profiles = allTypes
            //    .Where(t => typeof(BaseBE).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
            //    .Where(t => !t.GetTypeInfo().IsAbstract);

            var properties = typeof(CosmosSqlApiContext).GetProperties()
                .Where(t => t.PropertyType == typeof(CosmosSqlApiSet));
            foreach (var property in properties)
            {
                await Client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("Lingva"), new DocumentCollection { Id = property.Name });
            }

            //get all entities

            //var collection = UriFactory.CreateDocumentCollectionUri("Lingva", "Groups");

            //await collection.AddAsync(new { id = todo.Id, todo.CreatedTime, todo.IsCompleted, todo.TaskDescription });

            //Language languageEn = new Language() { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now };
            //Language languageRu = new Language() { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now };
            //Language[] languages = { languageEn, languageRu }; 

            //Set<Language>().InsertManyAsync(languages);

            //Group group1 = new Group { Id = 1, Name = "Harry Potter", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Good movie", Picture = "1" };
            //Group group2 = new Group { Id = 2, Name = "Librium", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Eq", Picture = "2" };
            ////Group group3 = new Group { Id = 3, Name = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 2, Description = "stuff", Picture = "3" };
            //Group group3 = new Group { Id = 3, Name = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, Language = new Language() { Id = 2, Name = "ru" }, Description = "stuff", Picture = "3" };
            //Group[] groups = { group1, group2, group3 };

            //Set<Group>().InsertManyAsync(groups);
        }
    }
}
