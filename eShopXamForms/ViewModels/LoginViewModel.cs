using System;
using eShopXamForms.Services.Settings;
using eShopXamForms.ViewModels.Base;

namespace eShopXamForms.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        private bool _isMock;
        private readonly ISettingsService _settingsService;

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

        public LoginViewModel(
            ISettingsService settingsService)
        {   
            _settingsService = settingsService;

            InvalidateMock();
        }

        private void InvalidateMock()
        {
            IsMock = _settingsService.UseMocks;
        }
    }
}
