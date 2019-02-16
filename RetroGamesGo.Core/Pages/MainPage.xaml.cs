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
    [MvxNavigationPagePresentation(WrapInNavigationPage = true)]
    public partial class MainPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        void ButtonClicked(object sender, System.EventArgs e)
        {
            string sound = ((Button)sender).BindingContext as string;
            switch (sound)
            {
                case "coin":
                    Sounds.Mario_Coin();
                    break;
                case "wakawaka":
                    Sounds.Pacman_WakaWaka();
                    break;
                case "yogafire":
                    Sounds.StreetFighter_YogaFire();
                    break;
                default:
                    break;
            }
        }
    }
}