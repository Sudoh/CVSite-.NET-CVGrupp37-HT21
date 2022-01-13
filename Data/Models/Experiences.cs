using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Experiences
    {
        public int Id { get; set; }
        public String WorkPlace { get; set; }
        public String Text { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public String Creator { get; set; }
    }
}
