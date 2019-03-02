using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;
using CustomerWidget.Repository.Interfaces;
using MongoDB.Driver;

namespace CustomerWidget.Repository.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoDbContext _context;

        public CustomerRepository(IMongoDbContext context)
        {
            _context = context;
        }

        // todo you never realize how spoiled you are by platform-wide exception-handling filters
        // until you have to write them yourself :/
        public async Task<Customer> GetCustomerAsync(int id)
        {
            var collectionResults = _context.Customers;
            return await collectionResults.Find(x => x.Id == id)
                .FirstOrDefaultAsync<Customer>();
        }

        public async Task<SearchResponse<Customer>> SearchCustomersAsync(CustomerSearchRequest request)
        {
            // As the search requirements expand, add additional logic to generate the filter.
            FilterDefinition<Customer> filter = $"{{ \"agent_id\": {request.AgentId} }}";
            var totalRecords = await _context.Customers.CountDocumentsAsync(filter);
            var findResults = _context.Customers.Find(filter);

            // ReSharper disable once InvertIf
            if (request.SortBy != null)
            {
                var sortDirection = request.IsSortDescending ? "-1" : "1";
                findResults = findResults.Sort($"{{ \"{request.SortBy}\": {sortDirection} }}");
            }

            var results = await findResults.Skip(request.PageSize * request.PageIndex)
                .Limit(request.PageSize)
                .ToListAsync();

            return new SearchResponse<Customer>
            {
                Data = results,
                TotalRecords = totalRecords,
                CurrentPageIndex = request.PageIndex,
                RecordsPerPage = request.PageSize
            };
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.InsertOneAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _context.Customers.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(s => s.Id, customer.Id);

            await _context.Customers.ReplaceOneAsync(filter, customer);
        }
    }
}
