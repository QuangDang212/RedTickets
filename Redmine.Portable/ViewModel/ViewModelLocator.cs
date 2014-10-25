using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Redmine.Portable.ViewModel
{
    public class ViewModelLocator
    {
        public static string PROJECTS_PAGE_KEY = "ProjectsPage";

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
    }
}