using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Interface
{
    public interface IDataService
    {
        void Initialize(EndpointCredential endpointCredential);

        Task<HttpResponse<IssuesResult>> GetIssues(int? offset = null, int? limit = null, string sort = null, int? projectId = null, int? subProjectId = null, int? trackerId = null, int? statusId = null, int? assignedToId = null);

        Task<HttpResponse<ProjectsResult>> GetProjects(int? offset = null, int? limit = null);

        Task<HttpResponse<ProjectResult>> GetProject(int id);

        Task<HttpResponse<UsersResult>> GetUsers(int? offset = null, int? limit = null);

        Task<HttpResponse<UserResult>> GetCurrentUser(bool includeMemberships = true, bool includeGroups = true);
    }
}
