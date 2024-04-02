using Inflow.Domain.Entities;
using Inflow.Domain.Interfaces.Repositories;
using Inflow.Infrastructure;

namespace Inflow.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(InflowDbContext context) : base(context)
        {
        }
    }
}
