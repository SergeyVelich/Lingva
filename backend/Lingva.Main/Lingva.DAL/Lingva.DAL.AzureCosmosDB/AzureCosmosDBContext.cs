using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Lingva.DAL.AzureCosmosDB
{
    public class AzureCosmosDBContext
    {
        private const string EndpointUri = "https://testacc.documents.azure.com:443/";
        private const string PrimaryKey = "rKrh4y8caT5wTyrUZKBz6BDj8ZShd9RIuzR7kSlK64RMbn0ofsS5zh817d3RE2qByhnhGKRHLtetyM7CcAshLw==";

        private readonly DocumentClient _client;

        public AzureCosmosDBContext(IConfiguration config)
        {
            _client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);

        //string connectionString = config.GetConnectionString("LingvaMongoConnection");
        //    MongoClient client = new MongoClient(connectionString);
        //    _session = client.StartSession();
        //    if (client != null)
        //    {
        //        _database = _session.Client.GetDatabase("lingva");
        //    }               
        }      

        //public IMongoCollection<T> Set<T>() where T : class, new()
        //{
        //    string collectionName = GetTableName<T>();
        //    return _database.GetCollection<T>(collectionName);
        //}

        //protected string GetTableName<T>()
        //{
        //    Type type = typeof(T);
        //    return type.Name + "s";
        //}

        public async Task InitializeAsync()
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = "Lingva" });
            var t = await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("Lingva"), new DocumentCollection { Id = "Groups" });
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
