
using EconoMe.Views.Components;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EconoMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CustomTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}