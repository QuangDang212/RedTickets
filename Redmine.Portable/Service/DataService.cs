using Newtonsoft.Json;
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
        private IHttpService _httpService;
        private ILocalStorageService _localStorageService;
        private EndpointCredential _credential;

        private const string PARAMETER_START = "?";
        private const string PARAMETER_DELIMITER = "&";
        private const string OFFSET_FORMAT = "offset={0}";
        private const string LIMIT_FORMAT = "limit={0}";

        public DataService(IHttpService httpService, ILocalStorageService localStorageService)
        {
            _httpService = httpService;
            _localStorageService = localStorageService;
        }

        public void Initialize(EndpointCredential endpointCredential)
        {
            _credential = endpointCredential;
            _httpService.Initialize(endpointCredential.UserName, endpointCredential.Password);

            if (!_credential.EndpointUrl.EndsWith("/"))
            {
                _credential.EndpointUrl += "/";
            }
        }

        private async Task<HttpResponse<T>> GetDeserializedObject<T>(string requestUrl, HttpMode mode, string message = null)
        {
            var result = await _httpService.ExecuteAsync(requestUrl, mode, message);
            var response = new HttpResponse<T>
            {
                HttpStatusCode = result.HttpStatusCode,
                Message = result.Message,
                Location = result.Location
            };

            if (result.IsSuccessStatusCode)
            {
                response.Result = await Task.Run(() => { return JsonConvert.DeserializeObject<T>(result.Result); });
            }

            return response;
        }

        private string ListParameterFactory(string requestUrl, int? offset, int? limit, List<string> parameters = null)
        {
            if (parameters == null)
                parameters = new List<string>();
            if (offset.HasValue)
                parameters.Add(String.Format(OFFSET_FORMAT, offset.Value));

            if (limit.HasValue)
                parameters.Add(String.Format(LIMIT_FORMAT, limit.Value));

            return ParameterFactory(requestUrl, parameters);
        }

        private string ParameterFactory(string requestUrl, List<string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                var parameterString = PARAMETER_START;
                for (int i = 0; i < parameters.Count; i++)
                {
                    parameterString += parameters[i];
                    if (i < parameters.Count - 1)
                        parameterString += PARAMETER_DELIMITER;
                }

                requestUrl += parameterString;
            }

            return requestUrl;
        }
    }
}
