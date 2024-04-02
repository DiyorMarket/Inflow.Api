namespace Inflow.Domain.DTOs.Product
{
    public record ProductForUpdateDto(
        int Id,
        string Name,
        string Description,
        decimal SalePrice,
        decimal SupplyPrice,
        DateTime ExpireDate,
        int CategoryId);
}
