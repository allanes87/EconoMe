using EconoMe.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EconoMe.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        public string HelloWorld { get; set; }
        public Command TodoCommand
        {
            get
            {
                return new Command(() => HelloWorld = "Button clicked");
            }
        }

        public TodoViewModel()
        {
            HelloWorld = "Welcome to Nareia's Xamarin.Forms base project";
        }
    }
}
