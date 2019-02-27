using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerWidget.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWidget.Api.Controllers
{
    [Route("customer")]
    public class CustomerController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Test method. returns a string
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public string DoSomething()
        {
            return _customerService.DoSomething();
        }
    }
}
