using System;
using System.Collections.Generic;

#nullable disable

namespace ORM_Introduction_EF_Core
{
    public partial class PackGrade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
