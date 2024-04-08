using Inflow.Domain.ResourceParameters;

namespace Inflow.ResourceParameters
{
    public record ProductResourceParameters : ResourceParametersBase
    {
        public override string OrderBy { get; set; } = "name";
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceLessThan { get; set; }
        public decimal? PriceGreaterThan { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}