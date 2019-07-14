using System.Collections.Generic;
using System.Linq;
using SampleAppWithDapper.DataAccess.Repositories.Contact;

namespace SampleAppWithDapper.Models.Contacts.Extensions
{
    public static class ContactExtensions
    {
        public static ContactDto ConvertToDto(this Domain.DomainModels.Contact.Contact contact)
        {
            var returnDto = new ContactDto
            {
                Id = contact.Id,
                EMail = contact.EMail,
                FirstName = contact.FirstName ?? "N/A",
                LastName = contact.LastName ?? "N/A",
                TelephoneNumber_Entry = contact.TelephoneNumber_Entry ?? "N/A",
                CreatedUtc = contact.CreatedUtc,
                CreatedBy = contact.CreatedBy ?? "N/A",
                ModifiedUtc = contact.ModifiedUtc,
                ModifiedBy = contact.ModifiedBy ?? "N/A"
            };

            return returnDto;

        }

        public static ContactDeleteViewModel ConvertToDeleteViewModel(this Domain.DomainModels.Contact.Contact contact)
        {
            var returnViewModel = new ContactDeleteViewModel
            {
                Id = contact.Id,
                EMail = contact.EMail,
                FirstName = contact.FirstName ?? "N/A",
                LastName = contact.LastName ?? "N/A",
                TelephoneNumber_Entry = contact.TelephoneNumber_Entry ?? "N/A",
                TotalCount = contact.TotalCount,
                CreatedUtc = contact.CreatedUtc.LocalDateTime,
                ModifiedUtc = contact.ModifiedUtc.GetValueOrDefault().LocalDateTime,
                CreatedBy = contact.CreatedBy ?? "N/A",
                ModifiedBy = contact.ModifiedBy ?? "N/A",
                Action = contact.Action
            };

            return returnViewModel;

        }


        public static ContactViewModel ConvertToViewModel(this Domain.DomainModels.Contact.Contact contact)
        {
            var returnViewModel = new ContactViewModel
            {
                Id = contact.Id,
                EMail = contact.EMail,
                FirstName = contact.FirstName ?? "N/A",
                LastName = contact.LastName ?? "N/A",
                TelephoneNumber_Entry = contact.TelephoneNumber_Entry ?? "N/A",
                TotalCount = contact.TotalCount,
                CreatedUtc = contact.CreatedUtc.LocalDateTime,
                ModifiedUtc = contact.ModifiedUtc.GetValueOrDefault().LocalDateTime,
                CreatedBy = contact.CreatedBy ?? "N/A",
                ModifiedBy = contact.ModifiedBy ?? "N/A",
                Action = contact.Action
            };

            return returnViewModel;

        }


        // TODO: from [Contact] to [PaginatedContactsViewModel]
        public static PaginatedContactsViewModel ConvertToPaginatedViewModel(this IEnumerable<Domain.DomainModels.Contact.Contact> contacts)
        {
            var returnViewModel = new PaginatedContactsViewModel
            {
                Contacts = contacts.ConvertToViewModel().ToList(),
                FilteredCount = contacts.FirstOrDefault().FilteredCount,
                TotalCount = contacts.FirstOrDefault().TotalCount
            };

            return returnViewModel;

        }

        public static IEnumerable<ContactViewModel> ConvertToViewModel(this IEnumerable<Domain.DomainModels.Contact.Contact> contacts)
        {
            return contacts.Select(contact => contact.ConvertToViewModel()).ToList();
        }

        public static EditContactViewModel ConvertToViewModel(this ContactDto contact)
        {
            var returnVM = new EditContactViewModel
            {
                EMail = contact.EMail ?? "N/A",
                FirstName = contact.FirstName ?? "N/A",
                LastName = contact.LastName ?? "N/A",
                TelephoneNumber_Entry = contact.TelephoneNumber_Entry ?? "N/A",
                CreatedUtc = contact.CreatedUtc,
                CreatedBy = contact.CreatedBy ?? "N/A",
                ModifiedUtc = contact.ModifiedUtc,
                ModifiedBy = contact.ModifiedBy ?? "N/A"
            };

            return returnVM;

        }
    }
}