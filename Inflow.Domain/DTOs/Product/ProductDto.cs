using Inflow.Domain.DTOs.Category;
using Inflow.Domain.DTOs.SaleItem;

namespace Inflow.Domain.DTOs.Product;

public record ProductDto(
    int Id, 
    string Name, 
    string? Description, 
    decimal Price,
    DateTime ExpireDate, 
    CategoryDto Category, 
    ICollection<SaleItemDto> SaleItems);
