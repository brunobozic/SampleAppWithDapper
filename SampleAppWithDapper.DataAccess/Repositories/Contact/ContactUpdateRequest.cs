namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactUpdateRequest
    {
        public string TelephoneNumber_Entry;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Updater { get; set; }
    }
}