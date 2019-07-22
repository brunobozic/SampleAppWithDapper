using System;

namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactUpdateRequest
    {
        public string TelephoneNumber_Entry;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Updater { get; set; }
        public int Id { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public string CreatedBy { get; set; }
    }
}