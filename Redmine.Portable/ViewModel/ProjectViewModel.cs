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
    public class ProjectViewModel : AsyncViewModelBase
    {
        private IDataService _dataService;
        private IResourceService _resourceService;
        private IDialogService _dialogService;
        private IExtendedNavigationService _navigationService;

        public RelayCommand<Project> InitCommand { get; private set; }

        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { _project = value; RaisePropertyChanged(); }
        }

        private string _projectTitle;
        public string ProjectTitle
        {
            get { return _projectTitle; }
            set { _projectTitle = value; RaisePropertyChanged(); }
        }

        public ProjectViewModel(IDataService dataService, IResourceService resourceService, IDialogService dialogService, IExtendedNavigationService navigationService) 
        {
            _dataService = dataService;
            _resourceService = resourceService;
            _dialogService = dialogService;
            _navigationService = navigationService;

            InitCommand = new RelayCommand<Project>(Init);

            if (IsInDesignMode)
            {
                ProjectTitle = "Kunden\nCeBIT 2015 intern";
            }
        }

        private async void Init(Project p)
        {
            IsLoading = true;

            var result = await _dataService.GetProject(p.Id);

            IsLoading = false;

            if (result.IsSuccessStatusCode && result.Result != null && result.Result.Project != null)
            {
                Project = result.Result.Project;
                if (_project.Parent == null)
                    ProjectTitle = _project.Name;
                else
                    ProjectTitle = String.Format(_resourceService.GetString("ProjectTitleWithParentFormat"), _project.Parent.Name, _project.Name);
            }
            else
            {
                var dialogResult = await _dialogService.ShowMessage(_resourceService.GetString("ProjectLoadingErrorMessage"), _resourceService.GetString("ErrorTitle"), _resourceService.GetString("ButtonRetry"), _resourceService.GetString("ButtonCancel"), null);
                if (dialogResult)
                    Init(p);
                else
                    _navigationService.GoBack();
            }
        }
    }
}
