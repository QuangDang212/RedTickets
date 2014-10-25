using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class ProjectsResult : ListResult
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}
