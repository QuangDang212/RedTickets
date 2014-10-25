using Redmine.Portable.Interface;
using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Service
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient = new HttpClient();

        public void Initialize(string userName, string password)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", userName, password))));
        }

        public async Task<HttpResponse<String>> ExecuteAsync(string requestUrl, HttpMode mode, string message = null)
        {
            HttpContent requestContent = null;
            HttpResponseMessage result = null;
            if (!String.IsNullOrEmpty(message))
                requestContent = new StringContent(message, Encoding.UTF8, "application/json");

            try
            {
                result = await ExecuteHttpAsync(requestUrl, mode, requestContent);
            }
            catch (Exception ex)
            {
                return new HttpResponse<string>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.ServiceUnavailable,
                    Message = ex.Message
                };
            }

            var content = await result.Content.ReadAsStringAsync();

            return new HttpResponse<string>
            {
                HttpStatusCode = result.StatusCode,
                Message = content,
                Location = result.RequestMessage.RequestUri,
                Result = content
            };
        }

        private async Task<HttpResponseMessage> ExecuteHttpAsync(string requestUrl, HttpMode mode, HttpContent requestContent)
        {
            switch (mode)
            {
                case HttpMode.Get:
                    return await _httpClient.GetAsync(requestUrl);
                case HttpMode.Post:
                    return await _httpClient.PostAsync(requestUrl, requestContent);
                case HttpMode.Put:
                    return await _httpClient.PutAsync(requestUrl, requestContent);
                case HttpMode.Delete:
                    return await _httpClient.DeleteAsync(requestUrl);
                default:
                    return null;
            }
        }

        public async Task<HttpResponse<byte[]>> GetBinaryAsync(string requestUrl)
        {
            var result = await _httpClient.GetAsync(requestUrl);
            var response = new HttpResponse<byte[]>();
            response.HttpStatusCode = result.StatusCode;
            response.Location = result.RequestMessage.RequestUri;

            if (result.IsSuccessStatusCode)
            {
                var bytes = await result.Content.ReadAsByteArrayAsync();
                response.Result = bytes;
            }
            else
            {
                response.Message = await result.Content.ReadAsStringAsync();
            }

            return response;
        }
    }
}
