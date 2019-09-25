using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EconoMe.Views.Components
{
    public class CustomTabbedPage : TabbedPage
    {
        public static readonly BindableProperty ViewModelsSourceProperty = BindableProperty.Create(
            nameof(ViewModelsSource),
            typeof(List<Type>),
            typeof(CustomTabbedPage),
            default(List<Type>));

        public List<Type> ViewModelsSource
        {
            get => (List<Type>)GetValue(ViewModelsSourceProperty);
            set => SetValue(ViewModelsSourceProperty, value);
        }
    }
}
