using System;
using Ninject;
using Ninject.Modules;
using TripLog.Modules;
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
        public IKernel Kernel { get; private set; }
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
           
            // Register core services
            Kernel = new StandardKernel(new TripLogCoreModule(), new TripLogNavModule());
            // Register platform specific services
            Kernel.Load(platformModules);
            SetMainPage();
        }

        private void SetMainPage()
        {
            // Startpunt xamarin app
            var mainPage = new NavigationPage(new MainPage())
            {
                // Get an instance of the MainViewModel via the IoC container and
                // use it to set the BindingContext(ViewModel) of the MainPage
                BindingContext = Kernel.Get<MainViewModel>() 
            };
            
            // Dependency injection via Xamarin.Forms default Injection service -->
            // var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;
            
            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;
            navService.XamarinFormsNav = mainPage.Navigation;
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
