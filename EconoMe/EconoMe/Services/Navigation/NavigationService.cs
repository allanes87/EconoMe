using EconoMe.Services.LogService;
using EconoMe.ViewModels;
using EconoMe.ViewModels.Base;
using EconoMe.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EconoMe.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private readonly ILoggerService _loggerService;
        protected readonly Dictionary<Type, Type> _mappings;

        public NavigationService(ILoggerService loggerService)
        {
            _loggerService = loggerService;

            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(TodoViewModel), typeof(TodoView));
            _mappings.Add(typeof(MainViewModel), typeof(MainPage));
            _mappings.Add(typeof(MySummaryExpensesViewModel), typeof(MySummaryExpensesPage));
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainViewModel>();
        }

        public async Task ShowInDetailAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            if (Application.Current.MainPage is MasterDetailPage)
            {
                Page page = CreateAndBindPage(typeof(TViewModel));

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

            Page page = CreateAndBindPage(viewModelType);

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

        protected Page CreateAndBindPage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            try
            {
                Page page = Activator.CreateInstance(pageType) as Page;
                ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {viewModelType}, {ex.InnerException.Message}");
            }
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
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
