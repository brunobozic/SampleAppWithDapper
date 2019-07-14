namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactGetByIdResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public Domain.DomainModels.Contact.Contact Contact { get; set; }
    }
}