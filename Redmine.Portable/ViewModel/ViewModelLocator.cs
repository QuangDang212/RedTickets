using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Redmine.Portable.ViewModel
{
    public class ViewModelLocator
    {
        public static string LOGIN_PAGE_KEY = "LoginPage";
        public static string PROJECTS_PAGE_KEY = "ProjectsPage";
        public static string PROJECT_PAGE_KEY = "ProjectPage";
        public static string ISSUE_PAGE_KEY = "IssuePage";

        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public ProjectsViewModel ProjectsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectsViewModel>();
            }
        }

        public ProjectViewModel ProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectViewModel>();
            }
        }

        public IssueViewModel IssueViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IssueViewModel>();
            }
        }
    }
}