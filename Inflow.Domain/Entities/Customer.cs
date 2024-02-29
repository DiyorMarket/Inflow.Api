﻿namespace Inflow.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
