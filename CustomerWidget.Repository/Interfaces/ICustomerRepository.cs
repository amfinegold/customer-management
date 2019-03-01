using System.Threading.Tasks;
using CustomerWidget.Models.Models;
using CustomerWidget.Models.Requests;
using CustomerWidget.Models.Responses;

namespace CustomerWidget.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(int id);
        Task<SearchResponse<Customer>> SearchCustomersAsync(CustomerSearchRequest request);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task UpdateCustomerAsync(Customer customer);
    }
}
