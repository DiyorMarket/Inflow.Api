using Inflow.Domain.DTOs.Product;
using Inflow.Domain.Pagniation;
using Inflow.Domain.Responses;
using Inflow.ResourceParameters;

namespace Inflow.Domain.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();
        GetBaseResponse<ProductDto> GetProducts(ProductResourceParameters productResourceParameters);
        ProductDto? GetProductById(int id);
        ProductDto CreateProduct(ProductForCreateDto productToCreate);
        void UpdateProduct(ProductForUpdateDto productToUpdate);
        void DeleteProduct(int id);
    }
}
