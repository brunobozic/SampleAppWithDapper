using System;

namespace SampleAppWithDapper.DataAccess.DTOs
{
    public class ContactDto
    {
        public string TelephoneNumber_Entry;
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public string ModifiedBy { get; set; }
    }
}