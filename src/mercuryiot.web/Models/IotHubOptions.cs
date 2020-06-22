namespace Mercuryiot.Web.Models
{
    public class IotHubOptions
    {
        /// <summary>
        /// The GlobalDeviceEndpoint for the Device Provisioning Service.
        /// </summary>
        /// <value></value>
        public string GlobalDeviceEndpoint { get; set; }

        /// <summary>
        /// The security provider instance.
        /// </summary>
        /// <value></value>
        public string IdScope { get; set; }

        /// <summary>
        /// Event Hub-compatible name setting in IoT Hub.
        /// </summary>
        /// <value></value>
        public string RegistrationId { get; set; }

        /// <summary>
        /// Primary key from Shared access policies.
        /// </summary>
        /// <value></value>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Secondary key from Shared access policies.
        /// </summary>
        /// <value></value>
        public string SecondaryKey { get; set; }
    }
}