using System;
using System.Collections.Generic;

using Xamarin.Forms;


namespace RetroGamesGo.Core.Pages
{
    using Xamarin.Forms.Xaml;
    using MvvmCross.Forms.Presenters.Attributes;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[MvxNavigationPagePresentation(WrapInNavigationPage = true)]
    public partial class OnboardingPage
    {
        public OnboardingPage()
        {
            InitializeComponent();
        }
    }
}
