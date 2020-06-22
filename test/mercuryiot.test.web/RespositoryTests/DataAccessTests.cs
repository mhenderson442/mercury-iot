using System.Threading.Tasks;
using Mercuryiot.Web.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Web.RespositoryTests
{
    public class DataAccessTests
    {
        private readonly Mock<ILogger<IDataAccess>> _logger;

        private readonly DataAccess _dataAccess;

        public DataAccessTests()
        {
            _logger = new Mock<ILogger<IDataAccess>>();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.Development.json", false);

            var configuration = configurationBuilder.Build();
            var vault = configuration.GetValue<string>("KeyVaultEndpoint");

            var clientId = configuration.GetValue<string>("ClientId");
            var clientSecret = configuration.GetValue<string>("ClientSecret");

            configurationBuilder.AddAzureKeyVault(vault, clientId, clientSecret);

            configuration = configurationBuilder.Build();

            _dataAccess = new DataAccess(_logger.Object, configuration);
        }

        [Fact(DisplayName = "GetSensorMonitoringSqlConnection should successfully connect to an Azure SQL database.")]
        public async Task GetSensorMonitoringSqlConnection_OpensSuccessfully()
        {
            // Arrange
            // Act
            using var sut = await _dataAccess.GetSensorMonitoringSqlConnectionAsync();
            sut.Open();

            // Assert
            Assert.IsType<SqlConnection>(sut);
            Assert.True(sut.State == System.Data.ConnectionState.Open);
        }
    }
}