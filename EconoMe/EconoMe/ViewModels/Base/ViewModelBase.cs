using EconoMe.Services.Dialog;
using EconoMe.Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EconoMe.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        public bool Initialized { get; set; } = false;

        public bool IsBusy { get; set; }
        public bool IsNotBusy
        {
            get
            {
                return !IsBusy;
            }
        }

        public ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            Initialized = true;
            return Task.FromResult(false);
        }
    }
}
