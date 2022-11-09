using SampleAppWithDapper.DataAccess.DTOs;
using SampleAppWithDapper.Domain.DomainModels.Contact;

namespace ContactManager.DataAccess.Extensions
{
    public static class ContactExtensions
    {
        public static ContactDto ConvertToDto(this Contact contact)
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
    }
}