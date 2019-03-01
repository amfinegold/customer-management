using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using CustomerWidget.Common.Configuration;
using CustomerWidget.Models.Models;
using CustomerWidget.Repository.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CustomerWidget.Repository.Implementations
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _mongoDb;
        private readonly DatabaseConfig _config;

        public MongoDbContext(DatabaseConfig config)
        {
            _config = config;

            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            var settings = MongoClientSettings
                .FromUrl(new MongoUrl(config.ConnectionString));
            settings.SslSettings =
                new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);
            _mongoDb = mongoClient.GetDatabase(config.DatabaseName);

            RegisterClassMaps();
        }

        public IMongoCollection<Agent> Agents =>
            _mongoDb.GetCollection<Agent>(_config.AgentCollectionName);

        public IMongoCollection<Customer> Customers =>
            _mongoDb.GetCollection<Customer>(_config.CustomerCollectionName);

        private static void RegisterClassMaps()
        {
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
    }
}
