namespace Inflow.Domain.DTOs.Product
{
    public record ProductForUpdateDto(
        int Id,
        string Name,
        string Description,
        decimal Price,
        DateTime ExpireDate,
        int CategoryId);
}
