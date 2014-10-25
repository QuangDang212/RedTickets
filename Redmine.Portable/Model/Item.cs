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

        //todo: add created_on and updated_on datetimes
    }
}
