
using System.Data;
using System.Data.SqlClient;

namespace SampleAppWithDapper.DataAccess
{
    public interface IDbConnectionProvider
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
    }
    public class DatabaseConnectionManager : IDbConnectionProvider
    {
        public DatabaseConnectionManager(string connection)
        {
            Connection = new SqlConnection(connection);
        }

        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
    }
}
