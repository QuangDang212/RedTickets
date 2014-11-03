using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class Tracker : Item
    {
        public String Name { get; set; }
        public int CountTotal { get; set; }
        public int CountOpen { get; set; }
    }
}
