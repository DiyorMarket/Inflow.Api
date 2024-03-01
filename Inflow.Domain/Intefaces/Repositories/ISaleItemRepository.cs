using Inflow.Domain.Entities;

namespace Inflow.Domain.Interfaces.Repositories;

public interface ISaleItemRepository : IRepositoryBase<SaleItem>
{
    Task<IEnumerable<SaleItem>> FindBySaleId(int saleId);
}
