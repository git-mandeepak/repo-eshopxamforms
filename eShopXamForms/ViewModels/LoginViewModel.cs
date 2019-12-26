using System;
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

        private bool _isMock;
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

        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public LoginViewModel(
            ISettingsService settingsService)
        {   
            _settingsService = settingsService;
            _userName = new ValidatableObject<string>();

            AddValidations();
            InvalidateMock();
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            //_password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private void InvalidateMock()
        {
            IsMock = _settingsService.UseMocks;
        }
    }
}
