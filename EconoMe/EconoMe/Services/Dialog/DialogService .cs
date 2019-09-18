using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EconoMe.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task<bool> ShowConfirmAlertAsync(string message, string title, string okButtonLabel, string canelButtonLabel)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okButtonLabel, canelButtonLabel);
        }

        public void ShowLoading(string title = null)
        {
            UserDialogs.Instance.ShowLoading(title);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
