using System;
using System.Collections.Generic;

#nullable disable

namespace ORM_Introduction_EF_Core
{
    public partial class Package
    {
        public Package()
        {
            Customers = new HashSet<Customer>();
        }

        public int PackId { get; set; }
        public string Speed { get; set; }
        public DateTime? StrtDate { get; set; }
        public int? MonthlyPayment { get; set; }
        public int? SectorId { get; set; }

        public virtual Sector Sector { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
