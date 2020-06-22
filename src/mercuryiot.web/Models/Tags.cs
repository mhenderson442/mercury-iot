namespace Mercuryiot.Web.Models
{
    public class Tags
    {
        private string clientId;
        public string ClientId { get => clientId; set => clientId = value; }

        private Location location;
        public Location Location { get => location; set => location = value; }
    }
}