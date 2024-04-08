namespace Inflow.Domain.ResourceParameters
{
    public record SupplierResourceParameters : ResourceParametersBase
    {
        public override string OrderBy { get; set; } = "firstname";
    }
}
