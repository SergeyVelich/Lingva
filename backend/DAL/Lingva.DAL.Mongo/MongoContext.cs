using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Lingva.DAL.Mongo
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly IClientSessionHandle _session;

        public IClientSessionHandle Session { get => _session; }

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
            Type type = typeof(T);
            return type.Name + "s";
        }
    }
}
