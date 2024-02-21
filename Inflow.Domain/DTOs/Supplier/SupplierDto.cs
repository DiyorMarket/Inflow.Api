using Inflow.Domain.DTOs.Supply;

namespace Inflow.Domain.DTOs.Supplier
{
    public record SupplierDto(
        int Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Company,
        ICollection<SupplyDto> Supplies);
}
