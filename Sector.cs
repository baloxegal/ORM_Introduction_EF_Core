using System;
using System.Collections.Generic;

#nullable disable

namespace ORM_Introduction_EF_Core
{
    public partial class Sector
    {
        public Sector()
        {
            Packages = new HashSet<Package>();
        }

        public int SectorId { get; set; }
        public string SectorName { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
    }
}
