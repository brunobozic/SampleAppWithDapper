

namespace SampleAppWithDapper.DataAccess.MessagePattern
{
    public class ContactsGetAllPaginatedRequest
    {
        public string SearchTerm { get; set; } = "";
        public string SortColumn { get; set; } = "LastName";
        public string SortOrder { get; set; } = "ASC";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
