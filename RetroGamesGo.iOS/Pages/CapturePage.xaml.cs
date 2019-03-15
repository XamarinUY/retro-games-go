using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetroGamesGo.iOS.Pages
{
    /// <summary>
    /// Avoids using the page on the core in
    /// favor of this empty one
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CapturePage
    {
        public static readonly BindableProperty ImageCapturedCommandProperty =
            BindableProperty.Create("ImageCapturedCommand", typeof(ICommand), typeof(CapturePage), null);

        public ICommand ImageCapturedCommand
        {
            get { return (ICommand)GetValue(ImageCapturedCommandProperty); }
            set { SetValue(ImageCapturedCommandProperty, value); }
        }

        public CapturePage()
        {
            InitializeComponent();
        }
    }
}