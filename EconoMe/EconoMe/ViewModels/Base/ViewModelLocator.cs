using Autofac;
using EconoMe.Services.Dialog;
using EconoMe.Services.Navigation;
using EconoMe.Services.HttpClient;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using EconoMe.Services.LogService;

namespace EconoMe.ViewModels.Base
{
    public class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            default(bool),
            propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        public static void RegisterDependencies(bool useMockServices = false)
        {
            var builder = new ContainerBuilder();

            // Register ViewModels -> builder.RegisterType<TodoViewModel>();
            // TODO
            builder.RegisterType<TodoViewModel>();

            // Register Services. For singleton pattern use .SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<RequestProvider>().As<IRequestProvider>();
            builder.RegisterType<LoggerService>().As<ILoggerService>();

            if (useMockServices)
            {
                // Register Mock services -> builder.RegisterInstance(new TodoMockService()).As<ITodoService>();
                // TODO
            }
            else
            {
                // Register Services -> builder.RegisterType<TodoService>().As<ITodoService>().SingleInstance();
                // TODO
            }


            if (_container != null)
            {
                _container.Dispose();
            }
            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
