namespace Inflow.Domain.Entities
{
    public class Sale : EntityBase
    {
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }
}
