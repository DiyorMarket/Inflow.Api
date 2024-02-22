using Inflow.Domain.DTOs.Sale;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;

namespace Inflow.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        IEnumerable<SaleDto> GetAllSales();
        IEnumerable<SaleDto> GetCustomersSale(int customersId);
        GetBaseResponse<SaleDto> GetSales(SaleResourceParameters saleResourceParameters);
        SaleDto? GetSaleById(int id);
        SaleDto CreateSale(SaleForCreateDto saleToCreate);
        void UpdateSale(SaleForUpdateDto saleToUpdate);
        void DeleteSale(int id);
    }
}
