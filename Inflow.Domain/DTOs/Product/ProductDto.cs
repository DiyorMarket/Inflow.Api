﻿using Inflow.Domain.DTOs.Category;
using Inflow.Domain.DTOs.SaleItem;
using Inflow.Domain.DTOs.SupplyItem;

namespace Inflow.Domain.DTOs.Product
{
    public record ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal SalePrice { get; init; }
        public decimal SupplyPrice { get; init; }
        public DateTime ExpireDate { get; init; }
        public CategoryDto Category { get; init; }
        public ICollection<SaleItemDto> SaleItems { get; init; }
        public ICollection<SupplyItemDto> SupplyItems { get; init; }
    }
}
