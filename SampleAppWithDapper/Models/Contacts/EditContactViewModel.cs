
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleAppWithDapper.Models.Contacts
{
    public class EditContactViewModel
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
        [Display(Name = "Display_Name_TelephoneNumber", ResourceType = typeof(Resource.Resource))]
        public string TelephoneNumber_Entry { get; set; }

        public int Id { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}