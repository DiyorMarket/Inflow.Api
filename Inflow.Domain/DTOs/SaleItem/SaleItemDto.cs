using Inflow.Domain.DTOs.Product;
using Inflow.Domain.DTOs.Sale;

namespace Inflow.Domain.DTOs.SaleItem;

public record SaleItemDto(
    int Id,
    int Quantity,
    decimal UnitPrice,
    ProductDto Product,
    SaleDto Sale);
