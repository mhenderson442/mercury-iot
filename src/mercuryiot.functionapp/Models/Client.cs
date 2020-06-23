using System.ComponentModel.DataAnnotations;

namespace Mercuryiot.Functions.Models
{
    public class Client
    {
        [Key]
        public string id { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public int? ttl { get; set; } = -1;
    }
}