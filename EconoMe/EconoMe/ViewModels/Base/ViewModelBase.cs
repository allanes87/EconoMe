using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EconoMe.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly ILoggerService LoggerService;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Initialized { get; set; } = false;
        public bool IsBusy { get; set; }

        public ViewModelBase(IDialogService dialogService,
            INavigationService navigationService,
            ILoggerService loggerService)
        {
            DialogService = dialogService;
            NavigationService = navigationService;
            LoggerService = loggerService;
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            Initialized = true;
            return Task.FromResult(false);
        }
    }
}
