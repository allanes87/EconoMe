using EconoMe.Services.Authentication;
using EconoMe.Services.Dialog;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using EconoMe.ViewModels;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EconoMe.UnitTest.ViewModels
{
    [TestFixture]
    public class LoginViewModelTest
    {
        private readonly Mock<INavigationService> _navigationService = new Mock<INavigationService>();
        private readonly Mock<IDialogService> _dialogService = new Mock<IDialogService>();
        private readonly Mock<ILoggerService> _loggerService = new Mock<ILoggerService>();
        private readonly Mock<IAuthenticationService> _AuthenticationService = new Mock<IAuthenticationService>();

        [Test]
        public void DoLoginEmailEmpty_ShouldBeInvalid()
        {
            //Arrange
            _AuthenticationService
                .Setup(x => x.DoLogin(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var loginViewModel = new LoginViewModel(_dialogService.Object,
                _navigationService.Object,
                _loggerService.Object,
                _AuthenticationService.Object);

            loginViewModel.Email.Value = string.Empty;
            loginViewModel.Password.Value = "Passw0rd";

            //Act
            loginViewModel.DoLoginCommand.Execute(null);

            //Assert
            loginViewModel.Email.IsValid.Should().BeFalse();
        }

        [Test]
        public void DoLoginEmailEmpty_ShouldBeNotNavigate()
        {
            //Arrange
            _AuthenticationService
                .Setup(x => x.DoLogin(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var loginViewModel = new LoginViewModel(_dialogService.Object,
                _navigationService.Object,
                _loggerService.Object,
                _AuthenticationService.Object);

            loginViewModel.Email.Value = string.Empty;
            loginViewModel.Password.Value = "Passw0rd";

            //Act
            loginViewModel.DoLoginCommand.Execute(null);

            //Assert
            _navigationService.VerifyNoOtherCalls();
        }

        [Test]
        public void DoLogin_ShouldBeNavigate()
        {
            //Arrange
            _AuthenticationService
                .Setup(x => x.DoLogin(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _navigationService
                .Setup(x => x.NavigateToAsync<MainViewModel>())
                .Returns(Task.CompletedTask);

            var loginViewModel = new LoginViewModel(_dialogService.Object,
                _navigationService.Object,
                _loggerService.Object,
                _AuthenticationService.Object);

            loginViewModel.Email.Value = "alex.llanes@gmail.com";
            loginViewModel.Password.Value = "Passw0rd";

            //Act
            loginViewModel.DoLoginCommand.Execute(null);

            //Assert
            _navigationService.Verify(x => x.NavigateToAsync<MySummaryExpensesViewModel>());
        }
    }
}
