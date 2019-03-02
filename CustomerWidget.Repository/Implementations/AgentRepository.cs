using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;
using CustomerWidget.Repository.Interfaces;
using MongoDB.Driver;

namespace CustomerWidget.Repository.Implementations
{
    public class AgentRepository : IAgentRepository
    {
        private readonly IMongoDbContext _context;

        public AgentRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<Agent> GetAgentAsync(int id)
        {
            return (await _context.Agents.FindAsync(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<SearchResponse<Agent>> SearchAgentsAsync(BaseSearchRequest request)
        {
            // As the search requirements expand, add additional logic to generate a filter.
            FilterDefinition<Agent> filter = "{}";
            var totalRecords = await _context.Agents.CountDocumentsAsync(filter);
            var findResults = _context.Agents.Find(filter);

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

        public async Task CreateAgentAsync(Agent agent)
        {
            await _context.Agents.InsertOneAsync(agent);
        }

        public async Task UpdateAgentAsync(Agent agent)
        {
            var filter = Builders<Agent>.Filter.Eq(s => s.Id, agent.Id);

            await _context.Agents.ReplaceOneAsync(filter, agent);
        }
    }
}
