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
    public class LoginViewModel : AsyncViewModelBase
    {
        private const string DEFAULT_ENDPOINT_NAME = "DefaultEndpoint";

        private IDataService _dataService;
        private ICredentialService _credentialService;
        private IDialogService _dialogService;
        private IResourceService _resourceService;
        private IExtendedNavigationService _navigationService;
        private INotificationService _notificationService;

        public RelayCommand InitCommand { get; private set; }
        public RelayCommand LoginCommand { get; private set; }
        public RelayCommand LogoutCommand { get; private set; }

        private string _endpointUrl;
        public string EndpointUrl
        {
            get { return _endpointUrl; }
            set { _endpointUrl = value; RaisePropertyChanged(); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }

        private bool _saveCredentials = true;
        public bool SaveCredentials
        {
            get { return _saveCredentials; }
            set { _saveCredentials = value; RaisePropertyChanged(); }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; RaisePropertyChanged(); }
        }

        private bool _showLoginUI = false;
        public bool ShowLoginUI
        {
            get { return _showLoginUI; }
            set { _showLoginUI = value; RaisePropertyChanged(); }
        }

        public LoginViewModel(IDataService dataService, ICredentialService credentialService, IDialogService dialogService, IResourceService resourceService, IExtendedNavigationService navigationService, INotificationService notificationService)
        {
            _dataService = dataService;
            _credentialService = credentialService;
            _dialogService = dialogService;
            _resourceService = resourceService;
            _navigationService = navigationService;
            _notificationService = notificationService;

            InitCommand = new RelayCommand(Init);
            LoginCommand = new RelayCommand(Login);
            LogoutCommand = new RelayCommand(Logout);
        }

        private async void Init()
        {
            var currentCredential = _credentialService.GetEndpointCredential(DEFAULT_ENDPOINT_NAME);
            if (currentCredential != null && ValidateCredentials(currentCredential.EndpointUrl, currentCredential.UserName, currentCredential.Password))
                await GetUserAsyc(currentCredential);
            else
                ShowLoginUI = true;
        }

        private async void Login()
        {
            // Validate input
            if (!ValidateCredentials(_endpointUrl, _userName, _password))
            {
                await _dialogService.ShowError(_resourceService.GetString("LoginIncompleteErrorMessage"), _resourceService.GetString("ErrorTitle"), _resourceService.GetString("ButtonOK"), null);
                return;
            }

            var currentCredential = new EndpointCredential()
            {
                Name = DEFAULT_ENDPOINT_NAME,
                EndpointUrl = _endpointUrl,
                UserName = _userName,
                Password = _password
            };

            // Save credentials if user didn't opt out
            if (_saveCredentials)
            {
                _credentialService.SaveEndpointCredential(currentCredential);
            }

            // try login
            await GetUserAsyc(currentCredential);

        }

        private bool ValidateCredentials(string endpointUrl, string userName, string password)
        {
            if (String.IsNullOrEmpty(endpointUrl) || String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
                return false;

            return true;
        }

        private async Task GetUserAsyc(EndpointCredential credential)
        {
            IsLoading = true;
            LoadingMessage = _resourceService.GetString("LoginLoadingMessage");

            _dataService.Initialize(credential);
            var result = await _dataService.GetCurrentUser();

            IsLoading = false;
            LoadingMessage = null;
            
            if (result.IsSuccessStatusCode && result.Result != null && result.Result.User != null)
            {
                CurrentUser = result.Result.User;

                EndpointUrl = null;
                UserName = null;
                Password = null;

                _navigationService.NavigateTo(ViewModelLocator.PROJECTS_PAGE_KEY, true, false);
                GetUsersIssues();
            }
            else
            {
                await _dialogService.ShowError(_resourceService.GetString("LoginErrorMessage"), _resourceService.GetString("ErrorTitle"), _resourceService.GetString("ButtonOK"), null);
                return;
            }
        }

        private async void GetUsersIssues()
        {
            if (_currentUser == null)
                return;

            var result = await _dataService.GetIssues(0, 1, null, null, null, null, Statuses.open, _currentUser.Id);
            if (result.IsSuccessStatusCode && result.Result != null)
            {
                _notificationService.UpdateMainTile("open tickets", result.Result.TotalCount, _resourceService.GetString("AppTitle"));
            }
        }

        private void Logout()
        {
            _credentialService.DeleteEndpointCredential(DEFAULT_ENDPOINT_NAME);
            CurrentUser = null;
            ShowLoginUI = true;
            _navigationService.NavigateTo(ViewModelLocator.LOGIN_PAGE_KEY, false, true);
        }
    }
}
