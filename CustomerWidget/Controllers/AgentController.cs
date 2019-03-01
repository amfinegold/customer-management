using System.Collections.Generic;
using CustomerWidget.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWidget.Api.Controllers
{
    [Route("agent")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Agent agent)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Agent agent)
        {
        }
    }
}
