using Inflow.Domain.Entities;
using Inflow.Domain.ResourceParameters;

namespace Inflow.Domain.Interfaces.Repositories;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    Task<IEnumerable<Customer>> FindAllAsync(CustomerResourceParameters parameters);
}
