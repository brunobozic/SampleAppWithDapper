namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber_Entry { get; set; }
        public int TotalCount { get; set; }
        public string Action { get; set; }
    }
}