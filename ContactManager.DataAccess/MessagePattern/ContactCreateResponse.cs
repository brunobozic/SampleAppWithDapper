using SampleAppWithDapper.DataAccess.DTOs;

namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactCreateResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public int InsertedId { get; set; } = -1;
        public ContactDto Contact { get; set; }
    }
}