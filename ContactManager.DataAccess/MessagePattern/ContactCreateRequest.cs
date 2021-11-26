namespace SampleAppWithDapper.DataAccess.MessagePattern
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