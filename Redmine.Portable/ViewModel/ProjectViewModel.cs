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

        private List<Membership> _memberships;
        private List<MembershipGroup> _groupedMembers;
        public List<MembershipGroup> GroupedMembers
        {
            get { return _groupedMembers; }
            set { _groupedMembers = value; RaisePropertyChanged(); }
        }

        private List<Issue> _issues;
        public List<Issue> Issues
        {
            get { return _issues; }
            set { _issues = value; RaisePropertyChanged(); }
        }

        private List<Tracker> _trackerStates;
        public List<Tracker> TrackerStates
        {
            get { return _trackerStates; }
            set { _trackerStates = value; RaisePropertyChanged(); }
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

            await LoadProjectAsync(p.Id);
            await LoadMembershipsAsync(p.Id);
            await LoadIssuesAsync(p.Id);

            IsLoading = false;

        }

        private async Task LoadProjectAsync(int projectId)
        {
            var result = await _dataService.GetProject(projectId);

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
                    await LoadProjectAsync(projectId);
                else
                    _navigationService.GoBack();
            }
        }

        private async Task LoadMembershipsAsync(int projectId)
        {
            var result = await _dataService.GetMemberships(projectId, 0, Int16.MaxValue);

            if (result.IsSuccessStatusCode && result.Result != null && result.Result.Memberships != null)
            {
                _memberships = result.Result.Memberships.ToList();
                GroupedMembers = await Task.Run(() => { return GroupMemberships(_memberships).ToList(); }); 
            }
            else
            {
                var dialogResult = await _dialogService.ShowMessage(_resourceService.GetString("ProjectLoadingErrorMessage"), _resourceService.GetString("ErrorTitle"), _resourceService.GetString("ButtonRetry"), _resourceService.GetString("ButtonCancel"), null);
                if (dialogResult)
                    await LoadMembershipsAsync(projectId);
                else
                    _navigationService.GoBack();
            }
        }

        private async Task LoadIssuesAsync(int projectId)
        {
            var result = await _dataService.GetIssues(0, Int16.MaxValue, null, projectId);

            if (result.IsSuccessStatusCode && result.Result != null && result.Result.Issues != null)
            {
                Issues = result.Result.Issues.ToList();
                TrackerStates = GetTrackerStates(_issues).ToList();
            }
            else
            {
                var dialogResult = await _dialogService.ShowMessage(_resourceService.GetString("ProjectLoadingErrorMessage"), _resourceService.GetString("ErrorTitle"), _resourceService.GetString("ButtonRetry"), _resourceService.GetString("ButtonCancel"), null);
                if (dialogResult)
                    await LoadMembershipsAsync(projectId);
                else
                    _navigationService.GoBack();
            }
        }

        private IEnumerable<MembershipGroup> GroupMemberships(List<Membership> memberships)
        {
            var roles = memberships.SelectMany(r => r.Roles);
            var distinctRoleIds = roles.Select(e => e.Id).Distinct();
            var groups = from d in distinctRoleIds
                         select new MembershipGroup() 
                         {
                             Role = roles.Where(e=>e.Id == d).FirstOrDefault(),
                             Members = FindUsersByRoleId(d, memberships)
                         };

            return groups;
        }

        private IEnumerable<User> FindUsersByRoleId(int roleId, List<Membership> memberships)
        {
            var users = new List<User>();

            if (memberships != null)
            {
                foreach (var membership in memberships)
                {
                    if (membership.Roles != null)
                    {
                        foreach (var role in membership.Roles)
                        {
                            if (role.Id == roleId)
                                users.Add(membership.User);
                        }
                    }
                }
            }

            return users;
        }

        private IEnumerable<Tracker> GetTrackerStates(IEnumerable<Issue> issues)
        {
            var allTrackers = issues.Select(i => i.Tracker);
            var trackerIds = allTrackers.Select(t => t.Id).Distinct();
            var trackers = new List<Tracker>();
            foreach (var id in trackerIds)
            {
                var tracker = allTrackers.Where(e => e.Id == id).FirstOrDefault();
                tracker.CountOpen = (from i in issues
                                     where i.Tracker.Id == id
                                     select i).Count();
                trackers.Add(tracker);
            }

            return trackers;
        }
    }
}
