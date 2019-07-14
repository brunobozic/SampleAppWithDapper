using System;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactDeleteRequest
    {
        public Nullable<int> Id { get; set; }
        public string Deleter { get; set; }
    }
}