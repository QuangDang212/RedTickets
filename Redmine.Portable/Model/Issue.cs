using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class Issue : Item
    {
        public Project Project { get; set; }
        public Tracker Tracker { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public User Author { get; set; }

        [JsonProperty("assigned_to")]
        public User AssignedTo { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
    }
}
