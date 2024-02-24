using AutoMapper;
using Inflow.Domain.DTOs.SupplyItem;
using Inflow.Domain.Entities;

namespace Inflow.Domain.Mappings
{
    public class SupplyItemMappings : Profile
    {
        public SupplyItemMappings() 
        {
            CreateMap<SupplyItemDto, SupplyItem>()
                .PreserveReferences();
            CreateMap<SupplyItem, SupplyItemDto>()
                .PreserveReferences();
            CreateMap<SupplyItemForCreateDto, SupplyItem>();
            CreateMap<SupplyItemForUpdateDto, SupplyItem>();
        }
    }
}
