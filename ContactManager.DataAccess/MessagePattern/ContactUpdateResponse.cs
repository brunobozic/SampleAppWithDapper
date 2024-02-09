using SampleAppWithDapper.DataAccess.DTOs;

namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactUpdateResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public int UpdatedId { get; set; }
        public ContactDto Contact { get; set; }
        public bool ContactDeleted { get; set; }
    }
}