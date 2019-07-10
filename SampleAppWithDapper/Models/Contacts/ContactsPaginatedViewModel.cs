using System;
using System.ComponentModel.DataAnnotations;

namespace SampleAppWithDapper.Models.Contacts
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string EMail { get; set; }

        public int FilteredCount { get; set; }
        public int TotalCount { get; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telephone number")]
        public string TelephoneNumber_Entry { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Date created")]
        public DateTimeOffset? CreatedUtc { get; set; }
    }
}