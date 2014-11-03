using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class Membership : Item
    {
        public User User { get; set; }
        public Project Project { get; set; }
        public List<Role> Roles { get; set; }
    }
}
