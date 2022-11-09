using System.Data.SqlClient;

namespace SampleAppWithDapper.DataAccess
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}