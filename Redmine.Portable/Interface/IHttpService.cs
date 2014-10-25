using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Interface
{
    public interface IHttpService
    {
        void Initialize(string userName, string password);

        Task<HttpResponse<String>> ExecuteAsync(string requestUrl, HttpMode mode, string message = null);

        Task<HttpResponse<byte[]>> GetBinaryAsync(string requestUrl);
    }
}
