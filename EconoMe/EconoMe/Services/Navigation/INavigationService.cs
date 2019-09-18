using EconoMe.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace EconoMe.Services.Navigation
{
    public interface INavigationService
    {
        #region Basic

        ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();

        #endregion

        #region Popups

        Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase;

        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase;

        Task NavigateToPopupWithResultAsync<TViewModel, T>(Action<T> OnResultAction) where TViewModel : ViewModelWithResult<T>;

        #endregion

        #region Modal

        Task NavigateBackModalAsync();

        Task NavigateToModalWithResultAsync<TViewModel, T>(Action<T> OnResultAction, object parameter) where TViewModel : ViewModelWithResult<T>;

        Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase;

        #endregion
    }
}
