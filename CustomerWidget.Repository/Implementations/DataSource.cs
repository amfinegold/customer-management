using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Common.Configuration;
using CustomerWidget.Models;
using CustomerWidget.Models.Models;
using CustomerWidget.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CustomerWidget.Repository.Implementations
{
    public class DataSource : IDataSource
    {
        private const string DatabaseName = "customerManagementDB";
        private readonly IMongoDatabase _mongoDb;

        public DataSource(DatabaseConfig config)
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            var settings = MongoClientSettings
                .FromUrl(new MongoUrl(config.ConnectionString));
            settings.SslSettings =
                new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);
            _mongoDb = mongoClient.GetDatabase(DatabaseName);

            // Registered as a singleton to do the mapping here
            // Check if the map is registered here because BsonClassMap.RegisterClassMap<T>
            // is a static method affecting a singleton in a singleton and can't be tested
            // The additional startup cost is to be able to test DataSource.
            // Open to better ideas for how to handle this effectively.

            if (!BsonClassMap.IsClassMapRegistered(typeof(Agent)))
            {
                BsonClassMap.RegisterClassMap<Agent>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Customer)))
            {
                BsonClassMap.RegisterClassMap<Customer>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<T>> GetCollectionAsync<T>(string collection)
            where T : BaseDocument
        {
            var collectionResults = _mongoDb.GetCollection<T>(collection);
            var findResults = await collectionResults.Find(new BsonDocument())
                .ToListAsync<T>();
            return findResults;
        }

        public async Task<T> GetCollectionItemAsync<T>(string collection, int key)
            where T : BaseDocument
        {
            var collectionResults = _mongoDb.GetCollection<T>(collection);
            var findResult = await collectionResults.Find(x => x.Id == key)
                .FirstOrDefaultAsync<T>();
            return findResult;
        }

        public async Task<T> AddCollectionItemAsync<T>(string collection, T item)
            where T : BaseDocument
        {
            var collectionResults = _mongoDb.GetCollection<T>(collection);
            await collectionResults.InsertOneAsync(item);
            return item;
        }

        public async Task UpdateCollectionItemAsync<T>(string collection, T item)
            where T : BaseDocument
        {
            var collectionResults = _mongoDb.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq(s => s.Id, item.Id);
            await collectionResults.ReplaceOneAsync(filter, item);
        }

        public async Task DeleteCollectionItemAsync<T>(string collection, int id)
            where T : BaseDocument
        {
            var collectionResults = _mongoDb.GetCollection<T>(collection);

          

            await collectionResults.DeleteOneAsync<T>(x => x.Id == id);
        }
    }
}
