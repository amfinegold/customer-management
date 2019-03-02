using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;
using CustomerWidget.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerWidget.Api.Controllers
{
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Gets a customer by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [SwaggerResponse(200, description: "Success", type: typeof(Customer))]
        [SwaggerOperation("get customer")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _customerService.GetCustomerAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Search customers.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [SwaggerResponse(200, description: "Success", type: typeof(SearchResponse<Customer>))]
        [SwaggerOperation("search customers")]
        public async Task<IActionResult> SearchCustomersAsync(CustomerSearchRequest request)
        {
            var result = await _customerService.SearchCustomersAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Create a new customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("")]
        [SwaggerResponse(204, description: "Success", type: typeof(Customer))]
        [SwaggerOperation("create customer")]
        public async Task<IActionResult> CreateCustomerAsync(Customer customer)
        {
            await _customerService.CreateCustomerAsync(customer);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing customer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("")]
        [SwaggerResponse(204, description: "Success")]
        [SwaggerOperation("delete customer")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Update an existing customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("")]
        [SwaggerResponse(204, description: "Success")]
        [SwaggerOperation("update customer")]
        public async Task<IActionResult> UpdateCustomerAsync(Customer customer)
        {
            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }


    }
}
