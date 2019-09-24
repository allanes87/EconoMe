using Android.Content;
using Android.Graphics;
using EconoMe.Android.Renderers;
using EconoMe.Views.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace EconoMe.Android.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control?.Background?.SetColorFilter(global::Android.Graphics.Color.Black, PorterDuff.Mode.SrcAtop);
            }
        }
    }
}