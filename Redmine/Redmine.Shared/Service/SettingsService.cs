using Redmine.Portable.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace Redmine.Service
{
    public class SettingsService : ISettingsService
    {
        public object GetSetting(string key)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                return ApplicationData.Current.LocalSettings.Values[key];

            return null;
        }

        public void SetSetting(string key, object value)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                ApplicationData.Current.LocalSettings.Values[key] = value;
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values.Add(key, value);
            }
        }
    }
}
