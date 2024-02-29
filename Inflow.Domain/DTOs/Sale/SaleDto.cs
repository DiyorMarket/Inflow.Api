using Inflow.Domain.DTOs.Customer;
using Inflow.Domain.DTOs.SaleItem;

namespace Inflow.Domain.DTOs.Sale;

public record SaleDto(
    int Id,
    DateTime SaleDate,
    decimal TotalDue,
    CustomerDto Customer,
    ICollection<SaleItemDto> SaleItems);


