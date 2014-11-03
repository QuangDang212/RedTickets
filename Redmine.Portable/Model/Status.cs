using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class Status : Item
    {
        public string Name { get; set; }
    }

    public enum Statuses 
    {
        open,
        closed,
        all
    }
}
