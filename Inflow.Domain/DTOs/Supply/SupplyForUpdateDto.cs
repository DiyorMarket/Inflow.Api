﻿namespace Inflow.Domain.DTOs.Supply
{
    public record SupplyForUpdateDto(
        int Id,
        DateTime SupplyDate,
        int SupplierId);
}
