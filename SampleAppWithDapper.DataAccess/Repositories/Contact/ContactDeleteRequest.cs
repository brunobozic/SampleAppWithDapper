using System;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactDeleteRequest
    {
        public int? Id { get; set; }
        public string Deleter { get; set; }
    }
}