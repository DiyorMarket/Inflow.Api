using Inflow.Domain.Entities;
using Inflow.Domain.Interfaces.Repositories;
using Inflow.Infrastructure;

namespace Inflow.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(InflowDbContext context)
            : base(context)
        {
        }
    }
}
