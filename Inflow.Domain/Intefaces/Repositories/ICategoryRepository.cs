using Inflow.Domain.Entities;
using Inflow.Domain.ResourceParameters;

namespace Inflow.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<IEnumerable<Category>> FindAllAsync(CategoryResourceParameters parameters);
}
