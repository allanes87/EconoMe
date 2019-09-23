using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using System;
using System.Threading.Tasks;

namespace EconoMe.ViewModels.Base
{
    public class ViewModelWithResult<T> : ViewModelBase
    {
        public ViewModelWithResult(IDialogService dialogService,
           INavigationService navigationService,
           ILoggerService loggerService) : base(dialogService, navigationService, loggerService)
        { }

        public Action<T> OnResultAction { get; set; }
        public Func<T, Task> OnAsyncResultAction { get; set; }
    }
}
