using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mercuryiot.Web.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Mercuryiot.Web.Services
{
    public class DeviceRegistrationService : IDeviceRegistrationService
    {
        private readonly ILogger<IDeviceRegistrationService> _logger;
        private readonly IotHubOptions _iotHubOptions;

        private readonly string _iothubConnectionString;

        private readonly DeviceProvisioningServiceOptions _deviceProvisioningServiceOptions;

        private const string GlobalDeviceEndpoint = "global.azure-devices-provisioning.net";

        private readonly string _primaryKey;
        private readonly string _secondaryKey;

        public DeviceRegistrationService(ILogger<IDeviceRegistrationService> logger, IConfiguration configuration)
        {
            _logger = logger;

            _deviceProvisioningServiceOptions = new DeviceProvisioningServiceOptions
            {
                IdScope = configuration.GetValue<string>("DeviceProvisioningService:IdScope")
            };

            configuration.GetValue<DeviceProvisioningServiceOptions>("DeviceProvisioningService");

            _iotHubOptions = configuration.GetSection("IotHubOptions").Get<IotHubOptions>();
            _iothubConnectionString = configuration.GetValue<string>("sensor-monitoring-iothub:primaryConnectionString");

            _primaryKey = configuration.GetValue<string>("sensor-monitoring-iothub:primaryKey");
            _secondaryKey = configuration.GetValue<string>("sensor-monitoring-iothub:secondaryKey");

        }

        public async Task <ProvisioningDeviceClient> CreateProvisioningDeviceClient(ProvisioningTransportEnum provisioningTransport, GroupDeviceRegistration groupDeviceRegistration)
        {
            var transport = await GetProvisioningTransportHandler(provisioningTransport);
            var securityProvider = new SecurityProviderSymmetricKey(groupDeviceRegistration.RegistrationId, groupDeviceRegistration.PrimaryKey, groupDeviceRegistration.SecondaryKey);

            var provisioningDeviceClient = ProvisioningDeviceClient.Create(GlobalDeviceEndpoint, _deviceProvisioningServiceOptions.IdScope, securityProvider, transport);
            provisioningDeviceClient.ProductInfo = "Test product info";

            return provisioningDeviceClient;
        }

        public async Task<DeviceRegistrationResult> RegisterDeviceAsync(
            ProvisioningTransportEnum provisioningTransport,
            GroupDeviceRegistration groupDeviceRegistration)
        {

            var masterPrimaryKey = Convert.FromBase64String(_primaryKey);
            var masterSecondaryKey = Convert.FromBase64String(_secondaryKey);

            var computedDerivedSymetricPrimaryKey = await ComputeDerivedSymmetricKey(masterPrimaryKey, groupDeviceRegistration.RegistrationId);
            var computedDerivedSymetricSecondayKey = await ComputeDerivedSymmetricKey(masterSecondaryKey, groupDeviceRegistration.RegistrationId);

            groupDeviceRegistration.PrimaryKey = computedDerivedSymetricPrimaryKey;
            groupDeviceRegistration.SecondaryKey = computedDerivedSymetricSecondayKey;

            using var security = await GetSecurityProviderSymmetricKey(groupDeviceRegistration.RegistrationId);

            var client = await CreateProvisioningDeviceClient(provisioningTransport, groupDeviceRegistration);

            var deviceRegistrationResult = await client.RegisterAsync().ConfigureAwait(false);

            var registryManager = RegistryManager.CreateFromConnectionString(_iothubConnectionString);
            var twin = await registryManager.GetTwinAsync(deviceRegistrationResult.DeviceId);

            var twinPatch = $"{{tags: { JsonSerializer.Serialize(groupDeviceRegistration.Tags) }}}";
            var result = await registryManager.UpdateTwinAsync(twin.DeviceId, twinPatch, twin.ETag);

            return deviceRegistrationResult;
        }

        private async Task<SecurityProviderSymmetricKey> GetSecurityProviderSymmetricKey(string registrationId)
        {
            var primaryKey = await ComputeDerivedSymmetricKey(Convert.FromBase64String(_primaryKey), registrationId);
            var secondaryKey = await ComputeDerivedSymmetricKey(Convert.FromBase64String(_secondaryKey), registrationId);

            var security = new SecurityProviderSymmetricKey(registrationId, primaryKey, secondaryKey);
            return security;
        }

        public static async Task<string> ComputeDerivedSymmetricKey(byte[] masterKey, string registrationId)
        {
            await Task.Yield();
            using var hmac = new HMACSHA256(masterKey);
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationId)));
        }

        private static async Task<ProvisioningTransportHandler> GetProvisioningTransportHandler(ProvisioningTransportEnum provisioningTransport)
        {
            await Task.Yield();

            ProvisioningTransportHandler provisioningTransportHandler = provisioningTransport switch
            {
                ProvisioningTransportEnum.Amqp => new ProvisioningTransportHandlerAmqp(),
                ProvisioningTransportEnum.Http => new ProvisioningTransportHandlerHttp(),
                ProvisioningTransportEnum.Mqtt => new ProvisioningTransportHandlerMqtt(),
                _ => null,
            };

            return provisioningTransportHandler;
        }
    }
}