using System.ComponentModel.DataAnnotations;

namespace SampleAppWithDapper.Models.Contacts
{
    public class CreateContactViewModel
    {
        [Display(Name = "Display_Name_Email", ResourceType = typeof(Resource.Resource))]
        [RegularExpression(".+@.+\\..+", ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "Validation_Email_Invalid")]
        // [EmailAddress]
        public string EMail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "Validation_FirstName_Required")]
        [StringLength(255, ErrorMessage = "FirstNameLong", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Display_Name_FirstName", ResourceType = typeof(Resource.Resource))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "Validation_LastName_Required")]
        [StringLength(255, ErrorMessage = "LastNameLong", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Display_Name_LastName", ResourceType = typeof(Resource.Resource))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "Validation_PhoneNumber_Required")]
        [StringLength(14, ErrorMessage = "PhoneNumberLong", MinimumLength = 1)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "Validation_Invalid_Phone_number")]
        [Display(Name = "Display_Name_TelephoneNumber", ResourceType = typeof(Resource.Resource))]
        public string TelephoneNumber_Entry { get; set; }
    }
}