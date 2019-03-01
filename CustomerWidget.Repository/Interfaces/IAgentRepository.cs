using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Models.Models;

namespace CustomerWidget.Repository.Interfaces
{
    public interface IAgentRepository
    {
        Task<Agent> GetAgentAsync(int id);
        Task<Agent> CreateAgentAsync(Agent agent);
        Task<Agent> UpdateAgentAsync(int id, Agent agent);

    }
}
