using CoreAnimation;
using CoreGraphics;
using EconoMe.IOS.Renderers;
using EconoMe.Views.Components;
using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace EconoMe.IOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;

                UpdateLineColor();
                UpdateCursorColor();
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            LineLayer lineLayer = GetOrAddLineLayer();
            lineLayer.Frame = new CGRect(0, Frame.Size.Height - LineLayer.LineHeight, Control.Frame.Size.Width, LineLayer.LineHeight);
        }

        private void UpdateLineColor()
        {
            LineLayer lineLayer = GetOrAddLineLayer();
            lineLayer.BorderColor = UIColor.White.CGColor;
        }

        private LineLayer GetOrAddLineLayer()
        {
            var lineLayer = Control.Layer.Sublayers?.OfType<LineLayer>().FirstOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new LineLayer();

                Control.Layer.AddSublayer(lineLayer);
                Control.Layer.MasksToBounds = true;
            }

            return lineLayer;
        }

        private void UpdateCursorColor()
        {
            Control.TintColor = Element.TextColor.ToUIColor();
        }

        class LineLayer : CALayer
        {
            public static nfloat LineHeight = 2f;

            public LineLayer()
            {
                BorderWidth = LineHeight;
            }
        }
    }
}