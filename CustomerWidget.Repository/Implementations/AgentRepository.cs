using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Common;
using CustomerWidget.Common.Configuration;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;
using CustomerWidget.Repository.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CustomerWidget.Repository.Implementations
{
    public class AgentRepository : IAgentRepository
    {
        private const string Collection = "agent";
        private readonly IMongoDatabase _mongoDb;

        public AgentRepository(DatabaseConfig config)
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            var settings = MongoClientSettings
                .FromUrl(new MongoUrl(config.ConnectionString));
            settings.SslSettings =
                new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);
            _mongoDb = mongoClient.GetDatabase(Constant.DatabaseName);

            // Pretty sure this isn't the right place to register this map
            // unfortunately I haven't the foggiest what is
            if (!BsonClassMap.IsClassMapRegistered(typeof(Agent)))
            {
                BsonClassMap.RegisterClassMap<Agent>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<Agent> GetAgentAsync(int id)
        {
            return (await _mongoDb.GetCollection<Agent>(Collection)
                .FindAsync(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<SearchResponse<Agent>> SearchAgentsAsync(BaseSearchRequest request)
        {
            // As the search requirements expand, add additional logic to generate a filter.
            FilterDefinition<Agent> filter = "{}";

            var collectionResults = _mongoDb.GetCollection<Agent>(Collection);
            var totalRecords = await collectionResults.CountDocumentsAsync(filter);
            var findResults = collectionResults.Find(filter);

            // ReSharper disable once InvertIf
            if (request.SortBy != null)
            {
                var sortDirection = request.IsSortDescending ? "-1" : "1";
                findResults = findResults.Sort($"{{ \"{request.SortBy}\": {sortDirection} }}");
            }

            var results = await findResults.Skip(request.PageSize * request.PageIndex)
                .Limit(request.PageSize)
                .ToListAsync();

            return new SearchResponse<Agent>
            {
                Data = results,
                TotalRecords = totalRecords,
                CurrentPageIndex = request.PageIndex,
                RecordsPerPage = request.PageSize
            };
        }

        public async Task<Agent> CreateAgentAsync(Agent agent)
        {
            var collectionResults = _mongoDb.GetCollection<Agent>(Collection);
            await collectionResults.InsertOneAsync(agent);
            return agent;
        }

        public async Task UpdateAgentAsync(Agent agent)
        {
            var collectionResults = _mongoDb.GetCollection<Agent>(Collection);
            var filter = Builders<Agent>.Filter.Eq(s => s.Id, agent.Id);
            await collectionResults.ReplaceOneAsync(filter, agent);
        }
    }
}
