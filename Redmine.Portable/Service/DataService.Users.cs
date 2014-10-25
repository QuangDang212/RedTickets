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
        private const string USERS_URL = "users.json";
        private const string CURRENT_USER_URL = "users/current.json";

        public async Task<HttpResponse<UsersResult>> GetUsers(int? offset = null, int? limit = null)
        {
            var requestUrl = _credential.EndpointUrl + USERS_URL;
            var result = await GetDeserializedObject<UsersResult>(requestUrl, HttpMode.Get);

            return result;
        }

        public async Task<HttpResponse<UserResult>> GetCurrentUser(bool includeMemberships = true, bool includeGroups = true)
        {
            var requestUrl = _credential.EndpointUrl + CURRENT_USER_URL;
            string include = String.Empty;
            if (includeMemberships)
                include += "memberships,";
            if (includeGroups)
                include += "groups,";

            if (!String.IsNullOrEmpty(include))
                requestUrl = String.Format("{0}?include={1}", requestUrl, include);

            var result = await GetDeserializedObject<UserResult>(requestUrl, HttpMode.Get);

            return result;
        }
    }
}
