using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class Journal : Item
    {
        public User User { get; set; }
        public string Notes { get; set; }
    }
}
