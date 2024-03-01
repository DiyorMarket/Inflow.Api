using Inflow.Domain.DTOs.Customer;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;

namespace Inflow.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<PaginatedList<CustomerDto>> GetCustomersAsync(CustomerResourceParameters customerResourceParameters);
    Task<CustomerDto?> GetCustomerByIdAsync(int id);
    Task<CustomerDto> CreateCustomerAsync(CustomerForCreateDto customerToCreate);
    Task<CustomerDto> UpdateCustomerAsync(CustomerForUpdateDto customerToUpdate);
    Task DeleteCustomerAsync(int id);
}
