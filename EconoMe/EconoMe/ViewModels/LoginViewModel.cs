using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IDialogService dialogService,
            INavigationService navigationService, 
            ILoggerService loggerService) : base(dialogService, navigationService, loggerService)
        {
        }
    }
}
