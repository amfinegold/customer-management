using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Repository.Interfaces;

namespace CustomerWidget.Repository.Implementations
{
    public class AgentRepository : IAgentRepository
    {
        private readonly IDataSource _dataSource;
        private const string CustomerCollection = "agent";

        public AgentRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<Agent> GetAgentAsync(int id)
        {
            return await _dataSource.GetCollectionItemAsync<Agent>(CustomerCollection, id);
        }

        public async Task<Agent> CreateAgentAsync(Agent agent)
        {
            await _dataSource.AddCollectionItemAsync(CustomerCollection, agent);
            return agent;
        }

        public async Task<Agent> UpdateAgentAsync(Agent agent)
        {
            await _dataSource.UpdateCollectionItemAsync(CustomerCollection, agent);
            return agent;
        }
    }
}
