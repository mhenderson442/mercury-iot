using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercuryiot.Web.Models
{
    public class Location
    {
        private string building;
        private string region;
        public string Building { get => building; set => building = value; }
        public string Region { get => region; set => region = value; }
    }
}
