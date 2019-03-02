using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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
    public class CustomerServiceTests
    {
        private Mock<ICustomerRepository> _customerRepository;
        private CustomerService _customerService;

        [TestInitialize]
        public void Initialize()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepository.Object);
        }

        [TestMethod]
        public async Task GetCustomerAsync_CallsRepository()
        {
            _customerRepository.Setup(m => m.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            await _customerService.GetCustomerAsync(1);

            _customerRepository.Verify(m => m.GetCustomerAsync(1), Times.Once);
        }

        [TestMethod]
        public async Task SearchCustomersAsync_CallsRepository()
        {
            _customerRepository.Setup(m => m.SearchCustomersAsync(It.IsAny<CustomerSearchRequest>()))
                .ReturnsAsync(new SearchResponse<Customer>());

            var request = new CustomerSearchRequest();
            await _customerService.SearchCustomersAsync(request);

            _customerRepository.Verify(m => m.SearchCustomersAsync(request), Times.Once);
        }

        [TestMethod]
        public async Task CreateCustomerAsync_CallsRepository()
        {
            await _customerService.CreateCustomerAsync(new Customer());

            _customerRepository.Verify(m => m.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCustomerAsync_CallsRepository()
        {
            await _customerService.UpdateCustomerAsync(new Customer());

            _customerRepository.Verify(m => m.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCustomerAsync_CallsRepository()
        {
            await _customerService.DeleteCustomerAsync(1);

            _customerRepository.Verify(m => m.DeleteCustomerAsync(1), Times.Once);
        }
    }
}
