using Inflow.Domain.DTOs.Category;
using Inflow.Domain.DTOs.SaleItem;
using Inflow.Domain.DTOsSaleItem;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Domain.Interfaces.Services
{
    public interface ISaleItemService
    {
        IEnumerable<SaleItemDto> GetAllSaleItems();
        IEnumerable<SaleItemDto> GetSalesSaleItems(int salesId);
        GetBaseResponse<SaleItemDto> GetSaleItems(SaleItemResourceParameters saleItemResourceParameters);
        SaleItemDto? GetSaleItemById(int id);
        SaleItemDto CreateSaleItem(SaleItemForCreateDto saleItemToCreate);
        void UpdateSaleItem(SaleItemForUpdateDto saleItemToUpdate);
        void DeleteSaleItem(int id);
    }
}
