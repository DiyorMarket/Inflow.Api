﻿using AutoMapper;
using Inflow.Domain.DTOs.Supplier;
using Inflow.Domain.Entities;
using Inflow.Domain.Interfaces.Services;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;
using Inflow.Infrastructure;

namespace DiyorMarket.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly InflowDbContext _context;

        public SupplierService(IMapper mapper, InflowDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetBaseResponse<SupplierDto> GetSuppliers(SupplierResourceParameters supplierResourceParameters)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(supplierResourceParameters.SearchString))
            {
                query = query.Where(x => x.FirstName.Contains(supplierResourceParameters.SearchString)
                || x.LastName.Contains(supplierResourceParameters.SearchString)
                || x.PhoneNumber.Contains(supplierResourceParameters.SearchString)
                || x.Company.Contains(supplierResourceParameters.SearchString));
            }

            if (!string.IsNullOrEmpty(supplierResourceParameters.OrderBy))
            {
                query = supplierResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "firstname" => query.OrderBy(x => x.FirstName),
                    "firstnamedesc" => query.OrderByDescending(x => x.FirstName),
                    "lastname" => query.OrderBy(x => x.LastName),
                    "lastnamedesc" => query.OrderByDescending(x => x.LastName),
                    "phonenumber" => query.OrderBy(x => x.PhoneNumber),
                    "phonenumberdesc" => query.OrderByDescending(x => x.PhoneNumber),
                    "company" => query.OrderBy(x => x.Company),
                    "companydesc" => query.OrderByDescending(x => x.Company),
                    _ => query.OrderBy(x => x.FirstName),
                };
            }

            var suppliers = query.ToPaginatedList(supplierResourceParameters.PageSize, supplierResourceParameters.PageNumber);

            var supplierDtos = _mapper.Map<List<SupplierDto>>(suppliers);

            var paginatedResult = new PaginatedList<SupplierDto>(supplierDtos, suppliers.TotalCount, suppliers.CurrentPage, suppliers.PageSize);

            return paginatedResult.ToResponse();
        }

        public IEnumerable<SupplierDto> GetAllSuppliers()
        {
            var suppliers = _context.Suppliers.ToList();

            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers) ?? Enumerable.Empty<SupplierDto>();
        }

        public SupplierDto? GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);

            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            return supplierDto;
        }

        public SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierToCreate);

            _context.Suppliers.Add(supplierEntity);
            _context.SaveChanges();

            var supplierDto = _mapper.Map<SupplierDto>(supplierEntity);

            return supplierDto;
        }

        public SupplierDto UpdateSupplier(SupplierForUpdateDto supplierToUpdate)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierToUpdate);

            _context.Suppliers.Update(supplierEntity);
            _context.SaveChanges();

            var supplierDto = _mapper.Map<SupplierDto>(supplierEntity);

            return supplierDto;
        }

        public void DeleteSupplier(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
            if (supplier is not null)
            {
                _context.Suppliers.Remove(supplier);
            }
            _context.SaveChanges();
        }
    }
}
