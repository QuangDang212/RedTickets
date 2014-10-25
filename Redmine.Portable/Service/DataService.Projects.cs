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

        public async Task<HttpResponse<ProjectsResult>> GetProjects(int? offset = null, int? limit = null)
        {
            var requestUrl = ListParameterFactory(_credential.EndpointUrl + PROJECTS_URL, offset, limit);
            var result = await GetDeserializedObject<ProjectsResult>(requestUrl, HttpMode.Get);

            return result;
        }
    }
}
