using SampleAppWithDapper.DataAccess.DTOs;

namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactGetByIdResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public ContactDto Contact { get; set; }
    }
}