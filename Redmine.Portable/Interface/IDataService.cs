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

        #region Issues

        Task<HttpResponse<IssuesResult>> GetIssues(int? offset = null, int? limit = null, string sort = null, int? projectId = null, int? subProjectId = null, int? trackerId = null, Statuses status = Statuses.open, int? assignedToId = null);

        #endregion

        #region Projects

        Task<HttpResponse<ProjectsResult>> GetProjects(int? offset = null, int? limit = null);

        Task<HttpResponse<ProjectResult>> GetProject(int projectId);

        Task<HttpResponse<MembershipsResult>> GetMemberships(int projectId, int? offset = null, int? limit = null);

        #endregion

        #region Users

        Task<HttpResponse<UsersResult>> GetUsers(int? offset = null, int? limit = null);

        Task<HttpResponse<UserResult>> GetCurrentUser(bool includeMemberships = true, bool includeGroups = true);

        #endregion
    }
}
