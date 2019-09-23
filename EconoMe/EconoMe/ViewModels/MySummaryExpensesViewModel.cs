﻿using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels.Base;

namespace EconoMe.ViewModels
{
    public class MySummaryExpensesViewModel : ViewModelBase
    {
        public MySummaryExpensesViewModel(IDialogService dialogService, 
            INavigationService navigationService,
            ILoggerService loggerService) : base(dialogService, navigationService, loggerService)
        {
        }
    }
}