namespace Mercuryiot.Web.Models
{
    public class GroupDeviceRegistration
    {
        public string PrimaryKey { get; set; }
        public string RegistrationId { get; set; }
        public string SecondaryKey { get; set; }
        public Tags Tags { get; set; }

        private string computedDerivedSymetricPrimaryKey;


    }
}