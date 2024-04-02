using Inflow.Domain.Entities;
using Inflow.Domain.Interfaces.Repositories;
using Inflow.Infrastructure;

namespace Inflow.Infrastructure.Persistence.Repositories
{
    public class SaleItemRepository : RepositoryBase<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(InflowDbContext context)
            : base(context)
        {
        }
    }
}
