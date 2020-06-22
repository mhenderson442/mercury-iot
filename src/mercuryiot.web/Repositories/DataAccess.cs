using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Mercuryiot.Web.Repositories
{
    public class DataAccess : IDataAccess
    {
        private readonly ILogger<IDataAccess> _logger;
        private readonly string _connectionString;

        public DataAccess(ILogger<IDataAccess> logger, IConfiguration configuration)
        {
            _logger = logger;

            var userId = configuration.GetValue<string>("MercurySqlConnection:UserId");
            var password = configuration.GetValue<string>("MercurySqlConnection:Password");
            var initialCatalog = configuration.GetValue<string>("MercurySqlConnection:InitialCatalog");
            var dataSource = configuration.GetValue<string>("MercurySqlConnection:DataSource");

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                PersistSecurityInfo = false,
                Encrypt = true,
                UserID = userId,
                Password = password,
                InitialCatalog = initialCatalog,
                TrustServerCertificate = false,
                MultipleActiveResultSets = false,
                Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword
            };

            _connectionString = connectionStringBuilder.ConnectionString;
            ;
        }

        public async Task<SqlConnection> GetSensorMonitoringSqlConnectionAsync()
        {
            await Task.Yield();
            var sqlConnection = new SqlConnection(_connectionString);

            return sqlConnection;
        }
    }
}