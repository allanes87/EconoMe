﻿using EconoMe.Services.Authentication;
using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.Validations;
using EconoMe.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EconoMe.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        #region Properties

        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();

        #endregion

        #region Commands

        public ICommand DoLoginCommand => new Command(async() => await SignInAsync());

        #endregion

        public LoginViewModel(IDialogService dialogService,
            INavigationService navigationService, 
            ILoggerService loggerService,
            IAuthenticationService authenticationService) : base(dialogService, navigationService, loggerService)
        {
            _authenticationService = authenticationService;

            AddValidations();
        }

        private async Task SignInAsync()
        {
            IsBusy = true;

            try
            {
                bool isValid = Validate();

                if (isValid)
                {
                    await DoLogin();
                    await NavigationService.NavigateToAsync<MySummaryExpensesViewModel>();
                }
                else
                {
                    await DialogService.ShowAlertAsync("Please fill all the fields", 
                        "Missing data", 
                        "OK");
                }
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

        private async Task DoLogin()
        {
            await _authenticationService.DoLogin(Email.Value, Password.Value);
        }
        
        #region Validations

        private void AddValidations()
        {
            Email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email should not be empty"
            });

            Email.Validations.Add(new EmailRule());

            Password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Password should not be empty"
            });
        }

        private bool Validate()
        {
            bool isValidEmail = Email.Validate();
            bool isValidPassword = Password.Validate();

            return isValidPassword && isValidEmail;
        }

        #endregion
    }
}
