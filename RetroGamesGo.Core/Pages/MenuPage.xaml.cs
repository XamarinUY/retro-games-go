namespace RetroGamesGo.Core.Pages
{
    using System;
    using Xamarin.Forms;
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms.Xaml;


    /// <summary>
    /// Menu UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MenuPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Hides the master page when an item is clicked
        /// </summary>
        public void ItemTapped(object sender, EventArgs e)
        {
            if (Parent is MasterDetailPage md)
            {
                md.MasterBehavior = MasterBehavior.Popover;
                md.IsPresented = !md.IsPresented;
            }
        }
    }
}