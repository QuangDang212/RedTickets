using GalaSoft.MvvmLight.Command;
using Redmine.Portable.Interface;
using Redmine.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.Portable.ViewModel
{
    public class IssueViewModel : AsyncViewModelBase
    {
        private IDataService _dataService;

        public RelayCommand<Issue> InitCommand { get; private set; }

        private Issue _issue;
        public Issue Issue
        {
            get { return _issue; }
            set { _issue = value; RaisePropertyChanged(); }
        }

        private List<Journal> _comments;
        public List<Journal> Comments
        {
            get { return _comments; }
            set { _comments = value; RaisePropertyChanged(); }
        }

        public IssueViewModel(IDataService dataService)
        {
            _dataService = dataService;
            InitCommand = new RelayCommand<Issue>(Init);
        }

        private async void Init(Issue issue)
        {
            IsLoading = true;

            await LoadIssueAsync(issue);

            IsLoading = false;
        }

        private async Task LoadIssueAsync(Issue issue)
        {
            var result = await _dataService.GetIssue(issue.Id);

            if (result.IsSuccessStatusCode && result.Result != null && result.Result.Issue != null)
            {
                Issue = result.Result.Issue;
                if (result.Result.Issue.Journals != null)
                    Comments = GetComments(result.Result.Issue.Journals);
            }
        }

        private List<Journal> GetComments(List<Journal> input)
        {
            var comments = input.Where(e => !String.IsNullOrEmpty(e.Notes)).ToList();
            return comments;
        }
    }
}
