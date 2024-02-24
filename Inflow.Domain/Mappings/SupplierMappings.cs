using AutoMapper;
using Inflow.Domain.DTOs.Supplier;
using Inflow.Domain.Entities;

namespace Inflow.Domain.Mappings
{
    public class SupplierMappings : Profile
    {
        public SupplierMappings() 
        {
            CreateMap<SupplierDto, Supplier>()
                .PreserveReferences();
            CreateMap<Supplier, SupplierDto>()
                .PreserveReferences();
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
    }
}
