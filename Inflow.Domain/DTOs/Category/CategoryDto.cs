using Inflow.Domain.DTOs.Product;

namespace Inflow.Domain.DTOs.Category;

public record CategoryDto(
    int Id, 
    string Name, 
    string? Description,
    int NumberOfProducts, 
    ICollection<ProductDto> Products);
