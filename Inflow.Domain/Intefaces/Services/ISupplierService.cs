using Inflow.Domain.DTOs.Supplier;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;

namespace Inflow.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDto> GetAllSuppliers();
        GetBaseResponse<SupplierDto> GetSuppliers(SupplierResourceParameters supplierResourceParameters);
        SupplierDto? GetSupplierById(int id);
        SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate);
        SupplierDto UpdateSupplier(SupplierForUpdateDto supplierToUpdate);
        void DeleteSupplier(int id);
    }
}
