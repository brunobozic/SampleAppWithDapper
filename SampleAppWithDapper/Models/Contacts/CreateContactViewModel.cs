using System.ComponentModel.DataAnnotations;

namespace SampleAppWithDapper.Models.Contacts
{
    public class CreateContactViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        public string EMail { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(14, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        [Display(Name = "Telephone number")]
        public string TelephoneNumber_Entry { get; set; }
    }
}