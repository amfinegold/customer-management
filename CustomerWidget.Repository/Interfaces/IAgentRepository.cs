using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;

namespace CustomerWidget.Repository.Interfaces
{
    public interface IAgentRepository
    {
        Task<Agent> GetAgentAsync(int id);

        Task<SearchResponse<Agent>> SearchAgentsAsync(BaseSearchRequest request);
        Task<Agent> CreateAgentAsync(Agent agent);
        Task UpdateAgentAsync(Agent agent);

    }
}
