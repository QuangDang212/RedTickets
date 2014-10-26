using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class Project : Item
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public Project Parent { get; set; }
        public int Status { get; set; }
    }
}
