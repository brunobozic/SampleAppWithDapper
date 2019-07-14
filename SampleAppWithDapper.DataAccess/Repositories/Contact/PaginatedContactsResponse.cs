using System.Collections.Generic;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class PaginatedContactsResponse
    {
        public int TotalCount { get; set; }
        public List<Domain.DomainModels.Contact.Contact> Contacts { get; set; }
        public int FilteredCount { get; set; }
        public bool Success { get; set; } = false;
    }
}