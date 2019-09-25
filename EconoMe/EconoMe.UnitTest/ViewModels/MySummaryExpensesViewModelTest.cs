using EconoMe.Services.Dialog;
using EconoMe.Services.Entries;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.UnitTest.ViewModels
{
    [TestFixture]
    public class MySummaryExpensesViewModelTest
    {
        private readonly Mock<INavigationService> _navigationService = new Mock<INavigationService>();
        private readonly Mock<IDialogService> _dialogService = new Mock<IDialogService>();
        private readonly Mock<ILoggerService> _loggerService = new Mock<ILoggerService>();
        private readonly Mock<IEntryService> _entryService = new Mock<IEntryService>();

    }
}
