using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Redmine.Portable.Interface;
using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.ViewModel
{
    public class ProjectsViewModel : AsyncViewModelBase
    {
        private IDataService _dataService;
        private IResourceService _resourceService;
        private IExtendedNavigationService _navigationService;
        private IDialogService _dialogService;

        public RelayCommand InitCommand { get; private set; }
        public RelayCommand<Project> ShowProjectCommand { get; private set; }

        private List<Project> _projects;
        public List<Project> Projects
        {
            get { return _projects; }
            set { _projects = value; RaisePropertyChanged(); }
        }

        public ProjectsViewModel(IDataService dataService, IResourceService resourceService, IExtendedNavigationService navigationService, IDialogService dialogService)
        {
            _dataService = dataService;
            _resourceService = resourceService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            InitCommand = new RelayCommand(Init);
            ShowProjectCommand = new RelayCommand<Project>(ShowProject);
        }

        private async void Init()
        {
            IsLoading = true;
            LoadingMessage = _resourceService.GetString("ProjectsLoadingMessage");

            var result = await _dataService.GetProjects(0, Int16.MaxValue);

            IsLoading = false;
            LoadingMessage = null;

            if (result.IsSuccessStatusCode && result.Result != null && result.Result.Projects != null)
            {
                Projects = result.Result.Projects.ToList();
            }
            else
            {
                var dialogResult = await _dialogService.ShowMessage(_resourceService.GetString("ProjectsLoadingErrorMessage"), _resourceService.GetString("ErrorTitle"), _resourceService.GetString("ButtonRetry"), _resourceService.GetString("ButtonCancel"), null);
                if (dialogResult)
                    Init();
            }
        }

        private void ShowProject(Project p)
        {
            _navigationService.NavigateTo(ViewModelLocator.PROJECT_PAGE_KEY, p);
        }
    }
}
