using SampleAppWithDapper.Domain.DomainModels.Contact;

namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactGetByIdResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public Contact Contact { get; set; }
    }
}