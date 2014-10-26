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

        public LoginViewModel(IDataService dataService, ICredentialService credentialService, IDialogService dialogService, IResourceService resourceService, IExtendedNavigationService navigationService)
        {
            _dataService = dataService;
            _credentialService = credentialService;
            _dialogService = dialogService;
            _resourceService = resourceService;
            _navigationService = navigationService;

            InitCommand = new RelayCommand(Init);
            LoginCommand = new RelayCommand(Login);
            LogoutCommand = new RelayCommand(Logout);
        }

        private async void Init()
        {
            var currentCredential = _credentialService.GetEndpointCredential(DEFAULT_ENDPOINT_NAME);
            if (currentCredential != null)
                await GetUserAsyc(currentCredential);
        }

        private async void Login()
        {
            // Validate input
            if (String.IsNullOrEmpty(_endpointUrl) || String.IsNullOrEmpty(_userName) || String.IsNullOrEmpty(_password))
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

        private async Task GetUserAsyc(EndpointCredential credential)
        {
            IsLoading = true;

            _dataService.Initialize(credential);
            var result = await _dataService.GetCurrentUser();

            IsLoading = false;
            
            if (result.IsSuccessStatusCode && result.Result != null && result.Result.User != null)
            {
                CurrentUser = result.Result.User;

                EndpointUrl = null;
                UserName = null;
                Password = null;

                _navigationService.NavigateTo(ViewModelLocator.PROJECTS_PAGE_KEY, true, false);
            }
            else
            {
                await _dialogService.ShowError(_resourceService.GetString("LoginErrorMessage"), _resourceService.GetString("ErrorTitle"), _resourceService.GetString("ButtonOK"), null);
                return;
            }
        }

        private void Logout()
        {
            _credentialService.DeleteEndpointCredential(DEFAULT_ENDPOINT_NAME);
            CurrentUser = null;
            _navigationService.NavigateTo(ViewModelLocator.LOGIN_PAGE_KEY, false, true);
        }
    }
}
