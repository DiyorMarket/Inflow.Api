using Inflow.Domain.DTOs.Sale;

namespace Inflow.Domain.DTOs.Customer
{
    public record CustomerDto(
        int Id,
        string FullName,
        string PhoneNumber,
        ICollection<SaleDto> Sales);
}
