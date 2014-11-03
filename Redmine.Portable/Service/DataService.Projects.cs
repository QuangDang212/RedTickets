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
        private const string PROJECTS_URL = "projects.json";
        private const string PROJECT_URL = "projects/{0}.json";
        private const string MEMBERSHIPS_URL = "projects/{0}/memberships.json";

        public async Task<HttpResponse<ProjectsResult>> GetProjects(int? offset = null, int? limit = null)
        {
            var requestUrl = ListParameterFactory(_credential.EndpointUrl + PROJECTS_URL, offset, limit);
            var result = await GetDeserializedObject<ProjectsResult>(requestUrl, HttpMode.Get);

            return result;
        }

        public async Task<HttpResponse<ProjectResult>> GetProject(int projectId)
        {
            var requestUrl = _credential.EndpointUrl + String.Format(PROJECT_URL, projectId);
            var result = await GetDeserializedObject<ProjectResult>(requestUrl, HttpMode.Get);

            return result;
        }

        public async Task<HttpResponse<MembershipsResult>> GetMemberships(int projectId, int? offset = null, int? limit = null)
        {
            var requestUrl = ListParameterFactory(_credential.EndpointUrl + String.Format(MEMBERSHIPS_URL, projectId), offset, limit);
            var result = await GetDeserializedObject<MembershipsResult>(requestUrl, HttpMode.Get);

            return result;
        }
    }
}
