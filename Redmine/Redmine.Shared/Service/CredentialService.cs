using Redmine.Portable.Interface;
using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Redmine.Service
{
    public class CredentialService : ICredentialService
    {
        private const string ENDPOINT_URL_KEY = "EndpointUrl";
        private PasswordVault _vault = new PasswordVault();
        private ISettingsService _settingsService;

        public CredentialService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void SaveEndpointCredential(EndpointCredential ec)
        {
            var credentials = new PasswordCredential(ec.Name, ec.UserName, ec.Password);
            _settingsService.SetSetting(ec.Name + ENDPOINT_URL_KEY, ec.EndpointUrl);
            _vault.Add(credentials);
        }

        public EndpointCredential GetEndpointCredential(string name)
        {
            try
            {
                var credentials = _vault.FindAllByResource(name);
                if (credentials != null && credentials.Count > 0)
                {
                    var c = credentials.FirstOrDefault();
                    if (c != null)
                    {
                        c.RetrievePassword();
                        var endpointUrl = (String)_settingsService.GetSetting(name + ENDPOINT_URL_KEY);

                        var ec = new EndpointCredential()
                        {
                            Name = name,
                            EndpointUrl = endpointUrl,
                            UserName = c.UserName,
                            Password = c.Password
                        };
                        return ec;
                    }
                }
            }
            catch
            {
            }

            return null;
        }

        public void DeleteEndpointCredential(string name)
        {
            try
            {
                var credentials = _vault.FindAllByResource(name);
                if (credentials != null)
                {
                    foreach (PasswordCredential c in credentials)
                    {
                        _vault.Remove(c);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
