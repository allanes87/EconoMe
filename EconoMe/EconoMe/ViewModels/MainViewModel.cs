using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EconoMe.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public List<Type> ViewModelTypes { get; set; } = new List<Type>();

        public MainViewModel(IDialogService dialogService, 
            INavigationService navigationService,
            ILoggerService loggerService) : base(dialogService, navigationService, loggerService)
        {
            ViewModelTypes.Add(typeof(MySummaryExpensesViewModel));
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            //await NavigationService.RemoveBackStackAsync();
        }
    }
}
