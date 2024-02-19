using System.Text.Json.Serialization;

namespace Inflow.Domain.Entities
{
    public class Supply : EntityBase
    {
        public DateTime SupplyDate { get; set; }

        public int SupplierId { get; set; }
        [JsonIgnore]
        public Supplier? Supplier { get; set; }

        public virtual ICollection<SupplyItem>? SupplyItems { get; set; }
    }
}
