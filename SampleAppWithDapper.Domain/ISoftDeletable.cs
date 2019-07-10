namespace SampleAppWithDapper.Domain
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}