namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactCreateResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public int InsertedId { get; set; } = -1;
        public ContactDto Contact { get; set; }
    }
}