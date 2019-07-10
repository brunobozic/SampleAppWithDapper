

using SampleAppWithDapper.DataAccess.Repositories.Contact;

namespace SampleAppWithDapper.Models.Contacts.Extensions
{
    public static class ContactExtensions
    {
        public static EditContactViewModel ConvertToViewModel(this ContactDto contact)
        {
            var returnVM = new EditContactViewModel
            {
                EMail = contact.EMail ?? "N/A",
                FirstName = contact.FirstName ?? "N/A",
                LastName = contact.LastName ?? "N/A",
                TelephoneNumber_Entry = contact.TelephoneNumber_Entry ?? "N/A"
            };

            return returnVM;

        }
    }
}