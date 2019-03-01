using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;
using CustomerWidget.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerWidget.Api.Controllers
{
    [Route("agent")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }
       
        /// <summary>
        /// Gets an agent by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [SwaggerResponse(200, description: "Success", type: typeof(Agent))]
        [SwaggerOperation("get agent")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _agentService.GetAgentAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Search customers.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [SwaggerResponse(200, description: "Success", type: typeof(SearchResponse<Agent>))]
        [SwaggerOperation("search agents")]
        public async Task<IActionResult> SearchAgentsAsync(BaseSearchRequest request)
        {
            var result = await _agentService.SearchAgentsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Create a new agent.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPost("")]
        [SwaggerResponse(200, description: "Success", type: typeof(Agent))]
        [SwaggerOperation("create agent")]
        public async Task<IActionResult> CreateAgentAsync(Agent agent)
        {
            await _agentService.CreateAgentAsync(agent);
            return NoContent();
        }

        /// <summary>
        /// Update an existing agent.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPut("")]
        [SwaggerResponse(204, description: "Success")]
        [SwaggerOperation("update agent")]
        public async Task<IActionResult> UpdateAgentAsync(Agent agent)
        {
            await _agentService.UpdateAgentAsync(agent);
            return NoContent();
        }
    }
}
