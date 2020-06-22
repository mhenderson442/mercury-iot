using System.Threading.Tasks;
using Mercuryiot.Web.Models;
using Mercuryiot.Web;
using Mercuryiot.Web.Services;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Mercuryiot.Test.Web.ServiceTests
{
    [Trait("Web Tests", "Device Registration Service Tests")]
    public class DeviceRegistrationServiceTests
    {
        private readonly Mock<ILogger<DeviceRegistrationService>> _logger;     
        private readonly DeviceRegistrationService _deviceRegistrationService;
        private readonly string _primaryKey;
        private readonly string _secondaryKey;

        public DeviceRegistrationServiceTests()
        {
            _logger = new Mock<ILogger<DeviceRegistrationService>>();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.Development.json", false);

            var configuration = configurationBuilder.Build();
            var _iotHubOptions = configuration.GetValue<string>("KeyVaultEndpoint");

            var vault = configuration.GetValue<string>("KeyVaultEndpoint");
            var clientId = configuration.GetValue<string>("ClientId");
            var clientSecret = configuration.GetValue<string>("ClientSecret");

            configurationBuilder.AddAzureKeyVault(vault, clientId, clientSecret);

            configuration = configurationBuilder.Build();

            _primaryKey = configuration.GetValue<string>("sensor-monitoring-iothub:primaryKey");
            _secondaryKey = configuration.GetValue<string>("sensor-monitoring-iothub:secondaryKey");

            _deviceRegistrationService = new DeviceRegistrationService(_logger.Object, configuration);
        }

        [Theory(DisplayName = "DeviceRegistrationService.RegisterAsync method should return a boolean indicating sucess")]
        // [InlineData(ProvisioningTransportEnum.Amqp)]
        [InlineData(ProvisioningTransportEnum.Http)]
        // [InlineData(ProvisioningTransportEnum.Mqtt)]
        public async Task RegisterDeviceAsyncReturnsSuccess(ProvisioningTransportEnum provisioningTransport){

            // Arrange
           
            var groupDeviceRegistration = new GroupDeviceRegistration {
                PrimaryKey = _primaryKey, 
                SecondaryKey = _secondaryKey,
                RegistrationId = $"Test-entry-{ provisioningTransport.ToString().ToLower()  }", 
                Tags = new Tags
                {
                    ClientId = "testClient_001",
                    Location = new Location { Building = "Dental Clinic", Region = "US" }
                }
            };

            // Act
            var sut = await _deviceRegistrationService.RegisterDeviceAsync(provisioningTransport, groupDeviceRegistration);
            
            // Assert
            Assert.IsType<DeviceRegistrationResult>(sut);           
        }
    }
}