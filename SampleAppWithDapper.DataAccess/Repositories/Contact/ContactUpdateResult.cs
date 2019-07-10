namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactUpdateResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public int UpdatedId { get; set; }
        public Domain.DomainModels.Contact.Contact Contact { get; set; }
    }
}