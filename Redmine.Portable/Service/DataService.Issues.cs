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

        public async Task<HttpResponse<IssuesResult>> GetIssues(int? offset = null, int? limit = null, string sort = null, int? projectId = null, int? subProjectId = null, int? trackerId = null, Statuses status = Statuses.open, int? assignedToId = null)
        {
            var parameters = new List<string>();
            if (projectId.HasValue)
                parameters.Add(String.Format("project_id={0}", projectId.Value));
            if (assignedToId.HasValue)
                parameters.Add(String.Format("assigned_to_id={0}", assignedToId.Value));

            var statusValue = status.ToString();
            if (status == Statuses.all)
                statusValue = "*";

            parameters.Add(String.Format("status_id={0}", statusValue));

            var requestUrl = ListParameterFactory(_credential.EndpointUrl + ISSUES_URL, offset, limit, parameters);
            var result = await GetDeserializedObject<IssuesResult>(requestUrl, HttpMode.Get);

            return result;
        }
    }
}
