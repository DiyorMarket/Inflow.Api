using Inflow.Domain.DTOs.SaleItem;

namespace Inflow.Domain.DTOs.Sale;

public record SaleForCreateDto(
    DateTime SaleDate,
    int CustomerId,
    ICollection<SaleItemForCreateDto> SaleItems);
