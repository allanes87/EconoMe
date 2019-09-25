using EconoMe.Models;
using EconoMe.Services.Dialog;
using EconoMe.Services.Entries;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.Validations;
using EconoMe.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EconoMe.ViewModels
{
    public class NewEntryViewModel : ViewModelBase
    {
        private readonly IEntryService _entryService;

        #region Properties

        public ValidatableObject<int> Amount { get; set; } = new ValidatableObject<int>();
        public ValidatableObject<string> Description { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<DateTime> Date { get; set; } = new ValidatableObject<DateTime>();
        public ObservableCollection<string> EntryType
        {
            get => new ObservableCollection<string>()
            {
                Models.Enums.EntryType.Income.ToString(),
                Models.Enums.EntryType.Outcome.ToString()
            };
        }
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();
        public string SelectedCategory { get; set; }

        #endregion

        #region Commands

        public ICommand SaveCommand => new Command(async () => await SaveEntry());

        #endregion

        public NewEntryViewModel(IDialogService dialogService, 
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
                var categorieNames = await _entryService.GetCategories();
                Categories = new ObservableCollection<string>(categorieNames);
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

        private async Task SaveEntry()
        {
            IsBusy = true;

            try
            {
                await CreateEntry();

                await NavigationService.RemoveBackStackAsync();
                await DialogService.ShowAlertAsync("Your entry was created", "Well done!", "Ok");
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

        private async Task CreateEntry()
        {
            var selectedCategory = await _entryService.GetCategoryByName(SelectedCategory);

            var entryToSave = new Models.Entry()
            {
                Amount = Amount.Value,
                Category = selectedCategory,
                Description = Description.Value,
                EntryType = Models.Enums.EntryType.Income,
                Date = DateTime.Now
            };

            await _entryService.SaveEntry(entryToSave);
        }
    }
}
