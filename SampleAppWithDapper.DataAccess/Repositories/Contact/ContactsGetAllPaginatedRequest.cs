using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public class ContactsGetAllPaginatedRequest
    {
        //@SearchTerm VARCHAR(50) = NULL,
        //@SortColumn VARCHAR(50) = 'LastName',
        //@SortOrder VARCHAR(50) = 'ASC',
        //@PageNumber INT = 1,
        //@PageSize INT = 10

        public string SearchTerm { get; set; } = "";
        public string SortColumn { get; set; } = "LastName";
        public string SortOrder { get; set; } = "ASC";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
