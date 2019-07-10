namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactCreateRequest
    {
        public string TelephoneNumber_Entry;
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EMail { get; set; }
        public string Creator { get; set; }
    }
}