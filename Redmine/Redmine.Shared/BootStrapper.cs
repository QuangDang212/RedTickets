using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Redmine.Portable.Interface;
using Redmine.Portable.Service;
using Redmine.Portable.ViewModel;
using Redmine.Service;
using Redmine.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine
{
    public class BootStrapper
    {
        public BootStrapper()
        {
            Init();
        }

        private void Init()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure(ViewModelLocator.PROJECTS_PAGE_KEY, typeof(ProjectsPage));
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<ISettingsService, SettingsService>();
            SimpleIoc.Default.Register<ILocalStorageService, LocalStorageService>();
            SimpleIoc.Default.Register<IHttpService, HttpService>();
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IResourceService, ResourceService>();
            SimpleIoc.Default.Register<ICredentialService, CredentialService>();

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<ProjectsViewModel>();
        }
    }
}
