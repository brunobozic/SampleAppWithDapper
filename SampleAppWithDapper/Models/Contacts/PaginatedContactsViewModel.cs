using System.Collections.Generic;

namespace SampleAppWithDapper.Models.Contacts
{
    public class PaginatedContactsViewModel
    {
        public List<ContactViewModel> Contacts { get;  set; }
        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }
    }
}