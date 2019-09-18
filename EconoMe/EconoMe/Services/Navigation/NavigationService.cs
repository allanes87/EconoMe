using EconoMe.Services.LogService;
using EconoMe.ViewModels;
using EconoMe.ViewModels.Base;
using EconoMe.Views;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EconoMe.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private readonly ILoggerService _loggerService;

        public NavigationService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<TodoViewModel>();
        }

        public async Task ShowInDetailAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            if (Application.Current.MainPage is MasterDetailPage)
            {
                Page page = CreatePage(typeof(TViewModel), null);

                (Application.Current.MainPage as MasterDetailPage).Detail = new CustomNavigationView(page);

                (Application.Current.MainPage as MasterDetailPage).IsPresented = false;

                await InitializePage(page, parameter);
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 1]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        public async Task PopAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;
            await mainPage.Navigation.PopAsync();
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            _loggerService.RegisterNavigationEvent(viewModelType, parameter);

            Page page = CreatePage(viewModelType, parameter);

            // if (page is LoginView)
            // {
            //     Application.Current.MainPage = new CustomNavigationView(page);
            // }
            // else
            // {
            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            // }

            await InitializePage(page, parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        private async Task InitializePage(Page page, object parameter = null)
        {
            if ((page.BindingContext as ViewModelBase).Initialized)
                return;

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            if (page is MasterDetailPage)
            {
                await InitializePage(((MasterDetailPage)page).Master, null);
                await InitializePage(((MasterDetailPage)page).Detail, null);
            }
            else if (page is TabbedPage)
            {
                foreach (var child in (page as TabbedPage).Children)
                {
                    await InitializePage(child, parameter);
                }
            }
            else if (page is NavigationPage)
            {
                await InitializePage((page as NavigationPage).CurrentPage, null);
            }
        }
    }
}
