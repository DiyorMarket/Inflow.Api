using Inflow.Domain.Entities;
using Inflow.Domain.Interfaces.Repositories;
using Inflow.Infrastructure;

namespace Inflow.Infrastructure.Persistence.Repositories
{
    public class SupplyItemRepository : RepositoryBase<SupplyItem>, ISupplyItemRepository
    {
        public SupplyItemRepository(InflowDbContext context)
            : base(context)
        {
        }
    }
}
