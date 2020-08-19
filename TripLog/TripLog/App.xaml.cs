using System;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TripLog
{
    public partial class App : Application
    {
        public App()
        {
            var navService2 = DependencyService.Get<INavService>() as XamarinFormsNavService;
            
            InitializeComponent();
            // Startpunt xamarin app
            var mainPage = new NavigationPage(new MainPage());
            // Dependency injection
            var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;
            navService.XamarinFormsNav = mainPage.Navigation;
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
