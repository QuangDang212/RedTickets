using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class EndpointCredential
    {
        public string Name { get; set; }
        public string EndpointUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
