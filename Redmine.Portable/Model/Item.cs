using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public abstract class Item
    {
        public int Id { get; set; }

        [JsonProperty("created_on")]
        public DateTime Created { get; set; }

        [JsonProperty("updated_on")]
        public DateTime Updated { get; set; }
    }
}
