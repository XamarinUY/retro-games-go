namespace RetroGamesGo.Core.Pages
{
    using Xamarin.Forms.Xaml;
    using MvvmCross.Forms.Presenters.Attributes;

    /// <summary>
    /// Master detail root page
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root)]
    public partial class RootPage
    {
        public RootPage()
        {
            InitializeComponent();
        }
    }
}