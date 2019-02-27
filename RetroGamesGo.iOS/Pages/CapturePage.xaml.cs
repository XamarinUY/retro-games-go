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
        public CapturePage()
        {
            InitializeComponent();
        }
    }
}