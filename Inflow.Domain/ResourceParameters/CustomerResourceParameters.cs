namespace Inflow.Domain.ResourceParameters
{
    public record CustomerResourceParameters : ResourceParametersBase
    { 
        public override string OrderBy { get; set; } = "firstname";
    }
}
