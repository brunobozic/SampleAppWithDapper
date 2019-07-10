namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactDto
    {
        public string TelephoneNumber_Entry;
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
    }
}