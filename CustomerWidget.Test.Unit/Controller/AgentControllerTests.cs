////TODO: VERSION CONFLICT WITH THE ENTIRE API PROJECT :/

//using System.Diagnostics.CodeAnalysis;
//using System.Threading.Tasks;
//using CustomerWidget.Api.Controllers;
//using CustomerWidget.Models.Models;
//using CustomerWidget.Service.Interfaces;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace CustomerWidget.Test.Unit.Controller
//{
//    [TestClass, ExcludeFromCodeCoverage]
//    public class AgentControllerTests
//    {
//        private Mock<IAgentService> _agentService;
//        private AgentController _agentController;

//        [TestInitialize]
//        public void Initialize()
//        {
//            _agentService = new Mock<IAgentService>();
//            _agentController = new AgentController(_agentService.Object);
//        }

//        [TestMethod]
//        public async Task Get_CallsService()
//        {
//            _agentService.Setup(m => m.GetAgentAsync(It.IsAny<int>())).ReturnsAsync(new Agent());

//            await _agentController.Get(1);

//            _agentService.Verify(m => m.GetAgentAsync(1), Times.Once);
//        }
//    }
//}
