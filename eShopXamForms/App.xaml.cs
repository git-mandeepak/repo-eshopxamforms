using System;
using System.Threading.Tasks;
using eShopXamForms.Services;
using eShopXamForms.Services.Settings;
using eShopXamForms.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShopXamForms
{
    public partial class App : Application
    {
        ISettingsService _settingsService;

        public App()
        {
            InitializeComponent();
            InitApp();
        }

        /// <summary>
        /// App Initialize
        /// </summary>
        private void InitApp()
        {
            _settingsService = ViewModelLocator.Resolve<ISettingsService>();
            if (!_settingsService.UseMocks)
                ViewModelLocator.UpdateDependencies(_settingsService.UseMocks);
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
