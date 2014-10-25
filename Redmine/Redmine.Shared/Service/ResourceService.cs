using Redmine.Portable.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;

namespace Redmine.Service
{
    public class ResourceService : IResourceService
    {
        private ResourceLoader _resLoader = new ResourceLoader();

        public string GetString(string key)
        {
            return _resLoader.GetString(key);
        }
    }
}
