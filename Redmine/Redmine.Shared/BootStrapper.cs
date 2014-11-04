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

            var nav = new ExtendedNavigationService();
            nav.Configure(ViewModelLocator.LOGIN_PAGE_KEY, typeof(LoginPage));
            nav.Configure(ViewModelLocator.PROJECTS_PAGE_KEY, typeof(ProjectsPage));
            nav.Configure(ViewModelLocator.PROJECT_PAGE_KEY, typeof(ProjectPage));
            nav.Configure(ViewModelLocator.ISSUE_PAGE_KEY, typeof(IssuePage));
            SimpleIoc.Default.Register<IExtendedNavigationService>(() => nav);

            SimpleIoc.Default.Register<ISettingsService, SettingsService>();
            SimpleIoc.Default.Register<ILocalStorageService, LocalStorageService>();
            SimpleIoc.Default.Register<IHttpService, HttpService>();
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IResourceService, ResourceService>();
            SimpleIoc.Default.Register<ICredentialService, CredentialService>();
            SimpleIoc.Default.Register<INotificationService, NotificationService>();

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<ProjectsViewModel>();
            SimpleIoc.Default.Register<ProjectViewModel>();
            SimpleIoc.Default.Register<IssueViewModel>();
        }
    }
}
