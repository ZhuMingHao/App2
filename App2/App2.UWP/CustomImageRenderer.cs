using App2;
using App2.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]

namespace App2.UWP
{
    internal class CustomImageRenderer : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var image = new Windows.UI.Xaml.Controls.Image();
                image.ImageOpened += Image_ImageOpened; ;
                SetNativeControl(image);
            }
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (Control.Source == null)
                return new SizeRequest();

            var result = new Size { Width = ((BitmapSource)Control.Source).PixelWidth, Height = ((BitmapSource)Control.Source).PixelHeight };

            if (result != null)
            {
                ((IImageController)Element)?.SetIsLoading(false);
            }
            return new SizeRequest(result);
        }

        private void Image_ImageOpened(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ((IImageController)Element)?.SetIsLoading(false);
        }
    }
}