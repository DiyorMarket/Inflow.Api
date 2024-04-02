using Inflow.Domain.DTOs.Supplier;
using Inflow.Domain.DTOs.SupplyItem;

namespace Inflow.Domain.DTOs.Supply
{
    public record SupplyDto(
        int Id,
        DateTime SupplyDate,
        decimal TotalDue,
        int SupplierId,
        ICollection<SupplyItemDto> SupplyItems);
}
