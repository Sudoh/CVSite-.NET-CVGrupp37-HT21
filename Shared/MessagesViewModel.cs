using System;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class CreateMessagesViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Namn")]
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }

        //public List<ApplicationUser> Users { get; set; }

    }

    public class ListMyMessagesViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string MessageText { get; set; }

        public bool IsRead { get; set; }
        public DateTime dateSent { get; set; }

    }
}
