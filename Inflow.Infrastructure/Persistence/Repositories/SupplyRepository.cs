using Inflow.Domain.Entities;
using Inflow.Domain.Interfaces.Repositories;
using Inflow.Infrastructure;

namespace Inflow.Infrastructure.Persistence.Repositories
{
    public class SupplyRepository : RepositoryBase<Supply>, ISupplyRepository
    {
        public SupplyRepository(InflowDbContext context)
            : base(context)
        {
        }
    }
}
