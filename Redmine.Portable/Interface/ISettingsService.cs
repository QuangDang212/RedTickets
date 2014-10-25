using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.Interface
{
    public interface ISettingsService
    {
        object GetSetting(string key);

        void SetSetting(string key, object value);
    }
}
