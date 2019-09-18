using EconoMe.Services.Dialog;
using EconoMe.Services.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EconoMe.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Initialized { get; set; } = false;

        public bool IsBusy { get; set; }

        public ViewModelBase()
        {
            DialogService = Locator.Instance.Resolve<IDialogService>();
            NavigationService = Locator.Instance.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            Initialized = true;
            return Task.FromResult(false);
        }
    }
}
