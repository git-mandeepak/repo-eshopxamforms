using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using eShopXamForms.Services.Settings;
using eShopXamForms.Validations;
using eShopXamForms.ViewModels.Base;
using Xamarin.Forms;

namespace eShopXamForms.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        private bool _isMock;
        private bool _isValid;
        private readonly ISettingsService _settingsService;

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public bool IsMock
        {
            get
            {
                return _isMock;
            }
            set
            {
                _isMock = value;
                RaisePropertyChanged(() => IsMock);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        public ICommand MockSignInCommand => new Command(async () => await MockSignInAsync());

        public LoginViewModel(
            ISettingsService settingsService)
        {   
            _settingsService = settingsService;
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
            InvalidateMock();
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private async Task MockSignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();
            bool isAuthenticated = false;

            if (isValid)
            {
                try
                {
                    await Task.Delay(10);

                    isAuthenticated = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }

            if (isAuthenticated)
            {
                _settingsService.AuthAccessToken = GlobalSetting.Instance.AuthToken;

                await NavigationService.NavigateToAsync<MainViewModel>();
                //await NavigationService.RemoveLastFromBackStackAsync();
            }

            IsBusy = false;
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private void InvalidateMock()
        {
            IsMock = _settingsService.UseMocks;
        }
    }
}
