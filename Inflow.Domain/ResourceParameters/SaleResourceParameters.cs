namespace Inflow.Domain.ResourceParameters
{
    public record SaleResourceParameters : ResourceParametersBase
    {
        public int? CustomerId { get; set; }
        public DateTime? SaleDate { get; set; }
        public override string OrderBy { get; set; } = "id";
    }
}
