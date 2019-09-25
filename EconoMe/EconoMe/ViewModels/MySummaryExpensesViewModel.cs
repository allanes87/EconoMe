using EconoMe.Services.Dialog;
using EconoMe.Services.Entries;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EconoMe.ViewModels
{
    public class MySummaryExpensesViewModel : ViewModelBase
    {
        private readonly IEntryService _entryService;

        public ObservableCollection<Models.Entry> MyEntries { get; set; }

        #region Commands

        public ICommand AddNewEntryCommand => new Command(async () => await AddNewEntry());

        #endregion


        public MySummaryExpensesViewModel(IDialogService dialogService, 
            INavigationService navigationService,
            ILoggerService loggerService,
            IEntryService entryService) : base(dialogService, navigationService, loggerService)
        {
            _entryService = entryService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await InitViewModel();

            await base.InitializeAsync(navigationData);
        }

        private async Task InitViewModel()
        {
            IsBusy = true;

            try
            {
                var entries = await _entryService.GetMyEntries();
                MyEntries = new ObservableCollection<Models.Entry>(entries);
            }
            catch (Exception ex)
            {
                IsBusy = false;

                LoggerService.LogError(ex);
                await DialogService.ShowAlertAsync(
                    ex.Message, "Ups!", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddNewEntry()
        {
            await NavigationService.NavigateToAsync<NewEntryViewModel>();
        }
    }
}
