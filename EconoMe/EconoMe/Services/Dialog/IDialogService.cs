using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EconoMe.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<bool> ShowConfirmAlertAsync(string message, string title, string okButtonLabel, string canelButtonLabel);
        void ShowLoading(string title = null);
        void HideLoading();
    }
}
