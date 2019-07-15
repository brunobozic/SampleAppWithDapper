
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleAppWithDapper.DataAccess.Repositories.Contact;

namespace SampleAppWithDapper.DataAccess.Test
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public PaginatedContacts Get_Paginated_Contacts()
        {
            var conn = new DatabaseConnectionManager(@"Data Source=DESKTOP-4DS2D0D\SQLEXPRESS;Initial Catalog=Dapper;Trusted_Connection=True;MultipleActiveResultSets=True");
            var repo = new ContactRepository(conn);
            var requestForPaginatedContacts = new ContactsGetAllPaginatedRequest
            {
                PageNumber = 1,
                PageSize = 10,
                SearchTerm = "",
                SortColumn = "",
                SortOrder = "ASC"
            };

            var res =  repo.GetPaginatedResultsAsync(requestForPaginatedContacts).Result;

            return res;
        }
    }
}
