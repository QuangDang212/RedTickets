using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class User : Item
    {
        public String Login { get; set; }
        public String Name { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Mail { get; set; }

        [JsonProperty("last_login_on")]
        public DateTime LastLogin { get; set; }
        public List<Membership> Memberships { get; set; }
        public List<Group> Groups { get; set; }

        [JsonProperty("api_key")]
        public String ApiKey { get; set; }
    }
}
