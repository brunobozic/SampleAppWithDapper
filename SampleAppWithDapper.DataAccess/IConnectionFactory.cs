using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SampleAppWithDapper.DataAccess
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
