using System.Collections.Generic;
using SampleAppWithDapper.Domain.DomainModels.Contact;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class PaginatedContacts
    {
        public List<Domain.DomainModels.Contact.Contact> Contacts { get; internal set; }
        public int FilteredCount { get; set; }
    }
}