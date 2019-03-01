using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Common.Configuration;
using CustomerWidget.Models;
using CustomerWidget.Models.Models;
using CustomerWidget.Repository.Interfaces;
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
        }

        public async Task<T> GetCollectionItemAsync<T>(string collection, string key)
            where T : BaseDocument
        {
            var collectionResults = _mongoDb.GetCollection<T>(collection);
            var findResult = await collectionResults.Find(x => x.Id == key)
                .FirstOrDefaultAsync<T>();
            return findResult;
        }
    }
}
