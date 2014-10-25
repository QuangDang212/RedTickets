using Redmine.Portable.Interface;
using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Service
{
    public partial class DataService : IDataService
    {
        private const string ISSUES_URL = "issues.json";

        public async Task<HttpResponse<IssuesResult>> GetIssues(int? offset = null, int? limit = null, string sort = null, int? projectId = null, int? subProjectId = null, int? trackerId = null, int? statusId = null, int? assignedToId = null)
        {
            var requestUrl = _credential.EndpointUrl + ISSUES_URL;
            var result = await GetDeserializedObject<IssuesResult>(requestUrl, HttpMode.Get);

            return result;
        }
    }
}
