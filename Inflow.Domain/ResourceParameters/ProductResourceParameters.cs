using Inflow.Domain.ResourceParameters;

namespace Inflow.ResourceParameters
{
    public class ProductResourceParameters : ResourceParametersBase
    {
        public override string OrderBy { get; set; } = "name";
    }
}