using System;
using System.Threading.Tasks;

namespace EconoMe.ViewModels.Base
{
    public class ViewModelWithResult<T> : ViewModelBase
    {
        public Action<T> OnResultAction { get; set; }
        public Func<T, Task> OnAsyncResultAction { get; set; }
    }
}
