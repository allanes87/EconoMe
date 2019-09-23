using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels.Base;
using Xamarin.Forms;

namespace EconoMe.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        public string HelloWorld { get; set; }
        public Command TodoCommand
        {
            get
            {
                return new Command(() => HelloWorld = "Button clicked");
            }
        }

        public TodoViewModel(IDialogService dialogService,
            INavigationService navigationService,
            ILoggerService loggerService) : base(dialogService, navigationService, loggerService)
        {
            HelloWorld = "Welcome";
        }
    }
}
