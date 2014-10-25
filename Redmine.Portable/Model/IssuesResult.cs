﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Model
{
    public class IssuesResult : ListResult
    {
        public IEnumerable<Issue> Issues { get; set; }
    }
}
