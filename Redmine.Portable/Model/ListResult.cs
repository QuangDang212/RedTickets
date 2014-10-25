using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public abstract class ListResult
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
