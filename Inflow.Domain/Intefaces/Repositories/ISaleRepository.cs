using Inflow.Domain.Entities;
using Inflow.Domain.ResourceParameters;

namespace Inflow.Domain.Interfaces.Repositories;

public interface ISaleRepository : IRepositoryBase<Sale>
{
    Task<IEnumerable<Sale>> FindAllAsync(SaleResourceParameters parameters);
}
