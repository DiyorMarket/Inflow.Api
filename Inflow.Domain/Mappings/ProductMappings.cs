using AutoMapper;
using Inflow.Domain.DTOs.Product;
using Inflow.Domain.Entities;

namespace Inflow.Domain.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings() 
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.SupplyPrice, r => r.MapFrom(x => x.Price))
                .ForMember(x => x.SalePrice, r => r.MapFrom(x => x.Price * (decimal)1.5));

            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreateDto, Product>()
                .ForMember(x => x.Price, r => r.MapFrom(x => x.SupplyPrice));
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<Product, ProductForCreateDto>();
            CreateMap<Product, ProductForUpdateDto>();
        }
    }
}
