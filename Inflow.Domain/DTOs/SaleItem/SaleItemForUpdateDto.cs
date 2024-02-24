namespace Inflow.Domain.DTOsSaleItem
{
    public record SaleItemForUpdateDto(
        int Id,
        int Quantity,
        decimal UnitPrice,
        int ProductId,
        int SaleId);
}
