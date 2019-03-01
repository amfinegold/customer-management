using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CustomerRepository : ICustomerRepository
    {
        private const string Collection = "customer";
        private readonly IMongoDatabase _mongoDb;

        public CustomerRepository(DatabaseConfig config)
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            var settings = MongoClientSettings
                .FromUrl(new MongoUrl(config.ConnectionString));
            settings.SslSettings =
                new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);
            _mongoDb = mongoClient.GetDatabase(Constant.DatabaseName);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Customer)))
            {
                BsonClassMap.RegisterClassMap<Customer>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                });
            }

        }

        // todo you never realize how spoiled you are by platform-wide exception-handling filters
        // until you have to write them yourself :/
        public async Task<Customer> GetCustomerAsync(int id)
        {
            var collectionResults = _mongoDb.GetCollection<Customer>(Collection);
            return await collectionResults.Find(x => x.Id == id)
                .FirstOrDefaultAsync<Customer>();
        }

        public async Task<SearchResponse<Customer>> SearchCustomersAsync(CustomerSearchRequest request)
        {
            // As the search requirements expand, add additional logic to generate the filter.
            FilterDefinition<Customer> filter = $"{{ \"agent_id\": {request.AgentId} }}";

            var collectionResults = _mongoDb.GetCollection<Customer>(Collection);
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
            var totalRecords = await collectionResults.CountDocumentsAsync(filter);

            return new SearchResponse<Customer>
            {
                Data = results,
                TotalRecords = totalRecords,
                CurrentPageIndex = request.PageIndex,
                RecordsPerPage = request.PageSize
            };
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var collectionResults = _mongoDb.GetCollection<Customer>(Collection);
            await collectionResults.InsertOneAsync(customer);
            return customer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _mongoDb.GetCollection<Customer>(Collection)
                .DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var collectionResults = _mongoDb.GetCollection<Customer>(Collection);
            var filter = Builders<Customer>.Filter.Eq(s => s.Id, customer.Id);
            await collectionResults.ReplaceOneAsync(filter, customer);
        }
    }
}
