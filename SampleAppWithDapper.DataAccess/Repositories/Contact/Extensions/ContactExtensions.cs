

using System.Collections.Generic;
using System.Linq;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact.Extensions
{
    public static class ContactExtensions
    {
        public static ContactDto ConvertToDto(this Domain.DomainModels.Contact.Contact contact)
        {
            var returnDto = new ContactDto
            {
                Id = contact.Id,
                EMail = contact.EMail ?? "N/A",
                FirstName = contact.FirstName ?? "N/A",
                LastName = contact.LastName ?? "N/A",
                TelephoneNumber_Entry = contact.TelephoneNumber_Entry ?? "N/A"
            };

            return returnDto;

        }

        public static ContactViewModel ConvertToViewModel(this Domain.DomainModels.Contact.Contact contact)
        {
            var returnViewModel = new ContactViewModel
            {
                Id = contact.Id,
                EMail = contact.EMail ?? "N/A",
                FirstName = contact.FirstName ?? "N/A",
                LastName = contact.LastName ?? "N/A",
                TelephoneNumber_Entry = contact.TelephoneNumber_Entry ?? "N/A",
                TotalCount = contact.TotalCount
            };

            return returnViewModel;

        }

        public static IEnumerable<ContactViewModel> ConvertToViewModel(this IEnumerable<Domain.DomainModels.Contact.Contact> contacts)
        {
            return contacts.Select(contact => contact.ConvertToViewModel()).ToList();
        }
    }
}
