namespace RetroGamesGo.Core.Pages
{

    using Xamarin.Forms.Xaml;
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms;
    using RetroGamesGo.Core.Utils;

    /// <summary>
    /// Main UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[MvxMasterDetailPagePresentation]
    public partial class MainPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }
    }
}