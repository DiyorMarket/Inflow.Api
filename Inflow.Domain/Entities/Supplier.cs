using System.Text.Json.Serialization;

namespace Inflow.Domain.Entities
{
    public class Supplier : EntityBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }

        [JsonIgnore]
        public virtual ICollection<Supply>? Supplies { get; set; }
    }
}
