using Inflow.Domain.Entities;
using Inflow.ResourceParameters;

namespace Inflow.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<IEnumerable<Product>> FindAllAsync(ProductResourceParameters parameters);
}
