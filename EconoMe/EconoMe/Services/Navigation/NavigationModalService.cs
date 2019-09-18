using EconoMe.ViewModels.Base;
using EconoMe.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EconoMe.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        public Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), null);
        }

        public Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), parameter);
        }

        public async Task NavigateToModalWithResultAsync<TViewModel, T>(Action<T> OnResultAction, object parameter) where TViewModel : ViewModelWithResult<T>
        {
            var page = await InternalNavigateToModalAsync(typeof(TViewModel), parameter);
            (page.BindingContext as ViewModelWithResult<T>).OnResultAction = OnResultAction;
        }

        protected virtual async Task<Page> InternalNavigateToModalAsync(Type viewModelType, object parameter)
        {
            _loggerService.RegisterNavigationEvent(viewModelType, parameter);

            Page page = CreateAndBindPage(viewModelType);

            var navigationPage = Application.Current.MainPage as CustomNavigationView;

            if (navigationPage != null)
            {
                await navigationPage.Navigation.PushModalAsync(page);
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
            return page;
        }

        public async Task NavigateBackModalAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;
            await mainPage.Navigation.PopModalAsync(true);
        }
    }
}
