using Autofac;
using EconoMe.Helpers;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EconoMe
{
    public partial class App : Application
    {
        public App(Action<ContainerBuilder> registerPlatformDependenciesAction)
        {
            InitializeComponent();
            InitApp(registerPlatformDependenciesAction);
        }

        private void InitApp(Action<ContainerBuilder> registerPlatformDependenciesAction)
        {
            Locator.Instance.RegisterDependencies(registerPlatformDependenciesAction);
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await InitNavigation();
        }

        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
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
