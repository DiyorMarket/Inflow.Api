using Inflow.Domain.DTOs.Sale;
using Inflow.Domain.Entities;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;

namespace Inflow.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<PaginatedList<Sale>> GetSalesAsync(SaleResourceParameters parameters);
    Task<PaginatedList<Sale>> GetSalesByCustomerIdAsync(int customerId);
    SaleDto? GetSaleById(int id);
    SaleDto CreateSale(SaleForCreateDto saleToCreate);
    void UpdateSale(SaleForUpdateDto saleToUpdate);
    void DeleteSale(int id);
}
