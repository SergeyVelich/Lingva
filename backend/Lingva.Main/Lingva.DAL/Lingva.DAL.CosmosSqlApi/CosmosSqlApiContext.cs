using Lingva.DAL.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var property = typeof(CosmosSqlApiContext).GetProperties().Where(t => t.PropertyType == typeof(CosmosSqlApiSet<T>)).FirstOrDefault();

            if (property != null)
            {
                tableName = property.Name;
            }
            else
            {
                tableName = typeof(T).Name + "s";
            }

            return tableName;
        }

        public async Task InitializeAsync()
        {
            await Client.CreateDatabaseIfNotExistsAsync(new Database { Id = "Lingva" });

            var properties = typeof(CosmosSqlApiContext).GetProperties().Where(t => t.PropertyType.IsSubclassOf(typeof(CosmosSqlApiSet)));
            foreach (var property in properties)
            {
                await Client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("Lingva"), new DocumentCollection { Id = property.Name });
            }

            Uri uri = UriFactory.CreateDocumentCollectionUri("lingva", "Languages");
            await Client.CreateDocumentAsync(uri, new { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now });
            await Client.CreateDocumentAsync(uri, new { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now });

            uri = UriFactory.CreateDocumentCollectionUri("lingva", "Groups");
            await Client.CreateDocumentAsync(uri, new { Id = 1, Name = "Harry Potter", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Good movie"});
            await Client.CreateDocumentAsync(uri, new { Id = 2, Name = "Librium", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Eq"});
            await Client.CreateDocumentAsync(uri, new { Id = 3, Name = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 2, Description = "stuff"});

            uri = UriFactory.CreateDocumentCollectionUri("lingva", "Users");
            await Client.CreateDocumentAsync(uri, new { Id = 1, Name = "Serhii", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "veloceraptor89@gmail.com" });
            await Client.CreateDocumentAsync(uri, new { Id = 2, Name = "Old", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "tucker_serega@mail.ru" });
        }
    }
}
