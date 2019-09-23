using Android.Support.Design.Widget;
using Android.Views;
using EconoMe.Android.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("EconoMe")]
[assembly: ExportEffect(typeof(NoShiftEffect), "NoShiftEffect")]
namespace EconoMe.Android.Effects
{
    public class NoShiftEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(Container.GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            // This is what we set to adjust if the shifting happens
            BottomNavigationViewUtils.SetShiftMode(bottomNavigationView, false, false);
        }

        protected override void OnDetached()
        {
        }
    }
}