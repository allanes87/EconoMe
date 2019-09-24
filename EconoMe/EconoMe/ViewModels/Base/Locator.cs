using Autofac;
using EconoMe.Services.Authentication;
using EconoMe.Services.DataAccess;
using EconoMe.Services.Dialog;
using EconoMe.Services.HttpClient;
using EconoMe.Services.LogService;
using EconoMe.Services.Navigation;
using System;

namespace EconoMe.ViewModels.Base
{
    public class Locator
    {
        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        private static readonly Locator _instance = new Locator();

        public static Locator Instance
        {
            get
            {
                return _instance;
            }
        }

        public Locator()
        {
        }

        public void RegisterDependencies(Action<ContainerBuilder> registerPlatformDepedencies = null)
        {
            _containerBuilder = new ContainerBuilder();

            // Register ViewModels
            _containerBuilder.RegisterType<TodoViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<MySummaryExpensesViewModel>();
            _containerBuilder.RegisterType<NewEntryViewModel>();
            _containerBuilder.RegisterType<LoginViewModel>();

            // Register Services. For singleton pattern use .SingleInstance();
            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            _containerBuilder.RegisterType<DataBaseService>().As<IDataBaseService>().SingleInstance();
            _containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            _containerBuilder.RegisterType<RequestProvider>().As<IRequestProvider>();
            _containerBuilder.RegisterType<LoggerService>().As<ILoggerService>();
            _containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();

            if (registerPlatformDepedencies != null)
                registerPlatformDepedencies.Invoke(_containerBuilder);

            if (_container != null)
            {
                _container.Dispose();
            }

            _container = _containerBuilder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
