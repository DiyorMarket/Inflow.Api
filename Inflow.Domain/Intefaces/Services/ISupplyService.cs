using Inflow.Domain.DTOs.Category;
using Inflow.Domain.DTOs.Supply;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;

namespace Inflow.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        IEnumerable<SupplyDto> GetAllSupplies();
        GetBaseResponse<SupplyDto> GetSupplies(SupplyResourceParameters supplyResourceParameters);
        SupplyDto? GetSupplyById(int id);
        SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate);
        void UpdateSupply(SupplyForUpdateDto supplyToUpdate);
        void DeleteSupply(int id);
    }
}
