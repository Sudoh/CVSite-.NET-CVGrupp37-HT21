using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Educations
    {
        public int Id { get; set; }
        public String School { get; set; }
        public String Education { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public String Creator { get; set; }
        public virtual ICollection<CV> CV { get; set; }
    }
}
