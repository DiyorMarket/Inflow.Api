using Inflow.Domain.Entities;
using Inflow.Domain.Interfaces.Repositories;

namespace Inflow.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(InflowDbContext context) : base(context)
        {
        }
    }
}
