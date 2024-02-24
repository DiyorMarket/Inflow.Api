using AutoMapper;
using Inflow.Domain.DTOs.Sale;
using Inflow.Domain.DTOs.Supply;
using Inflow.Domain.Entities;

namespace Inflow.Domain.Mappings
{
    public class SupplyMappings : Profile
    {
        public SupplyMappings() 
        {
            CreateMap<SupplyDto, Supply>()
                .PreserveReferences();
            CreateMap<Supply, SupplyDto>()
                .ForCtorParam(nameof(SupplyDto.TotalDue), x => x.MapFrom(s => s.SupplyItems.Sum(q => q.Quantity * (decimal)q.UnitPrice)));
            CreateMap<SupplyForCreateDto, Supply>();
            CreateMap<SupplyForUpdateDto, Supply>();
        }
    }
}
