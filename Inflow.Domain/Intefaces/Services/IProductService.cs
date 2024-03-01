using Inflow.Domain.DTOs.Product;
using Inflow.Domain.Pagniation;
using Inflow.ResourceParameters;

namespace Inflow.Domain.Interfaces.Services;

public interface IProductService
{
    Task<PaginatedList<ProductDto>> GetProductsAsync(ProductResourceParameters productResourceParameters);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(ProductForCreateDto productToCreate);
    Task<ProductDto> UpdateProductAsync(ProductForUpdateDto productToUpdate);
    Task DeleteProduct(int id);
}
