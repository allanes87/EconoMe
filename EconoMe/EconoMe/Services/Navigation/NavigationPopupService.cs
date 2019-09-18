using EconoMe.ViewModels.Base;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

namespace EconoMe.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {

        public Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase
        {
            return NavigateToPopupAsync<TViewModel>(null, animate);
        }

        public async Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase
        {
            var page = CreatePage(typeof(TViewModel), parameter);
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            if (page is PopupPage)
            {
                await PopupNavigation.Instance.PushAsync(page as PopupPage, animate);
            }
            else
            {
                throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
            }
        }

        public async Task NavigateToPopupWithResultAsync<TViewModel, T>(Action<T> OnResultAction) where TViewModel : ViewModelWithResult<T>
        {
            var page = CreatePage(typeof(TViewModel), null);
            await (page.BindingContext as ViewModelBase).InitializeAsync(null);

            if (page is PopupPage)
            {
                await PopupNavigation.Instance.PushAsync(page as PopupPage, true);
            }
            else
            {
                throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
            }

            (page.BindingContext as ViewModelWithResult<T>).OnResultAction = OnResultAction;
        }
    }
}