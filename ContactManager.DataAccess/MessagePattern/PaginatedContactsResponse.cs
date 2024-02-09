using SampleAppWithDapper.Domain.DomainModels.Contact;
using System.Collections.Generic;

namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class PaginatedContactsResponse
    {
        public string Message { get; set; } = string.Empty;
        public int TotalCount { get; set; } = 0;
        public List<Contact> Contacts { get; set; }
        public int FilteredCount { get; set; } = 0;
        public bool Success { get; set; } = false;
    }
}