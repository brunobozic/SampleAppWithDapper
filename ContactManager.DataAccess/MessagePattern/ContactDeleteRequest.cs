namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactDeleteRequest
    {
        public int? Id { get; set; }
        public string Deleter { get; set; }
    }
}