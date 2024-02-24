using Inflow.Domain.DTOs.SaleItem;

namespace Inflow.Domain.DTOs.Sale
{
    public record SaleDto(
        int Id,
        DateTime SaleDate,
        int CustomerId,
        decimal TotalDue,
        ICollection<SaleItemDto> SaleItems);

 }
