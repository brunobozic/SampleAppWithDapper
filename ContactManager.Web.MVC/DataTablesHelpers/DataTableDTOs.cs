using System.Collections.Generic;

namespace SampleAppWithDapper.DataTablesHelpers
{
    public class DataTableDTOs
    {
        public class DataTableAjaxPostModel
        {
            // properties are not capital due to json mapping
            public int draw { get; set; }

            public int start { get; set; }
            public int length { get; set; }
            public List<Column> columns { get; set; }
            public Search search { get; set; }
            public string search_extra { get; set; }
            public List<Order> order { get; set; }
        }

        public class Column
        {
            // properties are not capital due to json mapping
            public string data { get; set; }

            public string name { get; set; }
            public bool searchable { get; set; }
            public bool orderable { get; set; }
            public Search search { get; set; }
        }

        public class Search
        {
            // properties are not capital due to json mapping
            public string value { get; set; }

            public string regex { get; set; }
        }

        public class Order
        {
            // properties are not capital due to json mapping
            public int column { get; set; }

            public string dir { get; set; }
        }
    }
}