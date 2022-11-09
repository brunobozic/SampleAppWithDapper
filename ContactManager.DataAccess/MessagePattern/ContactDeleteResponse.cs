namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactDeleteResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public bool ContactDeleted { get; set; }
        public int DeletedId { get; set; }
        public string CurrentFilter { get; set; }
    }
}