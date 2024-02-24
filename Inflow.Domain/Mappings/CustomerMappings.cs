﻿using AutoMapper;
using Inflow.Domain.DTOs.Customer;
using Inflow.Domain.Entities;

namespace Inflow.Domain.Mappings
{
    internal class CustomerMappings : Profile
    {
        public CustomerMappings() 
        {
            CreateMap<Customer, CustomerDto>()
                .ForCtorParam(nameof(CustomerDto.FullName),
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<CustomerDto, Customer>();

            CreateMap<CustomerForUpdateDto, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ParseFullName(src.FullName).firstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ParseFullName(src.FullName).lastName));

            CreateMap<CustomerForCreateDto, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => ParseFullName(src.FullName).firstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => ParseFullName(src.FullName).lastName));

        }
        private (string firstName, string lastName) ParseFullName(string fullName)
        {
            var parts = fullName.Split(' ');
            return (parts.Length > 0 ? parts[0] : null, parts.Length > 1 ? parts[1] : null);
        }
    }
}
