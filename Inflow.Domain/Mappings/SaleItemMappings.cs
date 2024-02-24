using AutoMapper;
using Inflow.Domain.DTOs.SaleItem;
using Inflow.Domain.DTOsSaleItem;
using Inflow.Domain.Entities;

namespace Inflow.Domain.Mappings
{
    public class SaleItemMappings : Profile
    {
        public SaleItemMappings() 
        {
            CreateMap<SaleItemDto, SaleItem>();
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<SaleItemForCreateDto, SaleItem>();
            CreateMap<SaleItemForUpdateDto, SaleItem>();
        }
    }
}
