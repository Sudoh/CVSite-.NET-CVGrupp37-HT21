using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Messages
    {

        public int Id { get; set; }

        public string FromUser { get; set; }

        public string MessageText { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey(nameof(ToUser))]
        public string ToUserID { get; set; }

        public virtual ApplicationUser ToUser { get; set; }

    }
}