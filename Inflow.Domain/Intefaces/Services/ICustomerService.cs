using Inflow.Domain.DTOs.Customer;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;

namespace Inflow.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetCustomers();
        GetBaseResponse<CustomerDto> GetCustomers(CustomerResourceParameters customerResourceParameters);
        CustomerDto? GetCustomerById(int id);
        CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate);
        CustomerDto UpdateCustomer(CustomerForUpdateDto customerToUpdate);
        void DeleteCustomer(int id);
    }
}
