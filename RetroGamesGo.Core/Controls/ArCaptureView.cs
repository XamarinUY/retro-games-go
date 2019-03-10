using System.Windows.Input;
using Xamarin.Forms;

namespace RetroGamesGo.Core.Controls
{
    public class ArCaptureView : View
    {
        public static readonly BindableProperty ImageCapturedCommandProperty =
            BindableProperty.Create("ImageCapturedCommand", typeof(ICommand), typeof(ArCaptureView), null);

        public ICommand ImageCapturedCommand
        {
            get { return (ICommand)GetValue(ImageCapturedCommandProperty); }
            set { SetValue(ImageCapturedCommandProperty, value); }
        }
    }
}
