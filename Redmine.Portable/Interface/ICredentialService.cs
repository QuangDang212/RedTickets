using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Interface
{
    public interface ICredentialService
    {
        void SaveEndpointCredential(EndpointCredential ec);

        EndpointCredential GetEndpointCredential(string name);

        void DeleteEndpointCredential(string name);
    }
}
