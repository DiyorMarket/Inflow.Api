using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Domain.ResourceParameters
{
    public class SupplyItemResourceParameters : ResourceParametersBase
    {
        public override string OrderBy { get; set; } = "id";
    }
}
