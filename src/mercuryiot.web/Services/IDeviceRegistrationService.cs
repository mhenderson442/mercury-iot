using System.Threading.Tasks;
using Mercuryiot.Web.Models;
using Microsoft.Azure.Devices.Provisioning.Client;

namespace Mercuryiot.Web.Services
{
    public interface IDeviceRegistrationService
    {
         Task<DeviceRegistrationResult> RegisterDeviceAsync(ProvisioningTransportEnum provisioningTransport, GroupDeviceRegistration groupDeviceRegistration);
    }
}