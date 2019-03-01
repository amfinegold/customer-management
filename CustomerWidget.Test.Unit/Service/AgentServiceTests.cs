using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;
using CustomerWidget.Repository.Interfaces;
using CustomerWidget.Service.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CustomerWidget.Test.Unit.Service
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AgentServiceTests
    {
        private Mock<IAgentRepository> _agentRepository;
        private AgentService _agentService;

        [TestInitialize]
        public void Initialize()
        {
            _agentRepository = new Mock<IAgentRepository>();
            _agentService = new AgentService(_agentRepository.Object);
        }

        [TestMethod]
        public async Task GetAgentAsync_CallsRepository()
        {
            _agentRepository.Setup(m => m.GetAgentAsync(It.IsAny<int>())).ReturnsAsync(new Agent());
            await _agentService.GetAgentAsync(1);

            _agentRepository.Verify(m => m.GetAgentAsync(1), Times.Once);
        }

        [TestMethod]
        public async Task SearchAgentsAsync_CallsRepository()
        {
            _agentRepository.Setup(m => m.SearchAgentsAsync(It.IsAny<BaseSearchRequest>()))
                .ReturnsAsync(new SearchResponse<Agent>());

            var request = new BaseSearchRequest();
            await _agentService.SearchAgentsAsync(request);

            _agentRepository.Verify(m => m.SearchAgentsAsync(request), Times.Once);
        }

        [TestMethod]
        public async Task CreateAgentAsync_CallsRepository()
        {
            await _agentService.CreateAgentAsync(new Agent());

            _agentRepository.Verify(m => m.CreateAgentAsync(It.IsAny<Agent>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAgentAsync_CallsRepository()
        {
            await _agentService.UpdateAgentAsync(new Agent());

            _agentRepository.Verify(m => m.UpdateAgentAsync(It.IsAny<Agent>()), Times.Once);
        }
    }
}
