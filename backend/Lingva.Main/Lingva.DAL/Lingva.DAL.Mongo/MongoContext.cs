using Lingva.DAL.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DAL.Mongo
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly IClientSessionHandle _session;

        public IClientSessionHandle Session { get => _session; }

        public IMongoCollection<Group> Groups { get => Set<Group>(); }
        public IMongoCollection<Language> Languages { get => Set<Language>(); }
        public IMongoCollection<User> Users { get => Set<User>(); }

        public MongoContext(IConfiguration config)
        {
            string connectionString = config.GetConnectionString("LingvaMongoConnection");
            MongoClient client = new MongoClient(connectionString);
            _session = client.StartSession();
            if (client != null)
            {
                _database = _session.Client.GetDatabase("lingva");
            }               
        }      

        public IMongoCollection<T> Set<T>() where T : class, new()
        {
            string collectionName = GetTableName<T>();
            return _database.GetCollection<T>(collectionName);
        }

        protected string GetTableName<T>()
        {
            string tableName = null;
            var property = typeof(MongoContext).GetProperties().Where(t => t.PropertyType == typeof(IMongoCollection<T>)).FirstOrDefault();

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
            if (await Set<Language>().Find(_ => true).CountDocumentsAsync() == 0)
            {
                Language languageEn = new Language() { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now };
                Language languageRu = new Language() { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now };
                Language[] languages = { languageEn, languageRu };

                await Set<Language>().InsertManyAsync(languages);
            }

            if(await Set<Group>().Find(_ => true).CountDocumentsAsync() == 0)
            {
                Group group1 = new Group { Id = 1, Name = "Harry Potter", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, Language = new Language() { Id = 1, Name = "en" }, Description = "Good movie" };
                Group group2 = new Group { Id = 2, Name = "Librium", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, Language = new Language() { Id = 1, Name = "en" }, Description = "Eq" };
                Group group3 = new Group { Id = 3, Name = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, Language = new Language() { Id = 2, Name = "ru" }, Description = "stuff" };
                Group[] groups = { group1, group2, group3 };

                await Set<Group>().InsertManyAsync(groups);
            }
        }
    }
}
