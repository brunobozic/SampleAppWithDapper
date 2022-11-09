using System.Configuration;
using System.Data.SqlClient;

namespace SampleAppWithDapper.DataAccess
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = string.IsNullOrEmpty(connectionString) ? ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString : connectionString;
        }

        public SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}