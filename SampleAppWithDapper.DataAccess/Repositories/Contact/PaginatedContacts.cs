using System.Collections.Generic;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class PaginatedContacts
    {
        public List<ContactViewModel> Contacts { get;  set; }
        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }
    }
}