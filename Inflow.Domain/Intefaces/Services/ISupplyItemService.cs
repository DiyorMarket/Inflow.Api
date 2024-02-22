using Inflow.Domain.DTOs.SupplyItem;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;

namespace Inflow.Domain.Interfaces.Services
{
    public interface ISupplyItemService
    {
        IEnumerable<SupplyItemDto> GetAllSupplyItems();
        GetBaseResponse<SupplyItemDto> GetSupplyItems(SupplyItemResourceParameters supplyItemResourceParameters);
        SupplyItemDto? GetSupplyItemById(int id);
        SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto supplyItemToCreate);
        void UpdateSupplyItem(SupplyItemForUpdateDto supplyItemToUpdate);
        void DeleteSupplyItem(int id);
    }
}
